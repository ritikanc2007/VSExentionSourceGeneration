using Restarted.Generators.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Repositories.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true)]
    public class GeneratorRepositoryAttribute : GeneratorAttributeBase
    {
        /// <summary>
        /// </summary>
        /// <param name="pluralEntityName">PluralEntityName is like Users for User entity. This is required to query entity framework data.</param>
        public GeneratorRepositoryAttribute(
            string featureName,
            string featureModuleName,
            string convention,            
            
            string dtoName,
            string dbContextName,
            string disable)
        {

            FeatureName = featureName;
            FeatureModuleName = featureModuleName;
            Convention = string.IsNullOrEmpty(convention) ? "{entity}sRepository" : convention;
            DTOName=dtoName;
            DatabaseContextName = dbContextName;
            bool.TryParse(disable, out bool disabledValue);
            Disable = disabledValue;

        }

        public string DTOName { get; set; }
        public string DatabaseContextName { get; set; }
    }
}
