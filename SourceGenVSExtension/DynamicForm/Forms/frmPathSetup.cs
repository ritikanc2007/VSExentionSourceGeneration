using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.PlatformUI;
using Restarted.Generators.Common.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ToolWindow.DynamicForm.Model;
using ToolWindow.Models;
using ToolWindow.Utility;

namespace ToolWindow.DynamicForm.Forms
{

    public partial class frmPathSetup : Form
    {
        private ContextMenuStrip docMenu;

        public Dictionary<TypeOfCode, PathNameSpaceInfo> pathNameSpaceInfo = new Dictionary<TypeOfCode, PathNameSpaceInfo>();
        public frmPathSetup()
        {
            InitializeComponent();
            treeViewPaths.NodeMouseClick +=TreeViewPaths_NodeMouseClick;

            // treeViewPaths.Click +=TreeViewPaths_Click;

            // create context menu
            docMenu = CreateContextMenu();

            loadTree();

            loadSavedSettings();

            if (pathNameSpaceInfo == null)
                pathNameSpaceInfo = new Dictionary<TypeOfCode, PathNameSpaceInfo>();
        }

        void loadSavedSettings()
        {
            pathNameSpaceInfo= SerializationHelper.DeserializePathSettings(GeneratorConstants.CONST_GENERATOR_PATHS_FILE);
            if (pathNameSpaceInfo != null)
            {
                txtDTO.Text = pathNameSpaceInfo[TypeOfCode.DTO].Path;
                txtRepository.Text = pathNameSpaceInfo[TypeOfCode.Repository].Path;
                txtRepoInterface.Text = pathNameSpaceInfo[TypeOfCode.RepositoryInterface].Path;
                txtController.Text = pathNameSpaceInfo[TypeOfCode.Controller].Path;
                txtCQRS.Text = pathNameSpaceInfo[TypeOfCode.CQRSActions].Path;
                txtDI.Text = pathNameSpaceInfo[TypeOfCode.DI].Path;
                txtMappers.Text = pathNameSpaceInfo[TypeOfCode.Mapper].Path;
                // set root text 
                txtControllersRoot.Text= pathNameSpaceInfo[TypeOfCode.ControllersRootPath].Path;
                txtApplicationRoot.Text= pathNameSpaceInfo[TypeOfCode.ApplicationRootPath].Path;
                txtInfrastructureRoot.Text= pathNameSpaceInfo[TypeOfCode.InfrastructureRootPath].Path;

                // set conventions
                txtDTOConvention.Text = pathNameSpaceInfo[TypeOfCode.DTO].Convention;
                txtRepositoryConvention.Text = pathNameSpaceInfo[TypeOfCode.Repository].Convention;
                txtRepoInterfaceConvention.Text = pathNameSpaceInfo[TypeOfCode.RepositoryInterface].Convention;
                txtControllerConvention.Text = pathNameSpaceInfo[TypeOfCode.Controller].Convention;
                txtCQRSConvention.Text = pathNameSpaceInfo[TypeOfCode.CQRSActions].Convention;
                txtDIConvention.Text = pathNameSpaceInfo[TypeOfCode.DI].Convention;
                txtMappersConvention.Text = pathNameSpaceInfo[TypeOfCode.Mapper].Convention;
            }
        }

