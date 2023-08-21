using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Processor
{
    internal enum ProcessorType
    {
        Default,
        Useless
    }
    internal static class ProcessorFactory
    {
        public static ITemplateProcessor Get(ProcessorType type)
        {
            switch (type) { 
            case ProcessorType.Default:
                    return new TemplateProcessor();
               
                default: return null;
            }
        }
    }
}
