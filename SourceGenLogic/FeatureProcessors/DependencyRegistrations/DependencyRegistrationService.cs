using Restarted.Generators.Common.Configurations;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Definitions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.Generators.Dependencies;
using Restarted.Generators.Processor;
using Restarted.Generators.Processor.Interfaces;

namespace Restarted.Generators.FeatureProcessors.DependencyRegistrations
{
    internal static class DependencyRegistrationService
    {

        public static List<string> Process(GeneratorContext generatorContext)
        {


            List<string> files = new();

            string path = GeneratorConfigurations.GeneratorSetting().GetSetting("DependencyRegistrationProfile").GenerationPath;

            // Generate Folder Path and namespaces
            FolderAndNamespacePath fileInfo = FileService.GenerateFolderPathAndNamespace(path, "", "", "");

            // Generate FileName
            string fileName = "DependencyInjectionRegisterAuto";

            // Transform Template
            var result = ProcessDependencyRegistrationTemplate(fileInfo.NameSpacePath, fileName, generatorContext.DependencyRepositories);

            // 
            string pathGenerated = FileService.GenerateSourceAtFolderLocation(fileInfo.FolderPath, fileName, result.SourceCode);

            files.Add(pathGenerated);



            return files;


        }
        private static IProcessorResult ProcessDependencyRegistrationTemplate(string nameSpace, string sourceFileName, Dictionary<string, string> targetSourceMap)
        {
            ITemplateParameter parameter = new DependencyRegistrationTemplateParameter(nameSpace, sourceFileName, targetSourceMap);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new DependencyRegistrationTemplate(parameter);
            return processor.Process(codeTemplate);
        }
    }
}
