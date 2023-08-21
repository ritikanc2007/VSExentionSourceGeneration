using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolWindow.Utility;

namespace WinFormsApp1.DynamicForm
{
    internal static class DependentActions
    {
        public static void BasePathDependentAction(TextBox eventControl, List<TextBox> dependentControls)
        {
            eventControl.TextChanged += (sender, e) => { 
            
                foreach (var control in dependentControls)
                {
                    
                    string[] controlNameSplit = control.Name.Split('_');
                    string controlName = controlNameSplit[controlNameSplit.Length-1];
                    if (control.Text =="")
                        control.Text = eventControl.Text+ @$"\{controlName}";
                }

            };
        }

        public static void EntityPluralNameAction(TextBox eventControl, List<TextBox> dependentControls)
        {
            eventControl.TextChanged += (sender, e) => {

                foreach (var control in dependentControls)
                {
                    if (control.Name.Contains("Plural"))
                        control.Text = CommonHelper.GetPluralName(eventControl.Text);
                    else
                        control.Text = eventControl.Text;
                }
                
            };
        }
    }
}
