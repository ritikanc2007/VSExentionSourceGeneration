using Restarted.Generators.Generators.GlobalUsings;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Dependencies
{
    partial class DependencyRegistrationTemplate:ICodeTemplate
    {
        public string RegistrationString = "";
        public DependencyRegistrationTemplate(ITemplateParameter parameter)
        {

            this.Parameter = (DependencyRegistrationTemplateParameter)parameter;

            RegistrationString=GenerateRegistrationString();
        }

        internal DependencyRegistrationTemplateParameter Parameter { get; set; }

        string GenerateRegistrationString()
        {
            //services.AddScoped<IUserRepository, UserRepository>();
            StringBuilder bldr = new StringBuilder();

            foreach (var item in Parameter.TargetSourceMap)
            {
                bldr.AppendLine($"services.AddScoped<{item.Key},{item.Value}>();");
            }
            return bldr.ToString();
        }

        TemplateProcessingResult ICodeTemplate.Process()
        {
            var sourceCode = this.TransformText();
            return new TemplateProcessingResult() { SourceCode=sourceCode };
        }
    }
}