        void SetModifiedSettings()
        {
            if (pathNameSpaceInfo != null)
            {
                pathNameSpaceInfo[TypeOfCode.DTO].Path = txtDTO.Text;
                pathNameSpaceInfo[TypeOfCode.Repository].Path= txtRepository.Text;
                pathNameSpaceInfo[TypeOfCode.RepositoryInterface].Path=txtRepoInterface.Text;
                pathNameSpaceInfo[TypeOfCode.Controller].Path=txtController.Text;
                pathNameSpaceInfo[TypeOfCode.CQRSActions].Path = txtCQRS.Text;
                pathNameSpaceInfo[TypeOfCode.DI].Path = txtDI.Text;
                pathNameSpaceInfo[TypeOfCode.Mapper].Path = txtMappers.Text;
                // set root text 
                pathNameSpaceInfo[TypeOfCode.ControllersRootPath].Path = txtControllersRoot.Text;
                pathNameSpaceInfo[TypeOfCode.ApplicationRootPath].Path = txtApplicationRoot.Text;
                pathNameSpaceInfo[TypeOfCode.InfrastructureRootPath].Path = txtInfrastructureRoot.Text;

                // set conventions
                pathNameSpaceInfo[TypeOfCode.DTO].Convention = txtDTOConvention.Text;
                pathNameSpaceInfo[TypeOfCode.Repository].Convention = txtRepositoryConvention.Text;
                pathNameSpaceInfo[TypeOfCode.RepositoryInterface].Convention = txtRepoInterfaceConvention.Text;
                pathNameSpaceInfo[TypeOfCode.Controller].Convention = txtControllerConvention.Text;
                pathNameSpaceInfo[TypeOfCode.CQRSActions].Convention = txtCQRSConvention.Text;
                pathNameSpaceInfo[TypeOfCode.DI].Convention = txtDIConvention.Text;
                pathNameSpaceInfo[TypeOfCode.Mapper].Convention = txtMappersConvention.Text;
            }
        }

