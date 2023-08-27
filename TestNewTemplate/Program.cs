// See https://aka.ms/new-console-template for more information

using Source.Generator.TemplateManager;
using Source.Generator.TemplateManager.Models.Identifiers;
using Source.Generator.TemplateManager.Templates.Repository.Parameters;
using SourceGeneratorParser.Parsers.Helpers;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Source.Generator.TemplateManager.RepositoryPreset;

string basePath = @"C:\Users\Narendra\source\repos\VSExentionSourceGeneration\Source.Generator.TemplateManager\Templates";



var classParameters = RepositoryParameters.GetClassParameters();

var ClassPlaceHolderValues = new Dictionary<string, string>();
// populate values
ClassPlaceHolderValues.Add(classParameters[ParameterType.Name] , "UserRepository");
ClassPlaceHolderValues.Add(classParameters[ParameterType.AccessModifier], "public");
ClassPlaceHolderValues.Add(classParameters[ParameterType.DBContext], "MyDBContext");
ClassPlaceHolderValues.Add(classParameters[ParameterType.NameSpace], "Admin.Repositories");
ClassPlaceHolderValues.Add(classParameters[ParameterType.Interface], "IUserRepository");

RepositoryPreset preset = new RepositoryPreset(basePath);

// Process Main 
string mainSource = Process<ParameterType>(preset.Parent, ClassPlaceHolderValues);

var methodParameters = RepositoryParameters.GetMethodParameters();
Dictionary<string, List<string>> generatedSource = new Dictionary<string, List<string>>();
List<string> MethodSources = new List<string>();
foreach (var method in preset.Methods)
{
    var placeHolderValues = new Dictionary<string, string>();
    // populate values
    string DTO = "UserDTO";
    string entity = "User";
    string plural = "Users";
    placeHolderValues.Add(methodParameters[MethodParameterType.Name], method.Key);
    placeHolderValues.Add(methodParameters[MethodParameterType.DTO], DTO);
    placeHolderValues.Add(methodParameters[MethodParameterType.Entity], entity);
    placeHolderValues.Add(methodParameters[MethodParameterType.Plural], plural);


    string methodSource= Process<MethodParameterType>(method.Value, placeHolderValues);
    MethodSources.Add(methodSource);
}

var finalSourceCode= ClassParserHelper.AddNewMethodsToClass(mainSource, MethodSources);
string Process<T>(Identifier identifier, Dictionary<string,string> parameters)
{
    

    var SourceTemplate = File.ReadAllText(identifier.Path.Main);

    foreach (var placeHolder in parameters)
    {
        SourceTemplate= SourceTemplate.Replace(placeHolder.Key, placeHolder.Value);
    }
    //var TestTemplate = File.ReadAllText(identifier.Path.Test);

    return SourceTemplate;
}




Console.WriteLine("Hello, World!");
