using Restarted.Generators.Generators.Controllers.Models;
using Restarted.Generators.Generators.Dependencies;
using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Generators.GlobalUsings;
using Restarted.Generators.Generators.MapperProfiles;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.FeatureProcessors.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Process
{
    internal static class ReplacementHelper
    {
        //{@NameSpace}
        //{@Name}
        //{@Interface}
        //{@DBContext}
        //{@DTO}
        //{@Plural}
        //{@Entity}
        public static Dictionary<string, string> Repository(ITemplateParameter parameter)
        {
            RepositoryTemplateParameter param = (RepositoryTemplateParameter)parameter;
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.Name, param.SourceFileName},
                    { PlaceholderConstants.Interface, $"I{param.SourceFileName}" },
                    { PlaceholderConstants.DBContext, param.DatabaseContextName},
                    { PlaceholderConstants.DTO, param.DTOName},
                    { PlaceholderConstants.Plural, param.PluralEntityName},
                    { PlaceholderConstants.Entity, param.ClassName}
                  };
        }
        //      {@NameSpace}
        //      {@Name}
        //      {@ActionMethodsGetBodies}
        //      {@ActionMethodsPostBodies}
        //      {@ActionMethodsPutBodies}
        //      {@ActionMethodsDeleteBodies}
        internal static Dictionary<string, string>? ControllerWithCQRS(ITemplateParameter parameter)
        {
            ApiTemplateParameter param = (ApiTemplateParameter)parameter;
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.Name, param.SourceFileName},
                    { PlaceholderConstants.ActionMethodsGetBodies, ParameterHelper.ActionMethodsGetBodies(param) },
                    { PlaceholderConstants.ActionMethodsPostBodies, ParameterHelper.ActionMethodsPostBodies(param) },
                    { PlaceholderConstants.ActionMethodsPutBodies, ParameterHelper.ActionMethodsPutBodies(param) },
                    { PlaceholderConstants.ActionMethodsDeleteBodies, ParameterHelper.ActionMethodsDeleteBodies(param) }
                  };
        }
        //{@NameSpace}
        //{@Name}
        //{@Interface}
        //{@DTO}
        internal static Dictionary<string, string>? ControllerWithRepository(ITemplateParameter parameter)
        {
            ApiTemplateParameter param = (ApiTemplateParameter)parameter;
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.Name, param.SourceFileName},
                    { PlaceholderConstants.Interface, $"I{param.PluralName}Repository" },
                    { PlaceholderConstants.DTO, $"{param.EntityName}DTO" }
                  };
        }
        //Query / command Request
        //	@NameSpace
        //	@CQRSRequestName
        //	@ArgumentsTypeNameString
        //	{@ReturnType}

        //Request Query/Command Handler
        //	{@NameSpace}
        //	{@CQRSRequestName}
        //	{@ReturnType}
        //	{@Interface}
        //	{@MethodName}
        //	{@RequestArgumentsString}
        internal static Dictionary<string, string> CQRSActions(ITemplateParameter parameter)
        {
            CQRSTemplateParameter param = (CQRSTemplateParameter)parameter;
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.CQRSRequestName, param.CQRSRequestName},
                    { PlaceholderConstants.Interface, param.TypeDefinitionInfo.Name },
                    { PlaceholderConstants.MethodName, param.CurrentMethod.Name },
                    { PlaceholderConstants.ReturnType, param.CurrentMethod.ReturnType },
                    { PlaceholderConstants.ArgumentsTypeNameString, ParameterHelper.ArgumentsTypeNameString(param.CurrentMethod) },
                    { PlaceholderConstants.RequestArgumentsString, ParameterHelper.RequestArgumentsString(param.CurrentMethod) },

                  };
        }

        //{@NameSpace}
        //{@DependencyRegistrationString}
        internal static Dictionary<string, string>? DependencyRegistration(ITemplateParameter parameter)
        {
            DependencyRegistrationTemplateParameter param = (DependencyRegistrationTemplateParameter)parameter;
            StringBuilder blder = new StringBuilder();
            param.TargetSourceMap.ToList().ForEach
                (
                    pair =>
                    {
                        blder.AppendLine($"services.AddScoped<{pair.Key},{pair.Value}>();");
                    }
                );
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.DependencyRegistrationString, blder.ToString() },
                  };
        }

        //{@NameSpace}
        //{@Name}
        //{@dtoContructorParameterStrings}
        //{@dtoContructorParameterPropertyAssignmentStrings}
        //{@dtoPropertyDefinitionStrings}
        internal static Dictionary<string, string>? DTO(ITemplateParameter parameter)
        {
            ModelTemplateParameter param = (ModelTemplateParameter)parameter;
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.Name, param.SourceFileName},
                    { PlaceholderConstants.dtoContructorParameterStrings, ParameterHelper.ConstructorParamString(param).Key },
                    { PlaceholderConstants.dtoContructorParameterPropertyAssignmentStrings, ParameterHelper.ConstructorParamString(param).Value},
                    { PlaceholderConstants.dtoPropertyDefinitionStrings, ParameterHelper.PropertiesString(param)},
                  };
        }

        //{@GlobalUsingNameSpaceStrings}	
        internal static Dictionary<string, string>? GlobalUsings(ITemplateParameter parameter)
        {

            GlobalUsingTemplateParameter param = (GlobalUsingTemplateParameter)parameter;
            StringBuilder blder = new StringBuilder();
            param.UniqueNamespaces.ToList().ForEach
                (
                    nameSpace =>
                    {
                        blder.AppendLine($" global using  {nameSpace};");
                    }
                );
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.GlobalUsingNameSpaceStrings, blder.ToString() },
                  };
        }
        //{@NameSpace}
        //{@MapperString}
        internal static Dictionary<string, string>? Mappers(ITemplateParameter parameter)
        {
            MapperProfileTemplateParameter param = (MapperProfileTemplateParameter)parameter;
            StringBuilder blder = new StringBuilder();
            param.TargetSourceMap.ToList().ForEach
                (
                    pair =>
                    {
                        blder.AppendLine($"CreateMap<{pair.Value},{pair.Key}>().ReverseMap();");
                    }
                );
            return new Dictionary<string, string>()
                  {
                    { PlaceholderConstants.NameSpace, param.PreferredNameSpace},
                    { PlaceholderConstants.MapperString, blder.ToString() },
                  };
        }

        //Same as Repository
        internal static Dictionary<string, string>? RepositoryInterface(ITemplateParameter parameter)
        {
            return Repository(parameter);
        }
    }
}
