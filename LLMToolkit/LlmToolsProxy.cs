using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LLMToolkit;

public  class LlmToolsProxy
{
    private Dictionary<string, LlmToolsFunction> FuncTools;

    public  LlmToolsProxy()
    {
        FuncTools = new Dictionary<string, LlmToolsFunction>();
    }

    
    public void ScanFuncTools(Assembly? assembly)
    {
        foreach (Type typetype in assembly.GetTypes())
        {
            GenerateClassDef(typetype);
        }
    }


    public string GetFuncToolDefinitions()
    {
        List<string> toolDefinitions = new List<string>();
        
        // si GenerateClassDef contiene objetos de tipo LlmToolsFunction
        if (this.FuncTools.Count > 0)
        {
            foreach (var tool in this.FuncTools)
            {
                string funcDef = @"{
   ""type"": ""function"",
   ""function"": "+ tool.Value.Definition + @"
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
        if (this.FuncTools.ContainsKey(funcTool))
        {
            LlmToolsFunction funcToolDef = this.FuncTools[funcTool];

            // Obtener el tipo de la clase Ejemplo
            Type tipo = funcToolDef.AiTooType; //typeof(Ejemplo);
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




    private void GenerateClassDef(Type classType)
    {
        MethodInfo[] methods = classType.GetMethods();

        foreach (MethodInfo methodInfo in methods)
        {
            //verifico que sea un metodo estatico y publico
            if (methodInfo.IsStatic && methodInfo.IsPublic)
            {
                // si tiene el atributo que indica que es una funcion de la AI
                var aiToolAttribute = methodInfo.GetCustomAttribute<LlmToolsAttribute>();
                if (aiToolAttribute != null)
                {
                    string funtToolDef = JsonConvert.SerializeObject(
                        GetJsonFuncTool(methodInfo, aiToolAttribute),
                        Formatting.Indented);
                    this.FuncTools.Add(aiToolAttribute.Name, new LlmToolsFunction()
                    {
                        AiTooType = classType,
                        MethodName = methodInfo.Name,
                        Description = aiToolAttribute.Description,
                        Definition = funtToolDef
                    });
                }
            }
        }
    }



    private object GetJsonFuncTool(MethodInfo metodo, LlmToolsAttribute aiToolAtribute)
    {
        var parametersDic = new Dictionary<string, object>();
        List<string> requiredParameters = new List<string>();

        foreach (ParameterInfo paramDef in metodo.GetParameters())
        {
            var param = new
            {
                Type = paramDef.ParameterType.Name,
                Description = paramDef.GetCustomAttribute<DescriptionAttribute>().Description,
            };
            parametersDic.Add(paramDef.Name, param);
            if (paramDef.GetCustomAttribute<RequiredAttribute>() != null)
            {
                requiredParameters.Add(paramDef.Name);
            }
        }

        var definicionFuncion = new
        {
            Name = aiToolAtribute.Name,
            description = aiToolAtribute.Description,
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

