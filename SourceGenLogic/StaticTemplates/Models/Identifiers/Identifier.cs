using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Models.Identifiers
{
    public class Identifier
    {
        public Identifier(string uniqueId, PathMain path, PathMain partialPath)
        {
            UniqueId = uniqueId;
            Path = path;
            PartialPath = partialPath;
        }

        public string UniqueId { get; set; }

        public PathMain Path { get; set; }

        private PathMain PartialPath { get; set; }
    }
}
