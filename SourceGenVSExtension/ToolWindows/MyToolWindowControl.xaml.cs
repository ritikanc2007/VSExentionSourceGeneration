using Community.VisualStudio.Toolkit;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using EnvDTE;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

using SourceGeneratorParser;
using SourceGeneratorParser.Parsers;
using SourceGeneratorParser.Parsers.Common;
using SourceGeneratorParser.Models.Types;
using System.Windows.Controls;
using Microsoft.VisualStudio.PlatformUI;
using ToolWindow.Models;
using WinFormsApp1.DynamicForm;
using System.Threading.Tasks;
using Restarted.Generators.Common.Context;
using Restarted.Generators.FeatureProcessors.DTO;
using ToolWindow.DynamicForm.Model;
using Microsoft.VisualStudio.Shell.Design.Serialization;
using Restarted.Generators.FeatureProcessors.Models;
using Restarted.Generators.FeatureProcessors.Repository;
using Restarted.Generators.FeatureProcessors.CQRS;
using Restarted.Generators.FeatureProcessors.Controllers;
using WinFormsApp1.DynamicForm.Model;
using Newtonsoft.Json;
using ToolWindow.Utility;
using ToolWindow.DynamicForm;
using System.Reflection;
using static Restarted.Generators.FeatureProcessors.CQRS.CQRSService;
using Restarted.Generators.Generators.CQRS.Models;
using ToolWindow.ProcessRequest;
using ToolWindow.DynamicForm.Forms;
using System.Windows.Controls.Primitives;

namespace ToolWindow
{
    public partial class MyToolWindowControl : UserControl
    {

        private TypeDefinitionInfo SelectedTypeDefinitionInfo;
        private ProjectFile SelectedSolutionItem;
        public MyToolWindowControl(Version vsVersion)
        {
            InitializeComponent();

            lblHeadline.Content = $"Visual Studio v{vsVersion}";

            VS.Events.SolutionEvents.OnAfterOpenSolution += SolutionEvents_OnAfterOpenSolution;

            VS.Events.SolutionEvents.OnBeforeCloseSolution +=SolutionEvents_OnBeforeCloseSolution;

           
        }

        private void SolutionEvents_OnBeforeCloseSolution()
        {
            SourceTreeView.ItemsSource = new List<ProjectFile>();
        }

        private void SolutionEvents_OnAfterOpenSolution(Community.VisualStudio.Toolkit.Solution obj)
        {
            LoadSourceExplores();
        }

        void LoadSourceExplores()
        {
            List<ProjectFile> Projects = GetProjects();
            SourceTreeView.ItemsSource = Projects;
        }

        async void SelectedClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //TreeViewItem selectedItem = SourceTreeView.SelectedItem as TreeViewItem;
            SelectedSolutionItem = SourceTreeView.SelectedItem as ProjectFile;
            if (SelectedSolutionItem != null)
            {
                //read all content and load type, attributes, methods and member information
                this.SelectedTypeDefinitionInfo= await GetTypeInformation();
            }
        }

