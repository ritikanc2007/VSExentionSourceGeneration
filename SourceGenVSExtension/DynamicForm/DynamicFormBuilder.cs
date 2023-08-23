using Community.VisualStudio.Toolkit;
using SourceGeneratorParser.Models.Metadata;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ToolWindow.DynamicForm.Model;
using WinFormsApp1.DynamicForm.CustomControl;
using WinFormsApp1.DynamicForm.Model;

namespace WinFormsApp1.DynamicForm
{
    internal class DynamicFormBuilder
    {


        private FolderBrowserDialog openFolderDialog;
        public Form? form;

        TypeDefinitionInfo typeDefinitionInfo;
        List<GeneratorSetting> settings;

        string initialFolderPath;

        public DynamicFormBuilder()
        {
            
        }
        public List<GeneratorSetting> Generate(TypeDefinitionInfo typeDefinitionInfo, string SelectedMenuAction, string initialFolderDirectory, List<GeneratorSetting> savedSettings = null)
        {
            initialFolderPath = initialFolderDirectory;
            if (SelectedMenuAction == "DTO")
                settings = SettingHelper.GetGenerateDTOSettings(typeDefinitionInfo, initialFolderDirectory);
            if (SelectedMenuAction == "REPO")
                settings = SettingHelper.GetGenerateRepositorySettings(typeDefinitionInfo, initialFolderDirectory);
            if (SelectedMenuAction == "PATHS")
            {
                if (savedSettings !=null)
                    settings = savedSettings;
                else
                {
                    settings = new List<GeneratorSetting>();
                    settings = SettingHelper.GetPathSettings(typeDefinitionInfo, initialFolderDirectory);
                }
            }
            if (SelectedMenuAction == "CONVENTIONS")
            {
                if (savedSettings !=null)
                    settings = savedSettings;
                else
                {
                    settings = new List<GeneratorSetting>();
                    settings = SettingHelper.GetConventionSettings(typeDefinitionInfo, initialFolderDirectory);
                }
            }
            if (SelectedMenuAction == "CQRS")
                settings = SettingHelper.GetGenerateCQRSSettings(typeDefinitionInfo, initialFolderDirectory);
            else if (SelectedMenuAction == "CTRL")
            {
                //settings = SettingHelper.GetGenerateCTRLSettings(typeDefinitionInfo, initialFolderDirectory);

                settings= SettingHelper.GetGenerateControllerWithRepoSettings(typeDefinitionInfo, initialFolderDirectory);
            }

            if (settings == null) return null;
            form = new Form();
            form.Location = new Point(300, 200);

           

            //form.Width = 700;
            //form.Height = 800;

            //form.Width = 700;
            //form.Height = 800;
            form.SuspendLayout();
            form.AutoSize=true;
            int usedHeight = 20;


            foreach (GeneratorSetting kvp in settings)
            {
                if (kvp.ControlType == ControlType.text || kvp.ControlType == ControlType.textArea)
                {
                    AddLabelWithTextBox(form, ref usedHeight, kvp);

                }
                if (kvp.ControlType == ControlType.CheckBox)
                {
                    AddCheckBox(form, ref usedHeight, kvp);
                }
                if (kvp.ControlType == ControlType.FileDialog)
                {
                    AddFolderPathDialog(form, ref usedHeight, kvp);
                }
                if (kvp.ControlType == ControlType.CQRSControl)
                {
                    ;
                    // foreach (var item in Methods)
                    //{

                    MethodItemInfo methodItemInfo = typeDefinitionInfo.Methods.Where(o => o.Name == kvp.Name).FirstOrDefault();
                    string route = "";
                    string action = "GET";
                    if (methodItemInfo != null)
                    {
                        string[] Getkeywords = { "Get", "Find", "Exist", "Duplicate", "Validate" };
                        string[] Createkeywords = { "Create", "Add", "Save", "Insert" };
                        string[] Updatekeywords = { "Save", "Update", "Modiry", "Change", };
                        string[] Deletekeywords = { "Delete", "Remove",  };

                        if (Getkeywords.Any(g => methodItemInfo.Name.Contains(g)))
                        {
                            action="GET";
                            if (methodItemInfo.Arguments.Count == 1)
                            {
                                var arg = methodItemInfo.Arguments[0];
                                bool allPremitive = false; // methodItemInfo.Arguments.All(o => Type.GetType(o.Type).IsPrimitive);
                                if (allPremitive)
                                    route= $"{methodItemInfo.Name}/{{{arg.Name}}}";
                                else
                                    route= $"{methodItemInfo.Name}";

                            }
                            else
                            {
                                route= $"{methodItemInfo.Name}";
                            }
                        }else if (Createkeywords.Any(g => methodItemInfo.Name.Contains(g)))
                        {
                            action="POST";
                            route="";
                        }
                        else if (Updatekeywords.Any(g => methodItemInfo.Name.Contains(g)))
                        {
                            action="PUT";
                            route="";
                        }
                        else if (Deletekeywords.Any(g => methodItemInfo.Name.Contains(g)))
                        {
                            action="DELETE";
                            route="";
                        } else
                            route= $"{methodItemInfo.Name}";

                    }

                    MethodSettingsControl ctrl = new MethodSettingsControl(kvp.Name,route,action);
                    ctrl.Name= kvp.Name;
                    ctrl.Top = usedHeight + 5;
                    ctrl.Left = 5;
                    form.Controls.Add(ctrl);
                    usedHeight += ctrl.Height + 5;

                    //}
                    //form.Controls.Add(new MethodSettingsControl());
                }

                //textBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;


            }

            // Register dependent action
            RegisterDependentActions();

            AddSaveCancelButtons(form, ref usedHeight);
            form.ResumeLayout();
            //form.Height = usedHeight + 5;
            form.ShowDialog();

            return settings;

        }

