using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Source.Generator.TemplateManager.Templates.Repository.Parameters
{
    public enum ParameterType
    {
        AccessModifier,
        NameSpace,
        Name,
        Interface,
        DBContext
    }

    public enum MethodParameterType
    {
        AccessModifier,
        ReturnType,
        Name,
        ParameterString,
        LinqQuery,
        Entity,
        Plural,
        DTO
    }
    public class RepositoryParameters
    {
 
        public RepositoryParameters()
        {

        }

        public ParameterType Type { get; set; }
        public ParameterType PlaceHolder { get; set; }

        
        public static Dictionary<ParameterType, string> GetClassParameters() 
                => new Dictionary<ParameterType, string>()
                  {
                    { ParameterType.AccessModifier, @"{AccessModifier}" },
                    { ParameterType.NameSpace, @"{@NameSpace}" },
                    { ParameterType.Name, @"{@Name}"},
                    { ParameterType.Interface, @"{@Interface}"},
                    { ParameterType.DBContext, @"{@DBContext}"}
                  };

        public static Dictionary<MethodParameterType, string> GetMethodParameters()
        => new Dictionary<MethodParameterType, string>()
          {
                    { MethodParameterType.AccessModifier, @"{AccessModifier}" },
                    { MethodParameterType.ReturnType, @"{@ReturnType}" },
                    { MethodParameterType.Name, @"{@Name}"},
                    { MethodParameterType.ParameterString, @"{ParameterString}"},
                    { MethodParameterType.LinqQuery, @"{LinqQuery}"},
                    { MethodParameterType.Entity, @"{@Entity}"},
                    { MethodParameterType.Plural, @"{@Plural}"},
                    { MethodParameterType.DTO, @"{@DTO}"}
          };
     
    }
}
