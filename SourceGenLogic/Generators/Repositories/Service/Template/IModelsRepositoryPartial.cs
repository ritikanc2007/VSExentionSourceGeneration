using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Repositories.Service.Template
{
    partial class IModelsRepository:ICodeTemplate
    {
        
        public IModelsRepository(ITemplateParameter parameter)
        {
            this.Parameter = (RepositoryTemplateParameter)parameter;

         
        }

        public RepositoryTemplateParameter Parameter { get; set; }

  
        TemplateProcessingResult ICodeTemplate.Process()
        {

            var sourceCode = this.TransformText();
            return new TemplateProcessingResult() { SourceCode=sourceCode };
        }
    }
}
