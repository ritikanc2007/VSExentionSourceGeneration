using Restarted.Generators.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Processor.Models
{
    public class ProcessorResult : IProcessorResult
    {
        public string SourceCode { get; set; }
    }
}
