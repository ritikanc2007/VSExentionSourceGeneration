using Restarted.Generators.Definitions;
using Restarted.Generators.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Common.Context
{
    public class GeneratorContext
    {

        //public Dictionary<string, TemplateContext> TemplateContexts { get; set; } = new();


        //public Dictionary<string, string> DependentTypes{ get; set; } = new();


        public Dictionary<string, string> MapperProfiles { get; set; } = new();




        public Dictionary<string, string> DependencyRepositories { get; set; } = new();

        public List<NameSpaceInfo> NameSpaces { get; set; } = new();



    }

    public enum TypeOfCode
    {
        Controller,
        CQRSActions,
        Repository,
        RepositoryInterface,
        DTO,
        DI,
        Mapper,
        ControllersRootPath,
        ApplicationRootPath,
        InfrastructureRootPath,
        ContractsRootPath
    }
    public class NameSpaceInfo
    {
        public NameSpaceInfo(TypeOfCode typeOfCode, string className, string @namespace, string filePath)
        {
            TypeOfCode=typeOfCode;
            ClassName=className;
            Namespace=@namespace;
            FilePath=filePath;
        }

        public TypeOfCode TypeOfCode { get; set; }
        public string ClassName { get; set; }
        public string Namespace { get; set; }

        public string FilePath { get; set; }
    }
}
