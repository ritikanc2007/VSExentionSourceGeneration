using Restarted.Generators.Generators.Controllers.Models;
using Restarted.Generators.Generators.CQRS.Models;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Controllers.Templates
{
    public partial class ControllerWithRepository : ICodeTemplate
    {
        public ControllerWithRepository(ITemplateParameter parameter)
        {
            this.Parameter=(ApiTemplateParameter)parameter;


        }

        public ApiTemplateParameter Parameter { get; set; }

        TemplateProcessingResult ICodeTemplate.Process()
        {
            var sourceCode = this.TransformText();
            return new TemplateProcessingResult() { SourceCode=sourceCode };
        }
    }
}
