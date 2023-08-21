using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Definitions;
using Restarted.Generators.FeatureProcessors.Controllers;
using Restarted.Generators.FeatureProcessors.CQRS;
using Restarted.Generators.FeatureProcessors.DependencyRegistrations;
using Restarted.Generators.FeatureProcessors.DTO;
using Restarted.Generators.FeatureProcessors.GlobalUsing;
using Restarted.Generators.FeatureProcessors.MapperProfiles;
using Restarted.Generators.FeatureProcessors.Repository;
using Restarted.Generators.Receivers.Decorated;
using Restarted.Generators.Templates;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace Restarted.Generators;

[Generator]
public class ServiceGenerator : ISourceGenerator
{
    private const string ConstGeneratorDto = "GeneratorDto";
    private const string ConstFeatureName = "FeatureName";
    private const string ConstFeatureModuleName = "FeatureModuleName";
    private const string ConstGenerationPath = "GenerationPath";
    private const string ConstConvention = "Convention";
    private const string ConstMembers = "Members";

    // namespace in the key and value contains controller/dto/Repositorys/RepoInterface etc
    private static ConcurrentDictionary<string,string> GlobalNameSpacesConcurrent = new ConcurrentDictionary<string,string>();
    public void Initialize(GeneratorInitializationContext context)
    {
        
        
         
#if DEBUG
        if (!Debugger.IsAttached)
        {
           // Debugger.Launch();
        }
#endif
        context.RegisterForSyntaxNotifications(() => new AttributeSyntaxReceiver<GenerateServiceAttribute>());



    }
    public void OnAdditionalFilesChanged(AdditionalFileAnalysisContext context)
    {
        // determine which file changed, and if it affects this generator
        // regenerate only the parts that are affected by this change.
    }
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not AttributeSyntaxReceiver<GenerateServiceAttribute> syntaxReceiver)
        {
            return;
        }

        GeneratorContext generatorContext = new GeneratorContext();

        foreach (var classSyntax in syntaxReceiver.Classes)
        {
            // Converting the class to semantic model to access much more meaningful data.
            var model = context.Compilation.GetSemanticModel(classSyntax.SyntaxTree);
            // Parse to declared symbol, so you can access each part of code separately, such as interfaces, methods, members, contructor parameters etc.
            var symbol = model.GetDeclaredSymbol(classSyntax);

            ClassDefinition classDefinition = new ClassDefinition( classSyntax, syntaxReceiver.ClassAttributes[classSyntax.Identifier.Text]);
            classDefinition.Load();

            var filesDto = DTOService.Process(classDefinition, generatorContext);

            var filesRepo = RepositoryService.Process(classDefinition, generatorContext);

        }
        foreach (var interfaceSyntax in syntaxReceiver.Interfaces)
        {
            // Converting the class to semantic model to access much more meaningful data.
            //var model = context.Compilation.GetSemanticModel(interfaceSyntax.SyntaxTree);
            //// Parse to declared symbol, so you can access each part of code separately, such as interfaces, methods, members, contructor parameters etc.
            //var symbol = model.GetDeclaredSymbol(interfaceSyntax);
            string interfaceName = interfaceSyntax.Identifier.Text;
            InterfaceDefinition interfaceDefinition = new InterfaceDefinition( interfaceSyntax,
                syntaxReceiver.InterfaceAttributes[interfaceName],
                syntaxReceiver.InterfaceMethods[interfaceName]);



            var filesCQRS = CQRSService.Process(interfaceDefinition, generatorContext);

            var filesController = ControllerService.Process(interfaceDefinition, generatorContext);
        }


        // Generating global namespaces
        var result = GlobalUsingService.Process(generatorContext);

        var resultProfiles = MapperProfileService.Process(generatorContext);

        var resultDepInj = DependencyRegistrationService.Process(generatorContext);

    }

    #region Unused Code
    [Obsolete("Source Generator: This method is not in use.")]
    private void AddSourceCodeToCompilation(GeneratorExecutionContext context, INamedTypeSymbol symbol, string templateParameter,
        ClassDefinition classDefinition)
    {
        // Can't access embeded resource of main project.
        // So overridden template must be marked as Analyzer Additional File to be able to be accessed by an analyzer.
        var overridenTemplate = templateParameter != null ?
            context.AdditionalFiles.FirstOrDefault(x => x.Path.EndsWith(templateParameter))?.GetText().ToString() :
            null;

        // Generate the real source code. Pass the template parameter if there is a overriden template.
        var sourceCode = GetSourceCodeFor(symbol, overridenTemplate);

        string nameofSourceFile = $"{templateParameter.Replace("Model", symbol.Name).Replace(".txt", "")}.g.cs";
        //$"{symbol.Name}{templateParameter ?? "Controller"}.g.cs",

        context.AddSource(
            nameofSourceFile,
            SourceText.From(sourceCode, Encoding.UTF8));
        var filePath = $"{symbol.Name}{templateParameter ?? "Controller"}.g.cs";


        //var outputFolderPath = context.AdditionalFiles.FirstOrDefault(x => x.Path.EndsWith(templateParameter))?.Path;
        //outputFolderPath = outputFolderPath.Substring(0, outputFolderPath.IndexOf(@"\Templates"));
        //File.WriteAllText($@"{outputFolderPath}\GeneratedCode\" + nameofSourceFile, sourceCode);

    }

    [Obsolete("Source Generator: This method is not in use.")]
    private string GetSourceCodeFor(INamedTypeSymbol symbol, string template = null)
    {
        // If template isn't provieded, use default one from embeded resources.
        template ??= GetEmbededResource("Restarted.Generators.Templates.Default.txt");

        // Can't use scriban at the moment, make it manually for now.
        return template
            .Replace("{{" + nameof(DefaultTemplateParameters.ClassName) + "}}", symbol.Name)
            .Replace("{{" + nameof(DefaultTemplateParameters.Namespace) + "}}", GetNamespaceRecursively(symbol.ContainingNamespace))
            .Replace("{{" + nameof(DefaultTemplateParameters.PrefferredNamespace) + "}}", symbol.ContainingAssembly.Name)
            ;
    }

    [Obsolete("Source Generator: This method is not in use.")]
    private string GetEmbededResource(string path)
    {
        using var stream = GetType().Assembly.GetManifestResourceStream(path);

        using var streamReader = new StreamReader(stream);

        return streamReader.ReadToEnd();
    }
    [Obsolete("Source Generator: This method is not in use.")]
    private string GetNamespaceRecursively(INamespaceSymbol symbol)
    {
        if (symbol.ContainingNamespace == null)
        {
            return symbol.Name;
        }

        return (GetNamespaceRecursively(symbol.ContainingNamespace) + "." + symbol.Name).Trim('.');
    }
    #endregion

}
