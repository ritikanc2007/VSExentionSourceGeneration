using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.FeatureProcessors.Models
{
 
    internal class ProcessResult
    {
        public ProcessResult(string fileName, string sourceCode)
        {

            FileName = fileName;
            SourceCode = sourceCode;
        }


        public string FileName { get; set; }
        public string SourceCode { get; set; }
    }
}
