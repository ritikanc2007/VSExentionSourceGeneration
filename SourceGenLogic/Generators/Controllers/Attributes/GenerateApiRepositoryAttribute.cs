using Restarted.Generators.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Controllers.Attributes
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class GenerateApiRepositoryAttribute : GeneratorAttributeBase
    {
        public GenerateApiRepositoryAttribute(
            string repositoryInterface,
            string method)
        {
            this.RepositoryInterface = repositoryInterface;
            this.Method = method;
        }
        public string RepositoryInterface { get; set; }
        public string Method { get; set; }


    }


}