        void OptionClick(object sender, RoutedEventArgs e)
        {
            //Code 5
            var selectedItem = SourceTreeView.SelectedItem;
            MenuItem item = sender as MenuItem;

            var pathSettings = ConfigurationHelper.PathSettings();
            var conventionSetting = ConfigurationHelper.ConventionSettings();
            if (selectedItem != null && item != null)
            {
                string menuItemName = item.Name;
                string menuItemHeader = item.Header.ToString();

                var formBuilder = new DynamicFormBuilder();



                #region Validate Path and Convention setting and enforce
                // Path setting related
                if (pathSettings == null &&  menuItemName != "PATHS")
                {
                    VS.MessageBox.ShowError("Please ensure that PATH settings are configured!");
                    menuItemName = "PATHS"; // setting to force paths setupd
                }
                if (conventionSetting == null &&  menuItemName != "CONVENTIONS")
                {
                    VS.MessageBox.ShowError("Please ensure that CONVENTION settings are configured!");
                    menuItemName = "CONVENTIONS"; // setting to force paths setupd
                }

                // delete all settings
                if (menuItemName =="DELETE")
                {
                    if (VS.MessageBox.ShowConfirm("Are you sure you want to delete all saved settings?", "All setting related to Path,Convention & Saved context information will be deleted."))
                    {
                        File.Delete(GeneratorConstants.CONST_GENERATOR_CONVENTION_FILE);
                        File.Delete(GeneratorConstants.CONST_GENERATOR_PATHS_FILE);
                        File.Delete(GeneratorConstants.CONST_GENERATOR_CONTEXT_FILE);
                        return;
                    }
                }
                if ((menuItemName == "PATHS" || menuItemName == "CONVENTIONS"))
                {
                    List<GeneratorSetting> genSetting = null;

                    string fileName = "";

                    if (menuItemName == "PATHS")
                    {
                        fileName= GeneratorConstants.CONST_GENERATOR_PATHS_FILE;
                        frmPathSetup pathSetup = new frmPathSetup();
                        pathSetup.ShowDialog();
                        pathSettings = pathSetup.pathNameSpaceInfo;
                        return;
                    }
                    else if (menuItemName == "CONVENTIONS")
                    {
                        fileName=GeneratorConstants.CONST_GENERATOR_CONVENTION_FILE;

                        genSetting = SerializationHelper.DeserializeSettings(fileName);

                        genSetting = formBuilder.Generate(SelectedTypeDefinitionInfo, menuItemName, SelectedSolutionItem.ParentFolderPath, genSetting);

                        SerializationHelper.SaveSettings(genSetting, fileName);
                        return;
                    }
                }
                #endregion

                // Code generation related
                var settings = formBuilder.Generate(SelectedTypeDefinitionInfo, menuItemName, SelectedSolutionItem.ParentFolderPath, null);

                if (VS.MessageBox.ShowConfirm("Do you want to proceed with generation?", "File will be create in the SubFolder named 'Generated'") == true)
                {
                    List<string> files = new List<string>(); ;
                    // execute generator
                    GeneratorContext generatorContext = SerializationHelper.GetSavedGenerationContext(GeneratorConstants.CONST_GENERATOR_CONTEXT_FILE);
                    if (generatorContext == null) generatorContext = new GeneratorContext();



                    if (SelectedTypeDefinitionInfo != null && menuItemName == "DTO")
                    {

                        // generate CQRS
                        files=   ProcessGeneration.DTO(generatorContext, SelectedTypeDefinitionInfo, settings);

                    }
                    else if (SelectedTypeDefinitionInfo != null && menuItemName == "REPO")
                    {
                        // generate CQRS
                        files=  ProcessGeneration.Repository(generatorContext, SelectedTypeDefinitionInfo, settings);
                    }
                    else if (SelectedTypeDefinitionInfo != null && menuItemName == "CQRS")
                    {
                        // generate CQRS
                        files=   ProcessGeneration.CQRS(generatorContext, SelectedTypeDefinitionInfo, settings);

                        // generate controllers
                        files=  ProcessGeneration.ControllersCQRS(generatorContext, SelectedTypeDefinitionInfo, settings);
                    }
                    else if (SelectedTypeDefinitionInfo != null && menuItemName == "CTRL")
                    {

                        // generate controllers
                        files=  ProcessGeneration.ControllersCQRS(generatorContext, SelectedTypeDefinitionInfo, settings);
                    }
                    else if (SelectedTypeDefinitionInfo != null && menuItemName == "DI")
                    {

                        // generate controllers
                        files=  ProcessGeneration.DependencyRegistration(generatorContext, SelectedTypeDefinitionInfo, settings);
                    }
                    else if (SelectedTypeDefinitionInfo != null && menuItemName == "MAPPERS")
                    {

                        // generate controllers
                        files=  ProcessGeneration.MapperProfiles(generatorContext, SelectedTypeDefinitionInfo, settings);
                    }
                    else if (SelectedTypeDefinitionInfo != null && menuItemName == "GLOBALUSING")
                    {

                        // generate controllers
                        files=  ProcessGeneration.GlobalUsings(generatorContext, SelectedTypeDefinitionInfo, settings);
                    }

                    // Save Generation Context
                    SerializationHelper.SaveGenerationContext(generatorContext, GeneratorConstants.CONST_GENERATOR_CONTEXT_FILE);

                    string file = files.FirstOrDefault();
                    if (file != null)
                    {
                        if (VS.MessageBox.ShowConfirm($"File generated at {file} location.", "Do you want to repolulate the explorer?"))
                        {
                            LoadSourceExplores();
                        }

                    }
                }
            }
        }



        async Task<TypeDefinitionInfo> GetTypeInformation()
        {
            TypeDefinitionInfo typeDefinition = null;
            try
            {

                string source = File.ReadAllText(SelectedSolutionItem.Path);

                IParser Parser = TransformFactory.Create(ParserType.SyntaxTree);
                typeDefinition = Parser.Parse(new ParsingDetails(source));
            }
            catch (Exception ex)
            {


            }

            return typeDefinition;
        }






        #region Collect Solution Files information
        List<ProjectFile> GetProjects()
        {
            List<ProjectFile> projectList = new List<ProjectFile>();
            var solution = VS.Solutions.GetCurrentSolution();

            string parentFolderPAth = solution.FindParent(SolutionItemType.PhysicalFolder)?.FullPath;
            if (parentFolderPAth == null)
                parentFolderPAth =Path.GetDirectoryName(solution.FullPath);

            ProjectFile solutionItem = new ProjectFile(solution.Name, solution.Type.ToString(), solution.FullPath, parentFolderPAth);

            projectList.Add(solutionItem);



            loadChildren(solution.Children, ref solutionItem);


            return projectList;
        }

        void loadChildren(IEnumerable<SolutionItem> items, ref ProjectFile parentItem)
        {
            foreach (var ch in items)
            {
                string parentFolderPAth = ch.FindParent(SolutionItemType.PhysicalFolder)?.FullPath;
                if (parentFolderPAth == null)
                    parentFolderPAth =Path.GetDirectoryName(ch.FullPath);

                ProjectFile childCode = new ProjectFile(ch.Name, ch.Type.ToString(), ch.FullPath, parentFolderPAth);
                parentItem.Projects.Add(childCode);
                loadChildren(ch.Children, ref childCode);
            }
        }
        #endregion



    }
}