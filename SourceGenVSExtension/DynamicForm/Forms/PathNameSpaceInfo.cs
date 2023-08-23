using Restarted.Generators.Common.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ToolWindow.DynamicForm.Forms
{
    public class PathNameSpaceInfo
    {
        public PathNameSpaceInfo(TypeOfCode typeOfCode, string path,string projectName, string fullPath)
        {
            TypeOfCode=typeOfCode;
            Path=path;
            ProjectName=projectName;
            FullPath=fullPath;
        }

        public TypeOfCode TypeOfCode { get; set; }

        public string Path { get; set; }

        

        string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set
            {

                projectName = value;
                SetGenerateNamespace(projectName, Convention);
            }
        }
        public string NameSpace { get; set; }

        public string FullPath { get; set; }

        string convention;
        public string Convention { get { return convention; } set {

                convention = value;
                SetGenerateNamespace(ProjectName, convention);
            }
        }
        private  void SetGenerateNamespace(string projectName, string folderPath)
        {
            if (!string.IsNullOrEmpty(projectName) && !string.IsNullOrEmpty(folderPath))
            {
                string nameSpace;
                var projIndex = folderPath.IndexOf(projectName, StringComparison.OrdinalIgnoreCase);
                nameSpace = folderPath.Substring(projIndex).Replace("\\", ".");
                if (nameSpace.Substring(nameSpace.Length-1, 1) ==".") // remove last dot
                    nameSpace = nameSpace.Substring(0, nameSpace.Length-1);
                this.NameSpace = nameSpace;
            }
        }
    }
}
