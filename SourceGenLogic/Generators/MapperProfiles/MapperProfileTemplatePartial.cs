using Restarted.Generators.Generators.GlobalUsings;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Restarted.Generators.Generators.MapperProfiles
{
    partial class MapperProfileTemplate : ICodeTemplate
    {
        public string RegistrationString = "";
        public MapperProfileTemplate(ITemplateParameter parameter)
        {

            this.Parameter = (MapperProfileTemplateParameter)parameter;

            RegistrationString=GenerateRegistrationString();
        }

        internal MapperProfileTemplateParameter Parameter { get; set; }
        
        string GenerateRegistrationString()
        {
            //CreateMap<Address, AddressDTO>().ReverseMap();
            StringBuilder bldr = new StringBuilder();

            foreach (var item in Parameter.TargetSourceMap)
            {
                bldr.AppendLine($"CreateMap<{item.Value},{item.Key}>().ReverseMap();");
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
