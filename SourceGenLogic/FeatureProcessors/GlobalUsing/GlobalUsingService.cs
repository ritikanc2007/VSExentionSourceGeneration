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
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.FeatureProcessors.Process;

namespace Restarted.Generators.FeatureProcessors.GlobalUsing
{
    public static class GlobalUsingService
    {

        public static List<string> Process(GeneratorContext generatorContext, AttributeMetaDataGlobalUsing data)
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

            string controllerRootPath = data.RootProjectPaths.ContainsKey(TypeOfCode.ControllersRootPath) ? data.RootProjectPaths[TypeOfCode.ControllersRootPath] :null;
            string applicationRootPath = data.RootProjectPaths.ContainsKey(TypeOfCode.ApplicationRootPath) ? data.RootProjectPaths[TypeOfCode.ApplicationRootPath] : null;
            string infrastructureRootPath = data.RootProjectPaths.ContainsKey(TypeOfCode.InfrastructureRootPath) ? data.RootProjectPaths[TypeOfCode.InfrastructureRootPath] : null;
            string contractsRootPath = data.RootProjectPaths.ContainsKey(TypeOfCode.ContractsRootPath) ? data.RootProjectPaths[TypeOfCode.ContractsRootPath] : null;


            ApplicationPaths.Add(controllerRootPath, new TypeOfCode[]  { TypeOfCode.DTO,TypeOfCode.CQRSActions });
            ApplicationPaths.Add(applicationRootPath, new TypeOfCode[] { TypeOfCode.DTO, TypeOfCode.Repository,TypeOfCode.RepositoryInterface });
            ApplicationPaths.Add(infrastructureRootPath, new TypeOfCode[] { TypeOfCode.DTO, TypeOfCode.RepositoryInterface , TypeOfCode.Repository });
            ApplicationPaths.Add(contractsRootPath, new TypeOfCode[] { TypeOfCode.DTO });



            foreach (var genPathConst in ApplicationPaths)
            {

                TypeOfCode[] typeOfCodes= genPathConst.Value;

                HashSet<string> uniqueNamespaces = new HashSet<string>();
                string[] namespaes = generatorContext.NameSpaces.Where(o => typeOfCodes.Contains(o.TypeOfCode)).Select(o => o.Namespace).ToArray(); ;

                foreach (var item in namespaes)
                {
                    uniqueNamespaces.Add(item);
                }




                // Generate FileName
                string fileName = data.FileName;
                string finalReplacedPath = genPathConst.Key;
                //FolderAndNamespacePath finalReplacedPath = FileService.ConventionBasedPath("", data?.PathConvention.ConventionPath, data.PathConvention.FeatureName, data.PathConvention.FeatureModuleName, "");

                // Transform Template
                var result = ProcessGlobalUsingTemplate(fileName, uniqueNamespaces);

              
                // 
                string pathGenerated = FileService.GenerateSourceAtFolderLocation(finalReplacedPath, fileName, result.SourceCode);

                files.Add(pathGenerated);

            }

            return files;


        }
        private static IProcessorResult ProcessGlobalUsingTemplate(string sourceFileName, HashSet<string> uniqueNamespaces)
        {
            ITemplateParameter parameter = new GlobalUsingTemplateParameter(sourceFileName, uniqueNamespaces);
            ITemplateProcessor processor = ProcessorFactory.Get(ProcessorType.Default);
            ICodeTemplate codeTemplate = new GlobalUsingTemplate(parameter);
            //return processor.Process(codeTemplate);
            var func = () =>
            {
                if (TemplateToUse.SwitchFlag == TemplateType.T4)
                    return processor.Process(codeTemplate);
                else
                    return StaticTemplateProcessor.Process(TypeOfTemplate.GlobalUsings, parameter);
            };
            IProcessorResult result = func();
            return result;
        }

        public class AttributeMetaDataGlobalUsing : AttributeMetaData
        {
            public string FileName { get; set; }

            public string ProjectName { get; set; }
            public Dictionary<TypeOfCode,string> RootProjectPaths { get; set; }

        }
    }
}
