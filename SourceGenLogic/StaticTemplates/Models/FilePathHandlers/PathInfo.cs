using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Models.FilePathHandlers
{
    public class PathInfo
    {
        public PathInfo(string main, string partial, string unitTestMain, string unitTestPartial)
        {
            Main = main;
            Partial = partial;
            UnitTestMain = unitTestMain;
            UnitTestPartial = unitTestPartial;
        }

        public string Main { get; set; }

        public string Partial { get; set; }

        public string UnitTestMain { get; set; }

        public string UnitTestPartial { get; set; }
    }
}
