using Restarted.Generators.Common.Configurations;
using Restarted.Generators.Common.Context;
using Restarted.Generators.Definitions;
using Restarted.Generators.Definitions.Extensions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Generators.Repositories.Service.Models;
using Restarted.Generators.Generators.Repositories.Service.Template;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Restarted.Generators.FeatureProcessors.Models;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Models.Metadata;

namespace Restarted.Generators.FeatureProcessors.Repository
{
    public static class RepositoryService
    {

        private const string ConstGeneratorAttribute = "GeneratorRepository";
        private const string ConstFeatureName = "featureName";
        private const string ConstFeatureModuleName = "featureModuleName";
        private const string ConstGenerationPath = "generationPath";
        private const string ConstConvention = "convention";
        private const string ConstDtoName = "dtoName";
        private const string ConstDbContextName = "dbContextName";
        private const string ConstPluralEntityName = "PluralEntityName";

        public static List<string> Process(TypeDefinitionInfo typeDefinitionInfo, GeneratorContext generatorContext, AttributeMetaDataRepo data)
        {

            List<string> files = new List<string>();


            for (int i = 0; i <= 1; i++) // running it twice for repository & Irepository
            {
                bool isRepositoryProcessing = i== 1;

                string NameSpacePath = data.NameSpace["Repository"];  //data.NameSpace

                string sourcefileName = isRepositoryProcessing ? data.RepositoryName : data.InterFaceName;


                string generationPath = data.GenerationPaths["Repository"];



                IProcessorResult result = null;
                if (isRepositoryProcessing)
                {
                    result= ProcessRepository(typeDefinitionInfo, NameSpacePath, sourcefileName, data.DTOName, data.EntityName, data.DatabaseContextName, data.PluralEntityName);
                }
                else
                {
                    NameSpacePath = data.NameSpace["Interface"];
                    generationPath = data.GenerationPaths["Interface"];
                    result= ProcessInterface(typeDefinitionInfo, NameSpacePath, sourcefileName, data.DTOName, data.EntityName, data.DatabaseContextName, data.PluralEntityName);
                }

                string typeName = isRepositoryProcessing ? "Repository" : "Interface";
                string convPath = string.Empty;
                if (data.PathConventions.ContainsKey(typeName))
                {
                    convPath = data.PathConventions[typeName].ConventionPath;
                }


                var finalReplacedPath = FileService.ConventionBasedPath(convPath, data.PathConvention.FeatureName, data.PathConvention.FeatureModuleName, "");


                string pathGenerated = FileService.GenerateSourceAtFolderLocation(finalReplacedPath, sourcefileName, result.SourceCode);

                files.Add(pathGenerated);



                if (isRepositoryProcessing)
                    generatorContext.NameSpaces.Add(new NameSpaceInfo(TypeOfCode.Repository, sourcefileName, NameSpacePath, generationPath));
                else
                    generatorContext.NameSpaces.Add(new NameSpaceInfo(TypeOfCode.RepositoryInterface, sourcefileName, NameSpacePath, generationPath));

            }
            generatorContext.DependencyRepositories.Add(data.InterFaceName, data.RepositoryName);



            return files;


        }

        private static IProcessorResult ProcessRepository(TypeDefinitionInfo typeDefinitionInfo, string nameSpace, string sourceFileName, string dtoName, string className, string dbContextName, string pluralEntityName)
        {
            ITemplateParameter parameter = new RepositoryTemplateParameter(typeDefinitionInfo, nameSpace, sourceFileName, dtoName, className, pluralEntityName, dbContextName);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new ModelsRepository(parameter);
            return processor.Process(codeTemplate);
        }

        private static IProcessorResult ProcessInterface(TypeDefinitionInfo typeDefinitionInfo, string nameSpace, string sourceFileName, string dtoName, string className, string dbContextName, string pluralEntityName)
        {
            ITemplateParameter parameter = new RepositoryTemplateParameter(typeDefinitionInfo, nameSpace, sourceFileName, dtoName, className, pluralEntityName, dbContextName);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new IModelsRepository(parameter);
            return processor.Process(codeTemplate);
        }




    }
    public class AttributeMetaDataRepo : AttributeMetaData
    {
        public string DTOName { get; set; }

        public string DatabaseContextName { get; set; }

        public string EntityName { get; set; }

        public string PluralEntityName { get; set; }
        public string RepositoryName { get; set; }
        public string InterFaceName { get; set; }

        public Dictionary<string, string> GenerationPaths { get; set; }

        public Dictionary<string, string> NameSpace { get; set; }
        public Dictionary<string, FileParamInfo> PathConventions { get; set; }
    }

}