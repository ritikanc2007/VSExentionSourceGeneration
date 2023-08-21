using Restarted.Generators.Processor.Interfaces;
using Restarted.Generators.Processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Processor.Providers
{
    internal class TemplateProcessor : ITemplateProcessor
    {
        public event EventHandler BeforeProcessing;
        public event EventHandler AfterProcessing;
        
    
        public IProcessorResult Process(ICodeTemplate codeTemplate)
        {
            //this.BeforeProcessing.Invoke(this,new BeforeEventArgs(codeTemplate );
            var result = codeTemplate.Process();
            return new ProcessorResult() { SourceCode = result.SourceCode };
        }

    }

    internal class BeforeEventArgs : EventArgs
    {
        public BeforeEventArgs(ITemplateParameter templateParameter)
        {
            this.Parameter = templateParameter;
        }
        public ITemplateParameter Parameter { get; set; }
    }

    internal class AfterEventArgs : EventArgs
    {
        public AfterEventArgs(ProcessorResult result)
        {
            this.Result = result;
        }
        public ProcessorResult Result { get; set; }
    }
}
