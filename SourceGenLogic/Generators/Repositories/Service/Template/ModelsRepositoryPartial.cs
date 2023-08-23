using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Restarted.Generators.Generators.Repositories.Service.Template
{
    public partial  class ModelsRepository:ICodeTemplate
    {
        public string PluralClassName;
        public string IncludesString;
        public ModelsRepository(ITemplateParameter parameter)
        {
            this.Parameter = (RepositoryTemplateParameter)parameter;
            PluralClassName = this.Parameter.PluralEntityName;

            StringBuilder bldr = new StringBuilder();
            if (this.Parameter.Includes != null && this.Parameter.Includes.Length>0)
            {
                foreach (string item in this.Parameter.Includes)
                {
                    if (!string.IsNullOrEmpty(item))
                        bldr.AppendLine($".Include(\"{item}\")");
                }
                IncludesString = bldr.ToString();
            }
        }

        public RepositoryTemplateParameter Parameter { get; set; }



        TemplateProcessingResult ICodeTemplate.Process()
        {

            var sourceCode = this.TransformText();
            return new TemplateProcessingResult() { SourceCode=sourceCode };
        }
    }
}
