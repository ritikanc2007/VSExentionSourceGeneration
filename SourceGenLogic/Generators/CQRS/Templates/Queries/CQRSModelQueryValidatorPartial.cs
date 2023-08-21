using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.CQRS.Templates.Queries
{
    public partial class CQRSModelQueryValidator : ICodeTemplate
    {

        public CQRSModelQueryValidator(ITemplateParameter parameter)
        {
            this.Parameter = (CQRSTemplateParameter)parameter;
        }

        public CQRSTemplateParameter Parameter { get; set; }


        TemplateProcessingResult ICodeTemplate.Process()
        {

            var sourceCode = this.TransformText();
            return new TemplateProcessingResult() { SourceCode=sourceCode };
        }
    }
}
