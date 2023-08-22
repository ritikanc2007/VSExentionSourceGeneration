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
        public PathNameSpaceInfo(TypeOfCode typeOfCode, string path, string nameSpace, string fullPath)
        {
            TypeOfCode=typeOfCode;
            Path=path;
            NameSpace=nameSpace;
            FullPath=fullPath;
        }

        public TypeOfCode TypeOfCode { get; set; }

        public string Path { get; set; }

        public string NameSpace { get; set; }

        public string FullPath { get; set; }

        public string Convention { get; set; }

    }
}
