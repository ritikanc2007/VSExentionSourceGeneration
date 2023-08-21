using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.BaseModels
{
    internal class NameValue
    {
        public NameValue(string name, string value)
        {
            Name=name;
            Value=value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
