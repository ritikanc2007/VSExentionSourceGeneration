using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using Restarted.Generators.Generators.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;

namespace Restarted.Generators.Generators.DTO.Template
{
    partial class ModelDTO : ICodeTemplate
    {
        public ModelDTO(ITemplateParameter parameter)
        {

            this.Parameter = (ModelTemplateParameter)parameter;
        }
        internal ModelTemplateParameter Parameter { get; set; }

        TemplateProcessingResult ICodeTemplate.Process()
        {

            var sourceCode = this.TransformText();
            return new TemplateProcessingResult() { SourceCode=sourceCode };
        }

        public KeyValuePair<string,string> ConstructorParamString()
        {
            StringBuilder blder = new StringBuilder();
            StringBuilder blderMap = new StringBuilder();
            int idx = 0;
            foreach (var nameType in this.Parameter.Members)
            {
                if (string.IsNullOrEmpty(nameType.Name)) continue;
                idx +=1;
                string comma = (idx == Parameter.Members.Count) ? "" : ",";
                string name = LowerFirstChar(nameType.Name);
                blder.AppendLine($"{nameType.Type} {name}{comma}");

                blderMap.AppendLine($" {nameType.Name} = {name} " +";");
            }
            return new KeyValuePair<string,string>(blder.ToString(), blderMap.ToString());
        }

        public string PropertiesString()
        {
            StringBuilder blderCtor = new StringBuilder();

            
            int idx = 0;

            string conststructorParam = string.Empty;
            string memberMapping = string.Empty;
            foreach (var nameType in this.Parameter.Members)
            {
                idx +=1;
                string comma = (idx == Parameter.Members.Count) ? "" : ",";

               
                blderCtor.AppendLine($"public {nameType.Type} {nameType.Name} " +" { get; set; }");

               

            }
            return blderCtor.ToString();
        }

        private string LowerFirstChar(string value)
        { return value.Substring(0, 1).ToLower()+ value.Substring(1); }
    }
}
