using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Text;

namespace Restarted.Generators.FeatureProcessors.Models
{
    
    public class AttributeMetaData
    {
        public AttributeMetaData()
        {

        }
        public string GenerationPath { get; set; }
        public FileParamInfo PathConvention { get; set; }

        public string IsMethodGeneration { get; set; }
    }

    public class FileParamInfo
    {
        public FileParamInfo(string conventionPath,  string featureName, string featureModuleName,  string methodName)
        {
            FeatureName=featureName;
            FeatureModuleName=featureModuleName;
            ConventionPath= conventionPath;
            MethodName=methodName;
        }

        public string FeatureName { get; set; }
        public string FeatureModuleName { get; set; }
        public string MethodName { get; set; }
        public string ConventionPath { get; set;}
    }


}