        ContextMenuStrip CreateContextMenu()
        {
            docMenu = new ContextMenuStrip();
            //Create some menu items.
            ToolStripMenuItem dtoMenu = new ToolStripMenuItem();

            dtoMenu.Text = "DTO";
            dtoMenu.Click+=(sender, e) =>
            {
                txtDTO.Text = treeViewPaths.SelectedNode.FullPath.ToString();
               
                var pathInfo = GetPathInfo(TypeOfCode.DTO, treeViewPaths.SelectedNode);
                txtDTOConvention.Text = pathInfo.FullPath;
                //Set Convention
                pathInfo.Convention =  txtDTOConvention.Text;
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.DTO))
                    pathNameSpaceInfo[TypeOfCode.DTO] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.DTO, pathInfo);
            };
            ToolStripMenuItem repoMenu = new ToolStripMenuItem();

            repoMenu.Text = "Repository";
            repoMenu.Click+=(sender, e) =>
            {
                txtRepository.Text = treeViewPaths.SelectedNode.FullPath.ToString();
               
                var pathInfo = GetPathInfo(TypeOfCode.Repository, treeViewPaths.SelectedNode);
                txtRepositoryConvention.Text= pathInfo.FullPath;
                pathInfo.Convention =  txtRepositoryConvention.Text;
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.Repository))
                    pathNameSpaceInfo[TypeOfCode.Repository] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.Repository, pathInfo);
            };
            ToolStripMenuItem repoInterfaceMenu = new ToolStripMenuItem();

            repoInterfaceMenu.Text = "Repo Interface";
            repoInterfaceMenu.Click+=(sender, e) =>
            {
                txtRepoInterface.Text = treeViewPaths.SelectedNode.FullPath.ToString();
               
                var pathInfo = GetPathInfo(TypeOfCode.RepositoryInterface, treeViewPaths.SelectedNode);
                txtRepoInterfaceConvention.Text=pathInfo.FullPath;
                pathInfo.Convention =  txtRepoInterfaceConvention.Text;
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.RepositoryInterface))
                    pathNameSpaceInfo[TypeOfCode.RepositoryInterface] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.RepositoryInterface, pathInfo);
            };
            ToolStripMenuItem CQRSMenu = new ToolStripMenuItem();
            CQRSMenu.Text = "CQRS";
            CQRSMenu.Click+=(sender, e) =>
            {
                txtCQRS.Text = treeViewPaths.SelectedNode.FullPath.ToString();
               
                var pathInfo = GetPathInfo(TypeOfCode.CQRSActions, treeViewPaths.SelectedNode);
                txtCQRSConvention.Text= pathInfo.FullPath;
                pathInfo.Convention =  txtCQRSConvention.Text;
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.CQRSActions))
                    pathNameSpaceInfo[TypeOfCode.CQRSActions] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.CQRSActions, pathInfo);
            };

            ToolStripMenuItem ControllerMenu = new ToolStripMenuItem();
            ControllerMenu.Text = "Controller";
            ControllerMenu.Click+=(sender, e) =>
            {
                txtController.Text = treeViewPaths.SelectedNode.FullPath.ToString();
               
                var pathInfo = GetPathInfo(TypeOfCode.Controller, treeViewPaths.SelectedNode);
                txtControllerConvention.Text = pathInfo.FullPath;
                pathInfo.Convention =  txtControllerConvention.Text;
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.Controller))
                    pathNameSpaceInfo[TypeOfCode.Controller] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.Controller, pathInfo);
            };
            ToolStripMenuItem DIMenu = new ToolStripMenuItem();
            DIMenu.Text = "DI";
            DIMenu.Click+=(sender, e) =>
            {
                txtDI.Text = treeViewPaths.SelectedNode.FullPath.ToString();
               
                var pathInfo = GetPathInfo(TypeOfCode.DI, treeViewPaths.SelectedNode);
                txtDIConvention.Text = pathInfo.FullPath;
                pathInfo.Convention =  txtDIConvention.Text;
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.DI))
                    pathNameSpaceInfo[TypeOfCode.DI] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.DI, pathInfo);
            };
            ToolStripMenuItem MapperMenu = new ToolStripMenuItem();
            MapperMenu.Text = "Mappers";
            MapperMenu.Click+=(sender, e) =>
            {
                txtMappers.Text = treeViewPaths.SelectedNode.FullPath.ToString();
                
                var pathInfo = GetPathInfo(TypeOfCode.Mapper, treeViewPaths.SelectedNode);
                txtMappersConvention.Text = pathInfo.FullPath;
                pathInfo.Convention =  txtMappersConvention.Text;
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.Mapper))
                    pathNameSpaceInfo[TypeOfCode.Mapper] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.Mapper, pathInfo);
            };

            ToolStripMenuItem ControllerRootPathMenu = new ToolStripMenuItem();
            ControllerRootPathMenu.Text = "Controller Root";
            ControllerRootPathMenu.Click+=(sender, e) =>
            {
                txtControllersRoot.Text = treeViewPaths.SelectedNode.FullPath.ToString();
                var pathInfo = GetPathInfo(TypeOfCode.ControllersRootPath, treeViewPaths.SelectedNode);

                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.ControllersRootPath))
                    pathNameSpaceInfo[TypeOfCode.ControllersRootPath] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.ControllersRootPath, pathInfo);
            };

            ToolStripMenuItem ApplicationRootPathMenu = new ToolStripMenuItem();
            ApplicationRootPathMenu.Text = "Application Root";
            ApplicationRootPathMenu.Click+=(sender, e) =>
            {
                txtApplicationRoot.Text = treeViewPaths.SelectedNode.FullPath.ToString();
                var pathInfo = GetPathInfo(TypeOfCode.ApplicationRootPath, treeViewPaths.SelectedNode);
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.ApplicationRootPath))
                    pathNameSpaceInfo[TypeOfCode.ApplicationRootPath] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.ApplicationRootPath, pathInfo);
            };

            ToolStripMenuItem InfrastructureRootPathMenu = new ToolStripMenuItem();
            InfrastructureRootPathMenu.Text = "Infrastructure Root";
            InfrastructureRootPathMenu.Click+=(sender, e) =>
            {
                txtInfrastructureRoot.Text = treeViewPaths.SelectedNode.FullPath.ToString();
                var pathInfo = GetPathInfo(TypeOfCode.InfrastructureRootPath, treeViewPaths.SelectedNode);
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.InfrastructureRootPath))
                    pathNameSpaceInfo[TypeOfCode.InfrastructureRootPath] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.InfrastructureRootPath, pathInfo);
            };
            //Add the menu items to the menu.
            docMenu.Items.AddRange(new ToolStripMenuItem[]{
                dtoMenu,repoMenu, repoInterfaceMenu,CQRSMenu,
                ControllerMenu,DIMenu,MapperMenu,ControllerRootPathMenu,ApplicationRootPathMenu,InfrastructureRootPathMenu});


            return docMenu;
        }

        private void TreeViewPaths_Click(object sender, EventArgs e)
        {
        }

        private void TreeViewPaths_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //txtTreeDBlClick.Text= treeViewPaths.SelectedNode.FullPath.ToString();

            //txtNodeClick.Text = treeViewPaths.SelectedNode.FullPath.ToString();



        }

        private PathNameSpaceInfo GetPathInfo(TypeOfCode typeOfCode, TreeNode selectedNode)
        {
            if (selectedNode == null) return null;

            string displayPath = selectedNode.FullPath.ToString();
            var solutionItem = treeViewPaths.SelectedNode.Tag as SolutionItem;
            if (solutionItem == null) return null;

            string fullPath = solutionItem.FullPath;
            string nameSpace = "";

            if (solutionItem.Type.ToString() == "Project")
            {
                int projNameIndex = fullPath.ToString().IndexOf(solutionItem.Name+".csproj");
                if (projNameIndex != -1) {
                    fullPath = fullPath.ToString().Substring(0, projNameIndex);
                }
            }

                if (solutionItem is PhysicalFolder folder)
            {
                if (folder is null) return null;
                var project = folder.ContainingProject;
                var projectName = project.Name;
                var folderPath = folder.FullPath.Trim().ToString();
                var projIndex = folderPath.IndexOf(projectName, StringComparison.OrdinalIgnoreCase);
                nameSpace = folderPath.Substring(projIndex).Replace("\\", ".");
                if (nameSpace.Substring(nameSpace.Length-1, 1) ==".") // remove last dot
                    nameSpace = nameSpace.Substring(0, nameSpace.Length-1);
            }

            return new PathNameSpaceInfo(typeOfCode, displayPath, nameSpace, fullPath);
        }
        private void loadTree()
        {
            GetProjects();
        }
        List<ProjectFile> GetProjects()
        {
            List<ProjectFile> projectList = new List<ProjectFile>();
            var solution = VS.Solutions.GetCurrentSolution();

            string parentFolderPAth = solution.FindParent(SolutionItemType.PhysicalFolder)?.FullPath;
            if (parentFolderPAth == null)
                parentFolderPAth =Path.GetDirectoryName(solution.FullPath);

            ProjectFile solutionItem = new ProjectFile(solution.Name, solution.Type.ToString(), solution.FullPath, parentFolderPAth);


            projectList.Add(solutionItem);

            TreeNode node = new TreeNode();
            node.Text = solution.ToString();
            node.ToolTipText = solution.FullPath;
            node.Tag = solutionItem;
            treeViewPaths.Nodes.Add(node);

            loadChildren(solution.Children, ref solutionItem, ref node);


            return projectList;
        }
        void loadChildren(IEnumerable<SolutionItem> items, ref ProjectFile parentItem, ref TreeNode node)
        {
            foreach (var ch in items)
            {
                string parentFolderPAth = ch.FindParent(SolutionItemType.PhysicalFolder)?.FullPath;
                if (parentFolderPAth == null)
                    parentFolderPAth =Path.GetDirectoryName(ch.FullPath);

                ProjectFile childCode = new ProjectFile(ch.Name, ch.Type.ToString(), ch.FullPath, parentFolderPAth);
                parentItem.Projects.Add(childCode);






                if (ch.Type == SolutionItemType.Project || ch.Type == SolutionItemType.PhysicalFolder)
                {
                    TreeNode nodeChild = new TreeNode();
                    nodeChild.Text = ch.Name;
                    nodeChild.ToolTipText = ch.FullPath;
                    nodeChild.Tag = ch;
                    nodeChild.ContextMenuStrip=docMenu;
                    node.Nodes.Add(nodeChild);
                    loadChildren(ch.Children, ref childCode, ref nodeChild);
                }
                else
                {
                    //node.ContextMenuStrip = docMenu;
                    loadChildren(ch.Children, ref childCode, ref node);
                }


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetModifiedSettings();
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.Text =="" && !textBox.Name.Contains("Convention") && !textBox.Name.Contains("Root"))
                    {
                        VS.MessageBox.ShowWarning("Please enter path for all type of code");
                        return;
                    }
                }

            }

            foreach (var key in pathNameSpaceInfo.Keys)
            {
                var setting = pathNameSpaceInfo[key];
                if (!Directory.Exists(setting.FullPath))
                {
                    VS.MessageBox.ShowWarning($"Please enter path {key.ToString()} type of code.");
                }
            }
            SerializationHelper.SavePathSettings(pathNameSpaceInfo, GeneratorConstants.CONST_GENERATOR_PATHS_FILE);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void richTextBoxConventions_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
