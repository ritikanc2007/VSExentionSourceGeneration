using Source.Generator.TemplateManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Generator.TemplateManager.Models.Identifiers
{
    public class TemplateMethodBase
    {
        public Identifier Identifier { get; set; }
        public TemplateMethodBase(string name, ResultTypes resultType)
        {
            Name = name;
            ResultType = resultType;
        }

        public string Name { get; set; }
        public ResultTypes ResultType { get; set; }


    }
}
