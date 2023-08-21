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

namespace Restarted.Generators.FeatureProcessors.GlobalUsing
{
    internal static class GlobalUsingService
    {

        public static List<string> Process(GeneratorContext generatorContext)
        {

        //       public enum TypeOfCode
        //{
        //    Controller,
        //    CQRSActions,
        //    Repository,
        //    RepositoryInterface,
        //    DTO
        //}
        List<string> files = new();

            Dictionary<string, TypeOfCode[]> ApplicationPaths = new();
            // define which Project needs which namespaes , e.g. Controllers Project need CQRS namespaces and DTOs used in CQRS actions and responses
            // MOVE this in CONFIG
            ApplicationPaths.Add(FolderPath.CONTROLLERS_USING_PATH, new TypeOfCode[]  { TypeOfCode.DTO,TypeOfCode.CQRSActions });
            ApplicationPaths.Add(FolderPath.APPLICATION_USING_PATH, new TypeOfCode[] { TypeOfCode.DTO, TypeOfCode.Repository,TypeOfCode.RepositoryInterface });
            ApplicationPaths.Add(FolderPath.INFRASTRUCTURE_USING_PATH, new TypeOfCode[] { TypeOfCode.DTO, TypeOfCode.RepositoryInterface });

           

            foreach (var genPathConst in ApplicationPaths)
            {

                TypeOfCode[] typeOfCodes= genPathConst.Value;

                HashSet<string> uniqueNamespaces = new HashSet<string>();
                string[] namespaes = generatorContext.NameSpaces.Where(o => typeOfCodes.Contains(o.TypeOfCode)).Select(o => o.Namespace).ToArray(); ;

                foreach (var item in namespaes)
                {
                    uniqueNamespaces.Add(item);
                }
               


                // Generate Folder Path and namespaces
                FolderAndNamespacePath fileInfo = FileService.GenerateFolderPathAndNamespace(genPathConst.Key,"", "","");

                // Generate FileName
                string fileName = "GlobalUsing";

                // Transform Template
                var result = ProcessGlobalUsingTemplate(fileName, uniqueNamespaces);

                // 
                string pathGenerated = FileService.GenerateSourceAtFolderLocation(fileInfo.FolderPath, fileName, result.SourceCode);

                files.Add(pathGenerated);

            }

            return files;


        }
        private static IProcessorResult ProcessGlobalUsingTemplate(string sourceFileName, HashSet<string> uniqueNamespaces)
        {
            ITemplateParameter parameter = new GlobalUsingTemplateParameter(sourceFileName, uniqueNamespaces);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new GlobalUsingTemplate(parameter);
            return processor.Process(codeTemplate);
        }
    }
}
