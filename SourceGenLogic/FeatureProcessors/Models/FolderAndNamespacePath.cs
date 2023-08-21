using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Definitions
{
    public class FolderAndNamespacePath {
        public FolderAndNamespacePath(string folderPath, string nameSpacePath) { 

            this.FolderPath = folderPath;
            this.NameSpacePath = nameSpacePath;
        }

        public string FolderPath { get; set; }
        public string NameSpacePath { get; set; }
    };

  
}
