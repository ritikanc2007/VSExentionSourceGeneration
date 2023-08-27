using Source.Generator.TemplateManager.Templates.Repository.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Generator.TemplateManager.Models.Enums
{
    public static class PathConstants
    {
        //Path Constants
        public static string PresetPathTemplate = @"\Preset\{PresetType}\";
        public static string TypePathTemplate = @"\Repository\Parent\";
        public static string MethodPathTemplate = @"\Repository\Methods\{Commands}\{@MethodName}\{ResultType}Result\";


    }
    public static class PlaceholderConstants
    {
        //Path Constants
        public static string AccessModifier = @"{AccessModifier}";
        public static string NameSpace = @"{@NameSpace}";
        public static string Name = @"{@Name}";
        public static string Interface = @"{@Interface}";
        public static string DBContext = @"{@DBContext}";

        public static string Entity = @"{@Entity}";
        public static string Plural = @"{@Plural}";
        public static string DTO = @"{@DTO}";

        public static string ParameterString = @"{ParameterString}";
        public static string ReturnType = @"{@ReturnType}";
        public static string LinqQuery = @"{LinqQuery}";

        public static string CQRSRequestName = "{@CQRSRequestName}";
        public static string MethodName = "{@MethodName}";
        public static string RequestArgumentsString = "{@RequestArgumentsString}";// request.Id,request.Name
        public static string ArgumentsTypeNameString = "{@ArgumentsTypeNameString}"; //"int id,string name"

        // DTO
        public static string dtoContructorParameterStrings="{@dtoContructorParameterStrings}";
        public static string dtoContructorParameterPropertyAssignmentStrings="{@dtoContructorParameterPropertyAssignmentStrings}";
        public static string dtoPropertyDefinitionStrings = "{@dtoPropertyDefinitionStrings}";

        public static string DependencyRegistrationString="{@DependencyRegistrationString}";
        public static string MapperString = "{@MapperString}";
        public static string GlobalUsingNameSpaceStrings="{@GlobalUsingNameSpaceStrings}";

        // Controllers CQRS
        public static string ActionMethodsGetBodies = "{@ActionMethodsGetBodies}";
        public static string ActionMethodsPostBodies = "{@ActionMethodsPostBodies}";
        public static string ActionMethodsPutBodies = "{@ActionMethodsPutBodies}";
        public static string ActionMethodsDeleteBodies = "{@ActionMethodsDeleteBodies}";

        //Controller Repo

    }  
}
