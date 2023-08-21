using SourceGeneratorParser.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.CQRS.Models
{
    public class ControllerAction
    {
    

        public string HttpAction { get; set; }
        public string Route { get; set; }
        public string MethodName { get; set; }
        /// <summary>
        /// comma seperated parameter string like, "int id, string name"
        /// </summary>
        public string MethodParameterDefinition { get; set; }
        /// <summary>
        /// comma seperated method parameter like id,name,userName
        /// </summary>
        public string MethodParametersString { get; set; }

        public string MethodReturnType { get; set; }
        public string CQRSRequestName { get; set; }

        public string PluralEntityName { get; set; }
    }
    public class ControllerInfo
    {
       
        public List<ControllerAction> Actions { get; set; } = new List<ControllerAction> { };

        public MethodItemInfo CurrentMethod { get; set; }
        public string PreferredNameSpace { get; set; }
        public string ControllerName { get; internal set; }
        public string GenerationFilePath { get; internal set; }
        public string PluralEntityName { get; internal set; }
    }
}
