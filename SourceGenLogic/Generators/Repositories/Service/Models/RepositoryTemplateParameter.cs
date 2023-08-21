using Restarted.Generators.Processor.Interfaces;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Repositories.Service.Models
{
    public class RepositoryTemplateParameter : ITemplateParameter
    {
        
      
        public RepositoryTemplateParameter(TypeDefinitionInfo typeDefinitionInfo,string nameSpace,string sourceFileName, string dtoName, string className,string pluralEntityName, string dbContextName)
        {
            SourceFileName = sourceFileName;
            DTOName = dtoName;
            ClassName=className;
            PreferredNameSpace = nameSpace;
            DatabaseContextName= dbContextName;
            PluralEntityName = pluralEntityName;
            TypeDefinitionInfo=typeDefinitionInfo;
        }

        public TypeDefinitionInfo TypeDefinitionInfo { get; set; }
        public string PreferredNameSpace { get; set; }
     
        public string DTOName { get; set; }
        public string ClassName { get; set; }

        public string PluralEntityName { get; set; }
        public string DatabaseContextName { get; set; }

        public string SourceFileName { get; set; }
    }

   
}
