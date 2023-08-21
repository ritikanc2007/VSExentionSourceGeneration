using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using Restarted.Generators.Generators.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using Restarted.Generators.Common.Context;

namespace Restarted.Generators.Generators.GlobalUsings
{
    partial  class GlobalUsingTemplate : ICodeTemplate
    {
        public GlobalUsingTemplate(ITemplateParameter parameter)  {

            this.Parameter = (GlobalUsingTemplateParameter)parameter;

            NameSpaces = this.Parameter.UniqueNamespaces;
        }
      
    internal HashSet<string> NameSpaces { get; set; } = new();
        internal GlobalUsingTemplateParameter Parameter { get; set; }
      
        TemplateProcessingResult ICodeTemplate.Process()
        {
            
            var sourceCode = this.TransformText();
            return new TemplateProcessingResult() { SourceCode=sourceCode };
        }
    }
}
