using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Models.Enums
{
    
    public enum MethodTypes
    {
        Commands,
        Queries
    }
    public enum ResultTypes
    {
        Object,
        Scalar
    }
    public enum PresetType
    {
        
        DTO,
        Controller,
        Repository,
        CQRS,
        DI,
        Mappers,
        GlobalUsings
    }
}
