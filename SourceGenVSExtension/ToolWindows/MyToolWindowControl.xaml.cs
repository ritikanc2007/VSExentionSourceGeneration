using Community.VisualStudio.Toolkit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Restarted.Generators.Common.Context;
using SourceGeneratorParser;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Parsers;
using SourceGeneratorParser.Parsers.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToolWindow.DynamicForm;
using ToolWindow.DynamicForm.Forms;
using ToolWindow.DynamicForm.Model;
using ToolWindow.Models;
using ToolWindow.ProcessRequest;
using ToolWindow.Utility;
using WinFormsApp1.DynamicForm;
using WinFormsApp1.DynamicForm.Model;
using System.Threading;
using System.CodeDom;
using Microsoft.VisualStudio.PlatformUI;

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
            VS.Events.SolutionEvents.OnAfterBackgroundSolutionLoadComplete+=SolutionEvents_OnAfterBackgroundSolutionLoadComplete;
            VS.Events.SolutionEvents.OnBeforeCloseSolution +=SolutionEvents_OnBeforeCloseSolution;


        }

        private void SolutionEvents_OnAfterBackgroundSolutionLoadComplete()
        {
            if (SourceTreeView.ItemsSource == null)
                LoadSourceExplores();
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
#warning added for testing - DELETE
                //var doc = await VS.Documents.OpenAsync(SelectedSolutionItem.Path);
                //if (doc != null)
                //{
                //    doc.TextView.Selection.SelectionChanged +=Selection_SelectionChanged;

                //    // Warning ends here
                //}
                this.SelectedTypeDefinitionInfo= await GetTypeInformation();
            }
        }

        async private void Selection_SelectionChanged(object sender, EventArgs e)
        {
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
            var position = docView.TextView?.Selection.Start.Position.Position;
            //var text = docView.TextView?.Selection.TextView.TextBuffer.CurrentSnapshot;
            if (position.HasValue)
            {
                //docView.TextBuffer.Insert(position.Value, Guid.NewGuid().ToString());
                //var textDoc= docView.TextBuffer.GetTextDocument();

                //var doc = await VS.Documents.GetActiveDocumentViewAsync();

                var span = docView.TextView.Selection.SelectedSpans.FirstOrDefault();
                var text = span.GetText();
                if (text.Length >10)
                    CheckSelectionIsMethod(text);
            }
        }



        void OnMenuOpening(object sender, ContextMenuEventArgs e)
        {

            FrameworkElement fe = e.Source as FrameworkElement;
            if (this.SelectedTypeDefinitionInfo == null)
            {

                //cm.ContextMenu.Opened =;
                fe.ContextMenu.IsOpen=false;
                e.Handled=true;
            }
            else
            {
                TextBlock block = e.Source as TextBlock;
                //cm.ContextMenu.Opened =;
                ContextMenu cm = block.ContextMenu;

                var child = cm.Items.Count;
                foreach (var item in cm.Items)
                {
                    var sm = item as MenuItem;
                    if (sm.Name =="Generate" && this.SelectedTypeDefinitionInfo != null)
                    {
                        foreach (MenuItem sub in sm.Items)
                        {
                            if (this.SelectedTypeDefinitionInfo.DeclarationType == TypeOfDeclaration.Class)
                            {
                                sub.Visibility= Visibility.Visible;
                                if (new string[] { "DTO", "REPO", "CTRL" }.Contains(sub.Name))
                                    sub.IsEnabled=true;
                                else
                                    sub.IsEnabled= false;
                            }
                            else if (this.SelectedTypeDefinitionInfo.DeclarationType == TypeOfDeclaration.Interface)
                            {
                                sub.Visibility= Visibility.Visible;
                                if (new string[] { "CQRS", "EXPLORE" }.Contains(sub.Name))
                                    sub.IsEnabled= true;
                                else
                                    sub.IsEnabled= false;
                            }
                        }
                    }



                }


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
                        //files=  ProcessGeneration.ControllersCQRS(generatorContext, SelectedTypeDefinitionInfo, settings);
                        files=  ProcessGeneration.ControllerWithRepo(generatorContext, SelectedTypeDefinitionInfo, settings);

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

        void CheckSelectionIsMethod(string text)
        {
            //CompilationUnitSyntax root = null;
            try
            {
                CancellationToken token = new CancellationTokenSource().Token;
                var root = CSharpSyntaxTree.ParseText(text).GetRoot(token);

                if (root is CompilationUnitSyntax)
                {

                    foreach (var item in root.ChildNodes())
                    {
                        foreach (var item1 in item.ChildNodes())
                        {
                            if (item1 is LocalFunctionStatementSyntax fs)
                            {
                                var txt = fs.Identifier.Text;
                                var check = $"Do you want to generate Controller and Repo method for {txt}?";
                                if (txt.Length > 0)
                                {
                                    var method = SelectedTypeDefinitionInfo.Methods.Where(o => o.Name == txt);
                                    if (method != null &&  SelectedTypeDefinitionInfo.DeclarationType == TypeOfDeclaration.Interface)
                                    {
                                        // find repo class
                                        // lets assume we found it

                                        string filePath = @"C:\Users\Narendra\source\repos\VSExentionSourceGeneration\ApiTemplate_For_Testing\Restarted.System.Infrastructure\Persistence\Repositories\UserRepository.cs";
                                        string repoText = File.ReadAllText(filePath);
                                        var rootClass = CSharpSyntaxTree.ParseText(repoText).GetRoot(token);
                                        SyntaxNode classSyntaxNode = null;
                                        FindTypeDeclarationSyntax(rootClass.ChildNodes(), out classSyntaxNode);
                                        if (classSyntaxNode is ClassDeclarationSyntax classSyntax)
                                        {
                                            string methodSource = @"string Lookup(){ return 'Hello';}";
                                            var methodSyntax = CSharpSyntaxTree.ParseText(methodSource).GetRoot(token);
                                            var tree = methodSyntax as SyntaxNode;
                                            foreach (var node in tree.ChildNodes())
                                            {
                                                if (node is MemberDeclarationSyntax foundMethod)
                                                {
                                                    tree = node;
                                                }
                                            }
                                            if (tree is MemberDeclarationSyntax mm)
                                            {
                                                // Replacing class node
                                                // access old node
                                                ClassDeclarationSyntax old = classSyntax;
                                                // Search node with identified i.e classname e.g. IUserRepository and store new node
                                                ClassDeclarationSyntax New = old.WithIdentifier(classSyntax.Identifier);
                                                // add member to new node
                                                var value = New.AddMembers(mm);
                                                // replace old node with new in the ROOT
#warning Method name is used to locate the node. Overloaded methods will not work
#warning refer https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/get-started/syntax-transformation
                                                rootClass = rootClass.ReplaceNode(old, value);
                                                var source = rootClass.ToFullString();
                                                File.WriteAllText(filePath+".temp", source);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Couldnot parse the provided source code string", ex);
            }
        }
        private void FindTypeDeclarationSyntax(IEnumerable<SyntaxNode> childNodes, out SyntaxNode typeDeclarationSyntaxNode)
        {
            typeDeclarationSyntaxNode = null;
            foreach (var node in childNodes)
            {
                if (node is ClassDeclarationSyntax)
                {
                    typeDeclarationSyntaxNode = node;
                    return;
                }

                FindTypeDeclarationSyntax(node.ChildNodes(), out typeDeclarationSyntaxNode);
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

        async Task<TypeDefinitionInfo> GetTypeInformation(string filePath)
        {
            TypeDefinitionInfo typeDefinition = null;
            try
            {

                string source = File.ReadAllText(filePath);

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