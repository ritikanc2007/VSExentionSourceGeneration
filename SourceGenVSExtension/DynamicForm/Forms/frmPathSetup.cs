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
                if (pathNameSpaceInfo.ContainsKey(TypeOfCode.Mapper))
                    pathNameSpaceInfo[TypeOfCode.Mapper] = pathInfo;
                else
                    pathNameSpaceInfo.Add(TypeOfCode.Mapper, pathInfo);
            };
            //Add the menu items to the menu.
            docMenu.Items.AddRange(new ToolStripMenuItem[]{
                dtoMenu,repoMenu, repoInterfaceMenu,CQRSMenu,
                ControllerMenu,DIMenu,MapperMenu});


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
            if (solutionItem is PhysicalFolder folder)
            {
                if (folder is null) return null;
                var project = folder.ContainingProject;
                var projectName = project.Name;
                var folderPath = folder.FullPath.ToString();
                var projIndex = folderPath.IndexOf(projectName);
                nameSpace = folderPath.Substring(projIndex).Replace("\\", ".");
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
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.Text =="")
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
    }
}
