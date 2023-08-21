using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolWindow.Models
{
    public class ProjectFile
    {
        public ProjectFile(string name, string itemType)
        {
            Name=name;
            ItemType=itemType;

            Projects = new List<ProjectFile>();
        }
        public ProjectFile(string name, string itemType, string path, string parentFolderPath)
        {
            Name=name;
            ItemType=itemType;
            Path=path;
            Projects = new List<ProjectFile>();
            ParentFolderPath=parentFolderPath;
        }
        public string Name { get; set; }

        public string ItemType { get; set; }

        public string Path { get; set; }

        public string ParentFolderPath { get; set; }
        public List<ProjectFile> Projects { get; set; }
    }
}
