using Restarted.Generators.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.CQRS.Attributes
{


    [AttributeUsage(AttributeTargets.Interface| AttributeTargets.Method, AllowMultiple = true)]
    public class GeneratorCQRSAttribute : GeneratorAttributeBase
    {
        public GeneratorCQRSAttribute(

          string featureName,
          string featureModuleName
                 )
        {

            base.FeatureName = featureName;
            base.FeatureModuleName = featureModuleName;
           
        }
        public GeneratorCQRSAttribute(
            string requestType =CQRSRequestType.None ,
            string featureName="",
            string featureModuleName = "",
            string convention = ""
            
            )
        {

            base.FeatureName = featureName;
            base.FeatureModuleName = featureModuleName;
            base.Convention = string.IsNullOrEmpty(convention) ? "{entity}sRepository" : convention;
            RequestType = requestType;
            
        }

        public string RequestType { get; set; }
      
    }
}
