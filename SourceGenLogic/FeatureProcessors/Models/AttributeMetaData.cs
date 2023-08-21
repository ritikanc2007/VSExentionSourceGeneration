using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.FeatureProcessors.Models
{
    
    public class AttributeMetaData
    {
        public AttributeMetaData()
        {

        }
        public string GenerationPath { get; set; }
        //internal FileParamInfo FileInfo { get; set; }
    }

    internal class FileParamInfo
    {
        public FileParamInfo(string featureName, string featureModuleName, string generationPath, string methodName)
        {
            FeatureName=featureName;
            FeatureModuleName=featureModuleName;
            GenerationPath=generationPath;
            MethodName=methodName;
        }

        internal string FeatureName { get; set; }
        internal string FeatureModuleName { get; set; }
        internal string GenerationPath { get; set; }
        internal string MethodName { get; set; }

        
    }


}
