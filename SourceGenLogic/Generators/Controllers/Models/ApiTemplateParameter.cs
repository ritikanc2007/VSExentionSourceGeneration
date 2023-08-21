using Restarted.Generators.Definitions;
using Restarted.Generators.Generators.CQRS.Models;
using Restarted.Generators.Processor.Interfaces;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Controllers.Models
{
    public class ApiTemplateParameter : ITemplateParameter
    {

        public ApiTemplateParameter(
            TypeDefinitionInfo typeDefinitionInfo,
            string nameSpace,
            string sourceFileName,
            string entityName,
            string pluralName,
            List<ControllerAction> actions)
        {
            TypeDefinitionInfo = typeDefinitionInfo;
            PreferredNameSpace = nameSpace;
            SourceFileName = sourceFileName;
            EntityName= entityName;
            PluralName = pluralName;
            Actions = actions;
        }

        public TypeDefinitionInfo TypeDefinitionInfo { get; set; }
        public string PreferredNameSpace { get; set; }

        public string SourceFileName { get; set; }

        public string EntityName { get; set; }

        public string PluralName { get; set; }
        public List<ControllerAction> Actions { get; set; } = new List<ControllerAction> { };


    }
}