using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Restarted.Generators.Processor.Interfaces
{
    internal interface ITemplateProcessor
    {

        event EventHandler BeforeProcessing;

        IProcessorResult Process(ICodeTemplate codeTemplate);

        event EventHandler AfterProcessing;
            
       

    }
}
