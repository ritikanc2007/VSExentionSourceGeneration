using Restarted.Generators.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Controllers.Attributes
{


    [AttributeUsage( AttributeTargets.Method, AllowMultiple = true)]
    public class GenerateApiCQRSAttribute : GeneratorAttributeBase
    {
        public GenerateApiCQRSAttribute(
            string action, 
            string route,
            string cqrsRequestName,
            string methodName="",string controllerName="",string featureName = "",string featureModuleName="")
        {
            this.Action = action;
            this.Route = route;
            this.CQRSRequestName = cqrsRequestName;
            this.MethodName = methodName;
            this.ControllerName = controllerName;
            this.FeatureName= featureName;
            this.FeatureModuleName =featureModuleName;
        }
       

            public string Action { get; set; }

        public string Route { get; set; }

        /// <summary>
        /// Default it will take repositories methodname, if specified here, this name will be used in the controller
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// this will be used in the scenario where you have different controllers
        /// e.g. AuthenticationController, UsersController both used same repository
        /// </summary>
        public string ControllerName { get; set; }

        // this is added later for seperate Controller processing

        public string CQRSRequestName { get; set; }
    }

}
