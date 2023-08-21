using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Processor.Interfaces
{
    public interface ITemplateParameter
    {
        //string TemplateName { get; set; }
        //string PreferredNameSpace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string SourceFileName { get; set; }

        string PreferredNameSpace { get; set; }
    }
}
