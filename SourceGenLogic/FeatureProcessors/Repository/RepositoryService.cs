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
        private const string ConstRepository = "Repository";
        private const string ConstInterface = "Interface";

        public static List<string> Process(TypeDefinitionInfo typeDefinitionInfo, GeneratorContext generatorContext, AttributeMetaDataRepo data)
        {

            List<string> files = new List<string>();


            for (int i = 0; i <= 1; i++) // running it twice for repository & Irepository
            {
                bool isRepositoryProcessing = i== 1;
                string sourcefileName = null;// = isRepositoryProcessing ? data.RepositoryName : data.InterFaceName;
                string nameSpacePath = null; //= data.NameSpace[ConstRepository];  //data.NameSpace
                string generationPath; //= data.GenerationPaths[ConstRepository];
                string convPath = string.Empty;
                FolderAndNamespacePath finalReplacedPath = null;
                string typeName = isRepositoryProcessing ? ConstRepository : ConstInterface;

                sourcefileName = isRepositoryProcessing ? data.RepositoryName : data.InterFaceName;
                if (data.PathConventions.ContainsKey(typeName))
                    convPath = data.PathConventions[typeName].ConventionPath;

                if (data.NameSpace.ContainsKey(typeName))
                    nameSpacePath = data.NameSpace[typeName];



                if (data.GenerationPaths.ContainsKey(typeName))
                    generationPath = data.GenerationPaths[typeName];

                finalReplacedPath= FileService.ConventionBasedPath(nameSpacePath, convPath, data.PathConvention.FeatureName, data.PathConvention.FeatureModuleName, "");

                IProcessorResult result = null;

                if (isRepositoryProcessing)
                result= ProcessRepository(typeDefinitionInfo, finalReplacedPath.NameSpacePath, sourcefileName, data.DTOName, data.EntityName, data.DatabaseContextName, data.PluralEntityName, null);
                else
                result= ProcessInterface(typeDefinitionInfo, finalReplacedPath.NameSpacePath, sourcefileName, data.DTOName, data.EntityName, data.DatabaseContextName, data.PluralEntityName);


                FileService.GenerateSourceAtFolderLocation(finalReplacedPath.FolderPath, sourcefileName, result.SourceCode);

                files.Add(finalReplacedPath.FolderPath);


                TypeOfCode typeofCode = isRepositoryProcessing ? TypeOfCode.Repository : TypeOfCode.RepositoryInterface;

                generatorContext.NameSpaces.Add(new NameSpaceInfo(typeofCode, sourcefileName, finalReplacedPath.NameSpacePath, finalReplacedPath.FolderPath));


            }
            if (!generatorContext.DependencyRepositories.ContainsKey(data.InterFaceName))
            generatorContext.DependencyRepositories.Add(data.InterFaceName, data.RepositoryName);



            return files;


        }

        private static IProcessorResult ProcessRepository(TypeDefinitionInfo typeDefinitionInfo, string nameSpace, string sourceFileName, string dtoName, string className, string dbContextName, string pluralEntityName,string includes)
        {
            ITemplateParameter parameter = new RepositoryTemplateParameter(typeDefinitionInfo, nameSpace, sourceFileName, dtoName, className, pluralEntityName, dbContextName, includes);
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

        public string Includes { get; set; }
        public string PluralEntityName { get; set; }
        public string RepositoryName { get; set; }
        public string InterFaceName { get; set; }

        public Dictionary<string, string> GenerationPaths { get; set; }

        public Dictionary<string, string> NameSpace { get; set; }
        public Dictionary<string, string> ProjectName { get; set; }
        public Dictionary<string, FileParamInfo> PathConventions { get; set; }
    }

}