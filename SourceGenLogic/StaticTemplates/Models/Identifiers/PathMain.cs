using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Models.Identifiers
{
    public class PathMain
    {
        public PathMain(string main, string test)
        {
            Main = main;
            Test = test;
        }

        public string Main { get; set; }

        public string Test { get; set; }
    }
}
