namespace ToolWindow.DynamicForm.Forms
{
    partial class frmPathSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPathSetup));
            this.treeViewPaths = new System.Windows.Forms.TreeView();
            this.projectFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtDTO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtController = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRepository = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRepoInterface = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCQRS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDI = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMappers = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtMappersConvention = new System.Windows.Forms.TextBox();
            this.txtDIConvention = new System.Windows.Forms.TextBox();
            this.txtCQRSConvention = new System.Windows.Forms.TextBox();
            this.txtRepoInterfaceConvention = new System.Windows.Forms.TextBox();
            this.txtRepositoryConvention = new System.Windows.Forms.TextBox();
            this.txtControllerConvention = new System.Windows.Forms.TextBox();
            this.txtDTOConvention = new System.Windows.Forms.TextBox();
            this.lblPathHeader = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.richTextBoxConventions = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtInfrastructureRoot = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtApplicationRoot = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtControllersRoot = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtContractsRoot = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.projectFileBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewPaths
            // 
            this.treeViewPaths.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.projectFileBindingSource, "Name", true));
            this.treeViewPaths.Location = new System.Drawing.Point(13, 13);
            this.treeViewPaths.Name = "treeViewPaths";
            this.treeViewPaths.Size = new System.Drawing.Size(393, 570);
            this.treeViewPaths.TabIndex = 0;
            // 
            // projectFileBindingSource
            // 
            this.projectFileBindingSource.DataSource = typeof(ToolWindow.Models.ProjectFile);
            // 
            // txtDTO
            // 
            this.txtDTO.Location = new System.Drawing.Point(546, 39);
            this.txtDTO.Multiline = true;
            this.txtDTO.Name = "txtDTO";
            this.txtDTO.ReadOnly = true;
            this.txtDTO.Size = new System.Drawing.Size(539, 39);
            this.txtDTO.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(497, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "DTO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(462, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Controller";
            // 
            // txtController
            // 
            this.txtController.Location = new System.Drawing.Point(546, 86);
            this.txtController.Multiline = true;
            this.txtController.Name = "txtController";
            this.txtController.ReadOnly = true;
            this.txtController.Size = new System.Drawing.Size(539, 45);
            this.txtController.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(454, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repository";
            // 
            // txtRepository
            // 
            this.txtRepository.Location = new System.Drawing.Point(546, 139);
            this.txtRepository.Multiline = true;
            this.txtRepository.Name = "txtRepository";
            this.txtRepository.ReadOnly = true;
            this.txtRepository.Size = new System.Drawing.Size(539, 45);
            this.txtRepository.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(423, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Repo Interface";
            // 
            // txtRepoInterface
            // 
            this.txtRepoInterface.Location = new System.Drawing.Point(546, 192);
            this.txtRepoInterface.Multiline = true;
            this.txtRepoInterface.Name = "txtRepoInterface";
            this.txtRepoInterface.ReadOnly = true;
            this.txtRepoInterface.Size = new System.Drawing.Size(539, 45);
            this.txtRepoInterface.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(484, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "CQRS";
            // 
            // txtCQRS
            // 
            this.txtCQRS.Location = new System.Drawing.Point(546, 245);
            this.txtCQRS.Multiline = true;
            this.txtCQRS.Name = "txtCQRS";
            this.txtCQRS.ReadOnly = true;
            this.txtCQRS.Size = new System.Drawing.Size(539, 45);
            this.txtCQRS.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(513, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "DI";
            // 
            // txtDI
            // 
            this.txtDI.Location = new System.Drawing.Point(546, 298);
            this.txtDI.Multiline = true;
            this.txtDI.Name = "txtDI";
            this.txtDI.ReadOnly = true;
            this.txtDI.Size = new System.Drawing.Size(539, 45);
            this.txtDI.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(468, 361);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Mappers";
            // 
            // txtMappers
            // 
            this.txtMappers.Location = new System.Drawing.Point(546, 351);
            this.txtMappers.Multiline = true;
            this.txtMappers.Name = "txtMappers";
            this.txtMappers.ReadOnly = true;
            this.txtMappers.Size = new System.Drawing.Size(539, 45);
            this.txtMappers.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(1355, 591);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 40);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(1519, 591);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 40);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtMappersConvention
            // 
            this.txtMappersConvention.Location = new System.Drawing.Point(1119, 351);
            this.txtMappersConvention.Multiline = true;
            this.txtMappersConvention.Name = "txtMappersConvention";
            this.txtMappersConvention.Size = new System.Drawing.Size(539, 45);
            this.txtMappersConvention.TabIndex = 23;
            // 
            // txtDIConvention
            // 
            this.txtDIConvention.Location = new System.Drawing.Point(1119, 298);
            this.txtDIConvention.Multiline = true;
            this.txtDIConvention.Name = "txtDIConvention";
            this.txtDIConvention.Size = new System.Drawing.Size(539, 45);
            this.txtDIConvention.TabIndex = 22;
            // 
            // txtCQRSConvention
            // 
            this.txtCQRSConvention.Location = new System.Drawing.Point(1119, 245);
            this.txtCQRSConvention.Multiline = true;
            this.txtCQRSConvention.Name = "txtCQRSConvention";
            this.txtCQRSConvention.Size = new System.Drawing.Size(539, 45);
            this.txtCQRSConvention.TabIndex = 21;
            // 
            // txtRepoInterfaceConvention
            // 
            this.txtRepoInterfaceConvention.Location = new System.Drawing.Point(1119, 192);
            this.txtRepoInterfaceConvention.Multiline = true;
            this.txtRepoInterfaceConvention.Name = "txtRepoInterfaceConvention";
            this.txtRepoInterfaceConvention.Size = new System.Drawing.Size(539, 45);
            this.txtRepoInterfaceConvention.TabIndex = 20;
            // 
            // txtRepositoryConvention
            // 
            this.txtRepositoryConvention.Location = new System.Drawing.Point(1119, 139);
            this.txtRepositoryConvention.Multiline = true;
            this.txtRepositoryConvention.Name = "txtRepositoryConvention";
            this.txtRepositoryConvention.Size = new System.Drawing.Size(539, 45);
            this.txtRepositoryConvention.TabIndex = 19;
            // 
            // txtControllerConvention
            // 
            this.txtControllerConvention.Location = new System.Drawing.Point(1119, 86);
            this.txtControllerConvention.Multiline = true;
            this.txtControllerConvention.Name = "txtControllerConvention";
            this.txtControllerConvention.Size = new System.Drawing.Size(539, 45);
            this.txtControllerConvention.TabIndex = 18;
            // 
            // txtDTOConvention
            // 
            this.txtDTOConvention.Location = new System.Drawing.Point(1119, 39);
            this.txtDTOConvention.Multiline = true;
            this.txtDTOConvention.Name = "txtDTOConvention";
            this.txtDTOConvention.Size = new System.Drawing.Size(539, 39);
            this.txtDTOConvention.TabIndex = 17;
            // 
            // lblPathHeader
            // 
            this.lblPathHeader.AutoSize = true;
            this.lblPathHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPathHeader.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblPathHeader.Location = new System.Drawing.Point(467, 9);
            this.lblPathHeader.Name = "lblPathHeader";
            this.lblPathHeader.Size = new System.Drawing.Size(73, 26);
            this.lblPathHeader.TabIndex = 24;
            this.lblPathHeader.Text = "Paths";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label8.Location = new System.Drawing.Point(1115, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(251, 26);
            this.label8.TabIndex = 25;
            this.label8.Text = "Conventions (optional)";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // richTextBoxConventions
            // 
            this.richTextBoxConventions.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBoxConventions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxConventions.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxConventions.ForeColor = System.Drawing.Color.Black;
            this.richTextBoxConventions.Location = new System.Drawing.Point(1119, 402);
            this.richTextBoxConventions.Name = "richTextBoxConventions";
            this.richTextBoxConventions.ReadOnly = true;
            this.richTextBoxConventions.Size = new System.Drawing.Size(539, 181);
            this.richTextBoxConventions.TabIndex = 26;
            this.richTextBoxConventions.Text = resources.GetString("richTextBoxConventions.Text");
            this.richTextBoxConventions.TextChanged += new System.EventHandler(this.richTextBoxConventions_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(22, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 20);
            this.label9.TabIndex = 32;
            this.label9.Text = "Infrastructure";
            // 
            // txtInfrastructureRoot
            // 
            this.txtInfrastructureRoot.Location = new System.Drawing.Point(134, 125);
            this.txtInfrastructureRoot.Multiline = true;
            this.txtInfrastructureRoot.Name = "txtInfrastructureRoot";
            this.txtInfrastructureRoot.ReadOnly = true;
            this.txtInfrastructureRoot.Size = new System.Drawing.Size(539, 45);
            this.txtInfrastructureRoot.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(40, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 20);
            this.label10.TabIndex = 30;
            this.label10.Text = "Application";
            // 
            // txtApplicationRoot
            // 
            this.txtApplicationRoot.Location = new System.Drawing.Point(134, 72);
            this.txtApplicationRoot.Multiline = true;
            this.txtApplicationRoot.Name = "txtApplicationRoot";
            this.txtApplicationRoot.ReadOnly = true;
            this.txtApplicationRoot.Size = new System.Drawing.Size(539, 45);
            this.txtApplicationRoot.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(42, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 20);
            this.label11.TabIndex = 28;
            this.label11.Text = "Controllers";
            // 
            // txtControllersRoot
            // 
            this.txtControllersRoot.Location = new System.Drawing.Point(134, 25);
            this.txtControllersRoot.Multiline = true;
            this.txtControllersRoot.Name = "txtControllersRoot";
            this.txtControllersRoot.ReadOnly = true;
            this.txtControllersRoot.Size = new System.Drawing.Size(539, 39);
            this.txtControllersRoot.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtContractsRoot);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtControllersRoot);
            this.groupBox1.Controls.Add(this.txtInfrastructureRoot);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtApplicationRoot);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(412, 402);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 235);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Root Paths";
            // 
            // txtContractsRoot
            // 
            this.txtContractsRoot.Location = new System.Drawing.Point(134, 184);
            this.txtContractsRoot.Multiline = true;
            this.txtContractsRoot.Name = "txtContractsRoot";
            this.txtContractsRoot.ReadOnly = true;
            this.txtContractsRoot.Size = new System.Drawing.Size(539, 45);
            this.txtContractsRoot.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(22, 199);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "Contracts";
            // 
            // frmPathSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1683, 649);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBoxConventions);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblPathHeader);
            this.Controls.Add(this.txtMappersConvention);
            this.Controls.Add(this.txtDIConvention);
            this.Controls.Add(this.txtCQRSConvention);
            this.Controls.Add(this.txtRepoInterfaceConvention);
            this.Controls.Add(this.txtRepositoryConvention);
            this.Controls.Add(this.txtControllerConvention);
            this.Controls.Add(this.txtDTOConvention);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMappers);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDI);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCQRS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRepoInterface);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRepository);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtController);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDTO);
            this.Controls.Add(this.treeViewPaths);
            this.Name = "frmPathSetup";
            this.Text = "frmPathSetup";
            ((System.ComponentModel.ISupportInitialize)(this.projectFileBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewPaths;
        private System.Windows.Forms.BindingSource projectFileBindingSource;
        private System.Windows.Forms.TextBox txtDTO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtController;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRepository;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRepoInterface;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCQRS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDI;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMappers;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtMappersConvention;
        private System.Windows.Forms.TextBox txtDIConvention;
        private System.Windows.Forms.TextBox txtCQRSConvention;
        private System.Windows.Forms.TextBox txtRepoInterfaceConvention;
        private System.Windows.Forms.TextBox txtRepositoryConvention;
        private System.Windows.Forms.TextBox txtControllerConvention;
        private System.Windows.Forms.TextBox txtDTOConvention;
        private System.Windows.Forms.Label lblPathHeader;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox richTextBoxConventions;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtInfrastructureRoot;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtApplicationRoot;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtControllersRoot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtContractsRoot;
        private System.Windows.Forms.Label label12;
    }
}