        private void RegisterDependentActions()
        {
            var setting = settings.Where(o => o.IsEventControl).FirstOrDefault();
            bool isEventControl = setting != null ? setting.IsEventControl : false;
            if (isEventControl)
            {
                string eventMethod = setting.EventMethod;
                TextBox eventControl = null;
                List<TextBox> Dependents = new List<TextBox>();
                foreach (Control item in form.Controls)
                {
                    var dependentControl = settings.Where(o => o.IsDependentControl && o.ControlId == item.Name).FirstOrDefault();
                    if (item is TextBox || item is MethodSettingsControl)
                    {
                        // check if textbox is primary event control
                        if (item.Name == setting.ControlId)
                            eventControl = (TextBox)item;
                        else
                        {
                            if (item is TextBox)
                                Dependents.Add((TextBox)item);
                            else if (item is MethodSettingsControl)
                                Dependents.Add(((MethodSettingsControl)item).EntityHiddenTextBox);
                        }
                    }
                }

                // call registration method
                Type type = typeof(DependentActions);
                if (type != null)
                {
                    MethodInfo methodInfo = type.GetMethod(eventMethod);
                    object[] parameters = new object[] { eventControl, Dependents };

                    methodInfo.Invoke(type, parameters);
                }
            }

          
        }





        #region Static Methods
        public void AddLabelWithTextBox(Form form, ref int usedHeight, GeneratorSetting setting)
        {

            Label label = new Label();

            label.Text = setting.LabelText;
            label.Name = "lbl_"+setting.ControlId;
            label.Top = usedHeight + 7; label.Left = 5;
            form.Controls.Add(label);

            TextBox textBox = new TextBox();
            if (setting.ControlType == ControlType.textArea)
            {
                textBox.Multiline = true;
                textBox.WordWrap=true;
                textBox.Height = textBox.Height *3;
            }
            textBox.Text = setting.Value;
            textBox.Name = setting.ControlId;
            textBox.Top = usedHeight + 5;
            textBox.Left = 150;
            textBox.Width = 500;



            form.Controls.Add(textBox);

            usedHeight += textBox.Height + 5;

            //Setting control property to capture events
            setting.EventControl = textBox;

        }
        public void AddCheckBox(Form form, ref int usedHeight, GeneratorSetting setting)
        {

            System.Windows.Forms.CheckBox checkBox = new CheckBox();
            checkBox.Text = setting.LabelText;
            checkBox.Checked = bool.Parse(setting.Value);
            checkBox.Name = setting.ControlId;
            checkBox.Top = usedHeight + 5;
            checkBox.Left = 150;
            checkBox.Width = 300;
            form.Controls.Add(checkBox);
            usedHeight += checkBox.Height + 5;

            //Setting control property to capture events
            setting.EventControl = checkBox;
        }

