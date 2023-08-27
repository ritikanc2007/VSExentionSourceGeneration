using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolWindow.DynamicForm.Model
{
    public class ControllerSetting
    {
        public ControllerSetting()
        {
            
        }

        public ControllerSetting(string qualifiedName, string hTTPAction, string methodName, string route)
        {
            HTTPAction=hTTPAction;
            MethodName=methodName;
            QualifiedName=qualifiedName;
            Route=route;
        }

        public string HTTPAction { get; set; }
        public string QualifiedName { get; set; }

        public string MethodName { get; set; }
        public string Route  { get; set; }
    }
}
