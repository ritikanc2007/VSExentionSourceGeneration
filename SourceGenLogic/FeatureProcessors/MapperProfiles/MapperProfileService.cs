using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using Restarted.Generators.Generators.GlobalUsings;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Definitions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.Common.Configurations;
using System.Collections.Immutable;
using Restarted.Generators.Generators.MapperProfiles;

namespace Restarted.Generators.FeatureProcessors.MapperProfiles
{
    internal static class MapperProfileService
    {

        public static List<string> Process(GeneratorContext generatorContext)
        {


            List<string> files = new();

            string path = GeneratorConfigurations.GeneratorSetting().GetSetting("MapperProfile").GenerationPath;

            // Generate Folder Path and namespaces
            FolderAndNamespacePath fileInfo = FileService.GenerateFolderPathAndNamespace(path, "", "", "");

            // Generate FileName
            string fileName = "MapperProfilesAuto";

            // Transform Template
            var result = ProcessMapperProfileTemplate(fileInfo.NameSpacePath, fileName, generatorContext.MapperProfiles);

            // 
            string pathGenerated = FileService.GenerateSourceAtFolderLocation(fileInfo.FolderPath, fileName, result.SourceCode);

            files.Add(pathGenerated);



            return files;


        }
        private static IProcessorResult ProcessMapperProfileTemplate(string nameSpace, string sourceFileName, Dictionary<string,string> targetSourceMap)
        {
            ITemplateParameter parameter = new MapperProfileTemplateParameter(nameSpace, sourceFileName, targetSourceMap);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new MapperProfileTemplate(parameter);
            return processor.Process(codeTemplate);
        }
    }
}
