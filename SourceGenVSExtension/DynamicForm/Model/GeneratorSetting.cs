using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolWindow.DynamicForm.Model;

namespace WinFormsApp1.DynamicForm.Model
{

    public enum ControlType
    {
        text,
        CheckBox,
        FileDialog,
        textArea,
        CQRSControl
    }
    public class GeneratorSetting
    {    public GeneratorSetting()
        {
            
        }
        public GeneratorSetting(string name, string label, string value, ControlType controlType,
            bool isEventControl = false, bool isDependentControl = true, string eventMethod="",
            string qualifiedName = null)
        {
            Name = name;
            Value = value;
            ControlType = controlType;
            LabelText = label;
            ControlId = generateControlId();
            IsEventControl = isEventControl;
            EventMethod = eventMethod;
            IsDependentControl = isDependentControl;
            QualifiedName = qualifiedName;
        }
        //public GeneratorSetting(string name, string label, TextBox eventControl, List<TextBox> dependentControls, string Value = null)
        //{
        //    LabelText = label;
        //    Name = name;
        //    ControlType = ControlType.text;
        //    ControlId = generateControlId();
        //    EventControl = eventControl;
        //    DependentControls = dependentControls;
        //}

        public string Name { get; set; }

        public string Value { get; set; }

        public string LabelText { get; set; }
        public ControlType ControlType { get; set; }

        public string ControlId { get; set; }

        public  bool IsEventControl { get; set; }
        public bool IsDependentControl { get; set; } = true;
        public string EventMethod { get; set; }
        [JsonIgnore]
        public Control EventControl { get; set; }
        [JsonIgnore]
        public List<TextBox> DependentControls { get; set; }

        public string QualifiedName { get; set; }
        public ControllerSetting ContollerSetting { get; set; } = new ControllerSetting();

        private string generateControlId()
        {
            if (ControlType == ControlType.text)
            {
                return "ctrltxt_" + Name;
            }
            else if (ControlType == ControlType.CheckBox)
            {
                return "ctrlchk_" + Name;
            }
            else if (ControlType == ControlType.textArea)
            {
                return "ctrltxtarea_" + Name;
            }
            else if (ControlType == ControlType.FileDialog)
            {
                return "ctrltxtFile_" + Name;
            }

            return null;
        }
    }
}
