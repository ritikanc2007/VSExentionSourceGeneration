using Restarted.Generators.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.CQRS.Attributes
{
   
    /*
     * [GenerateApi(Implementation:"CQRS")]
         IUsersController	
	        [GenerateApi(Action:"GET",CQRSRequestType:"Query" RequestName:"GetUsersRequest")]
	        List<> GetUsers(pageno, index, filter)

         [GenerateApi(Implementation:"Repository",Interface:"IUsersRepository")]
         IUsersController	
	        [GenerateApi(Action:"GET", RepositoryMethod:"GetPagedUsersAsync")]
	        List<> GetUsers(pageno, index, filter)

	        [GenerateApi(Action:"Create", RepositoryMethod:"AddAsync")]
	        int Create(UserDTO)
     */

    [AttributeUsage(AttributeTargets.Interface| AttributeTargets.Method, AllowMultiple = true)]
    public class GeneratorApiAttribute : GeneratorAttributeBase
    {
        public GeneratorApiAttribute(

            
             string implementation,
             string controllerName,
             string featureName = "", string featureModuleName = ""
                 )
        {
          
            this.Implementation = implementation;
            
            
            this.ControllerName = controllerName;

            this.FeatureName= featureName;
            this.FeatureModuleName =featureModuleName;
        }
    
        
        
        public string Implementation { get; set; } = ImplementationType.CQRSWithRepository;

        public string ControllerName { get; set; }


    }
}
