using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.BaseModels
{
    public class NameType
    {
        public NameType(string name, string type)
        {
            Name=name;
            Type=type;
        }

        public string Name { get; set; }
     
        public string Type { get; set; }
    }
}
