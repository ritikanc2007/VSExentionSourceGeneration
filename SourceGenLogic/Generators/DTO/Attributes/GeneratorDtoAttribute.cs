using Restarted.Generators.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.DTO.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class GeneratorDtoAttribute : GeneratorAttributeBase
    {
        public GeneratorDtoAttribute(
            string featureName,
            string featureModuleName,
            string convention,
            
            string members,            
            bool allPropertiesNullable)

        {
            base.FeatureName = featureName;
            base.FeatureModuleName = featureModuleName;
            base.Convention = string.IsNullOrEmpty(convention) ? "{entity}DTO" : convention;
            Members=members;
            AllPropertiesNullable=allPropertiesNullable;
        }

        public string Identifier { get; set; }


        /// <summary>
        /// Convention is used to define pattern for DTO Name. e.g.{Entity}DTO will generate name UserDTO.cs
        /// </summary>
        //public string Convention { get; set; } = "{entity}DTO";
        /// <summary>
        /// you can define fields in multiple ways. If type is not provided, it will use types used in the entity definition. 
        /// "Id,Name,UserName" OR "Id:int?,Name:string?,UserName:string?, Address, Phone, Email"  
        /// "Id,Name,UserName" OR "Id:int?,Name:string?,UserName:string?, Address:AddressDTO, Contact:ContactDTO, Roles:List<RoleDTO>"  
        /// if you pass "" i.e. empty in the Members, it will add all properties of entity to DTO
        /// </summary>
        string Members { get; set; }

        /// <summary>
        /// This will define all properties as nullable.
        /// e.g. "Id:int?,Name:string?,UserName:string?"
        /// </summary>
        bool AllPropertiesNullable { get; set; }

    }
}
