﻿using Microsoft.CodeAnalysis;
using Restarted.Generators.Definitions;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Generators.DTO.Template;
using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using Restarted.Generators.Common.Configurations;
using Restarted.Generators.Definitions.Extensions;
using Restarted.Generators.FeatureProcessors.Common;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.Common.Context;
using System.Reflection.Metadata;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Models.Metadata;

namespace Restarted.Generators.FeatureProcessors.DTO
{
    public static class DTOService
    {


        public static List<string> Process(TypeDefinitionInfo typeDefinitionInfo, GeneratorContext generatorContext, AttributeMetaDataDto data = null)
        {

            List<string> files = new List<string>();

            string SourceFileNAme = data.Name;

            string NameSpacePath = data.NameSpace;

            // Transform Template
            var result = ProcessDTOTemplate(typeDefinitionInfo, NameSpacePath, SourceFileNAme, data.Members, bool.Parse(data.AllPropertiesNullable));

            var finalReplacedPath = FileService.ConventionBasedPath(data.PathConvention.ConventionPath, data.PathConvention.FeatureName, data.PathConvention.FeatureModuleName, "");


            // Generate FileName
            string fileName = SourceFileNAme;
            // 
            string pathGenerated = FileService.GenerateSourceAtFolderLocation(finalReplacedPath, fileName, result.SourceCode);

            files.Add(pathGenerated);

            //GENERATORCONTEXT Adding namesapces
            generatorContext.NameSpaces.Add(new NameSpaceInfo(TypeOfCode.DTO, SourceFileNAme, NameSpacePath, finalReplacedPath));
            // Key Contains Targe & value contains source i.e UserDTO is Key & User is value
            // Reason: you can have multiple DTOs for User entity
            generatorContext.MapperProfiles.Add(SourceFileNAme, typeDefinitionInfo.Name);

            return files;



        }


        private static IProcessorResult ProcessDTOTemplate(TypeDefinitionInfo typeDefinitionInfo, string nameSpace, string className, string commanSeperatedMembers, bool allPropertiesNullable)
        {
            ITemplateParameter parameter = new ModelTemplateParameter(typeDefinitionInfo, nameSpace, className, commanSeperatedMembers, allPropertiesNullable);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new ModelDTO(parameter);
            return processor.Process(codeTemplate);
        }



    }

    public class AttributeMetaDataDto : AttributeMetaData
    {
        public string AllPropertiesNullable { get; set; }

        public string Name { get; set; }
        public string Convention { get; set; }

        public string Members { get; set; }

        public string NameSpace { get; set; }

    }
}
