# Changes for ollama custom model files




## changes in llama3.1 to avoid common allucinations

The alternative is to modify the template and try to make the model decide whether it needs to use a tool or not. 

You can do this by getting the Modelfile 

- Get ollama modelo
    - ollama show --modelfile llama3.1 > llama3.1.Modelfile

- Copy model to llama3.1-func.Modelfile

- Editing the TEMPLATE field in llama3.1-func.Modelfile

- Create a new model 
    - ollama create llama3.1-func -f llama3.1-func.Modelfile


## References

  - Modelfile documentation	
  https://github.com/ollama/ollama/blob/main/docs/modelfile.md

  - Template documentation for template format:
  https://pkg.go.dev/text/template
   
  - Issues
  https://github.com/ollama/ollama/issues/6127