        public void AddFolderPathDialog(Form form, ref int usedHeight, GeneratorSetting setting)
        {

            Label label = new Label();

            label.Text = setting.LabelText;
            label.Top = usedHeight + 7;
            label.Left = 5;
            form.Controls.Add(label);

            TextBox textBox = new TextBox();
            textBox.Text = setting.Value;
            textBox.Name = setting.ControlId;
            textBox.Top = usedHeight + 5;
            textBox.Left = 150;
            textBox.Width = 300;

            form.Controls.Add(textBox);

            Button btnBrowse = new Button();
            btnBrowse.Name = "btn_" + setting.ControlId; ;
            btnBrowse.Text = "Browse";
            btnBrowse.Top = usedHeight + 5;
            btnBrowse.Left = textBox.Right + 5;
            btnBrowse.Width = 100;
            btnBrowse.Click += (sender, eventArgs) =>
            {
                openFolderDialog = new FolderBrowserDialog();
                openFolderDialog.SelectedPath = initialFolderPath;

                // name and age are accessible here!!
                if (openFolderDialog.ShowDialog() == DialogResult.OK)
                    textBox.Text = openFolderDialog.SelectedPath;
            };
            btnBrowse.Height += 10;
            form.Controls.Add(btnBrowse);

            usedHeight += btnBrowse.Height + 5;

            //Setting control property to capture events
            setting.EventControl = textBox;
        }


        public void AddSaveCancelButtons(Form form, ref int usedHeight)
        {
            Button btnSave = new Button();
            btnSave.Name = "btnSave";
            btnSave.Text = "Save";
            btnSave.Top = usedHeight + 5;
            btnSave.Left = 150;
            btnSave.Width = 80;
            btnSave.Height += 10;
            btnSave.Click += (sender, eventArgs) =>
            {
                saveSettings();
                form.Close();
            };
            usedHeight += btnSave.Height + 5;

            form.Controls.Add(btnSave);
            Button btnCancel = new Button();
            btnCancel.Top = btnSave.Top;
            btnCancel.Left = btnSave.Right + 40;
            btnCancel.Width = 80;
            btnCancel.Text = "Cancel";
            btnCancel.Name = "btnCancel";
            btnCancel.Height += 10;
            btnCancel.Click += (sender, eventArgs) =>
            {
                form.Close();
            };
            form.Controls.Add(btnCancel);
            usedHeight += btnCancel.Height + 5;
        }

        void saveSettings()
        {
            foreach (GeneratorSetting setting in settings)
            {


                if (setting.EventControl is CheckBox)
                {
                    setting.Value= ((CheckBox)setting.EventControl).Checked.ToString();
                }
                else if (setting.EventControl is TextBox)
                {
                    setting.Value= ((TextBox)setting.EventControl).Text;
                }
                else
                {
                    if (setting.ControlType == ControlType.CQRSControl)
                    {

                        foreach (var ctrl in form.Controls)
                        {
                            if (ctrl is MethodSettingsControl methodCtrl)
                            {
                                if (setting.Name == methodCtrl.Name)
                                { 
                                    setting.Value=  methodCtrl.CQRSRequestNameTextBox.Text;
                                    setting.LabelText = methodCtrl.IsCQRSQuery.ToString(); // Just extra checkbox value stored in label
                                    setting.ContollerSetting = new ControllerSetting(methodCtrl.HttpAction, methodCtrl.ControllerMethod, methodCtrl.Route);
                                   
                                }
                            }

                        }
                    }
                    else
                        setting.Value= ((TextBox)setting.EventControl).Text;
                }


            }
        }
        #endregion
    }
}
