using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LLMToolkit.Tools;

public class LlmToolsProxy
{
    // Dictionary to store the functions tools definitions
    private Dictionary<string, LlmToolFunction> FuncTools;

    public LlmToolsProxy()
    {
        FuncTools = new Dictionary<string, LlmToolFunction>();
    }


    public void ScanFuncTools(Assembly? assembly)
    {
        foreach (Type typetype in assembly.GetTypes())
        {
            RegisterTools(typetype);
        }
    }


    public string GetFuncToolDefinitions()
    {
        List<string> toolDefinitions = new List<string>();

        // si RegisterTools contiene objetos de tipo LlmToolFunction
        if (FuncTools.Count > 0)
        {
            foreach (var tool in FuncTools)
            {
                string funcDef = @"{
   ""type"": ""function"",
   ""function"": " + tool.Value.Definition + @"
}";
                toolDefinitions.Add(funcDef);
            }
            var generatedToolDef = string.Join(",", toolDefinitions);
            return "[" + generatedToolDef + "]";
        }
        return "";
    }


    public string Invoke(string funcTool, string parameters)
    {
        if (FuncTools.ContainsKey(funcTool))
        {
            LlmToolFunction funcToolDef = FuncTools[funcTool];

            // Obtener el tipo de la clase Ejemplo
            Type tipo = funcToolDef.LlmToolType; //typeof(Ejemplo);
            MethodInfo methodInfo = tipo.GetMethod(funcToolDef.MethodName, BindingFlags.Static | BindingFlags.Public);

            // Obtener los parámetros del método
            ParameterInfo[] parametersInfos = methodInfo.GetParameters();

            // Deserializar el string JSON los diferentes tipo del parámetros
            JObject jsonParams = JObject.Parse(parameters);

            // ahora debe aparear los parametros con los valores del json
            object[] argumentos = new object[parametersInfos.Length];
            // recorre los parametros del metodo y verifica los parametros del json
            for (int i = 0; i < parametersInfos.Length; i++)
            {
                ParameterInfo paramInfo = parametersInfos[i];
                string paramName = paramInfo.Name;
                JToken paramValue = jsonParams[paramName];
                if (paramValue == null)
                {
                    // si el parametro no esta en el json
                    // si el parametro es requerido
                    if (paramInfo.GetCustomAttribute<RequiredAttribute>() != null)
                    {
                        throw new Exception($"Parameter {paramName} is required.");
                    }
                    else
                    {
                        // si el parametro no es requerido
                        argumentos[i] = null;
                    }
                }
                else
                {
                    // si el parametro esta en el json
                    // se debe convertir el valor del json al tipo del parametro
                    argumentos[i] = paramValue.ToObject(paramInfo.ParameterType);
                }
            }

            // invocar el metodo
            return (string)methodInfo.Invoke(null, argumentos);

        }
        return "";
    }




    private void RegisterTools(Type classType)
    {
        MethodInfo[] methods = classType.GetMethods();

        foreach (MethodInfo methodInfo in methods)
        {
            // Only static public methods
            if (methodInfo.IsStatic && methodInfo.IsPublic)
            {
                // If the method is llm tool function add it to the dictionary
                var llmToolAtribute = methodInfo.GetCustomAttribute<LlmToolAttribute>();
                if (llmToolAtribute != null)
                {
                    string functionToolDef = JsonConvert.SerializeObject(
                        GetJsonFuncTool(methodInfo, llmToolAtribute),
                        Formatting.Indented);

                    FuncTools.Add(llmToolAtribute.Name, new LlmToolFunction()
                    {
                        LlmToolType = classType,
                        MethodName = methodInfo.Name,
                        Description = llmToolAtribute.Description,
                        Definition = functionToolDef
                    });
                }
            }
        }
    }



    private object GetJsonFuncTool(MethodInfo methodInf, LlmToolAttribute llmToolAtribute)
    {
        var parametersDic = new Dictionary<string, object>();
        List<string> requiredParameters = new List<string>();

        foreach (ParameterInfo paramDef in methodInf.GetParameters())
        {
            var param = new
            {
                Type = paramDef.ParameterType.Name,
                paramDef.GetCustomAttribute<DescriptionAttribute>().Description,
            };
            parametersDic.Add(paramDef.Name, param);
            if (paramDef.GetCustomAttribute<RequiredAttribute>() != null)
            {
                requiredParameters.Add(paramDef.Name);
            }
        }

        var definicionFuncion = new
        {
            llmToolAtribute.Name,
            description = llmToolAtribute.Description,
            parameters = new
            {
                type = "object",
                properties = parametersDic,
                required = requiredParameters.ToArray()
            }
        };
        return definicionFuncion;
    }
}

