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
            ((System.ComponentModel.ISupportInitialize)(this.projectFileBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewPaths
            // 
            this.treeViewPaths.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.projectFileBindingSource, "Name", true));
            this.treeViewPaths.Location = new System.Drawing.Point(13, 13);
            this.treeViewPaths.Name = "treeViewPaths";
            this.treeViewPaths.Size = new System.Drawing.Size(393, 425);
            this.treeViewPaths.TabIndex = 0;
            // 
            // projectFileBindingSource
            // 
            this.projectFileBindingSource.DataSource = typeof(ToolWindow.Models.ProjectFile);
            // 
            // txtDTO
            // 
            this.txtDTO.Location = new System.Drawing.Point(539, 19);
            this.txtDTO.Multiline = true;
            this.txtDTO.Name = "txtDTO";
            this.txtDTO.Size = new System.Drawing.Size(660, 39);
            this.txtDTO.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(490, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "DTO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Controller";
            // 
            // txtController
            // 
            this.txtController.Location = new System.Drawing.Point(539, 66);
            this.txtController.Multiline = true;
            this.txtController.Name = "txtController";
            this.txtController.Size = new System.Drawing.Size(660, 45);
            this.txtController.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(447, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repository";
            // 
            // txtRepository
            // 
            this.txtRepository.Location = new System.Drawing.Point(539, 119);
            this.txtRepository.Multiline = true;
            this.txtRepository.Name = "txtRepository";
            this.txtRepository.Size = new System.Drawing.Size(660, 45);
            this.txtRepository.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(416, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Repo Interface";
            // 
            // txtRepoInterface
            // 
            this.txtRepoInterface.Location = new System.Drawing.Point(539, 172);
            this.txtRepoInterface.Multiline = true;
            this.txtRepoInterface.Name = "txtRepoInterface";
            this.txtRepoInterface.Size = new System.Drawing.Size(660, 45);
            this.txtRepoInterface.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(477, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "CQRS";
            // 
            // txtCQRS
            // 
            this.txtCQRS.Location = new System.Drawing.Point(539, 225);
            this.txtCQRS.Multiline = true;
            this.txtCQRS.Name = "txtCQRS";
            this.txtCQRS.Size = new System.Drawing.Size(660, 45);
            this.txtCQRS.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(506, 289);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "DI";
            // 
            // txtDI
            // 
            this.txtDI.Location = new System.Drawing.Point(539, 278);
            this.txtDI.Multiline = true;
            this.txtDI.Name = "txtDI";
            this.txtDI.Size = new System.Drawing.Size(660, 45);
            this.txtDI.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(461, 341);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Mappers";
            // 
            // txtMappers
            // 
            this.txtMappers.Location = new System.Drawing.Point(539, 331);
            this.txtMappers.Multiline = true;
            this.txtMappers.Name = "txtMappers";
            this.txtMappers.Size = new System.Drawing.Size(660, 45);
            this.txtMappers.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(883, 398);
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
            this.btnCancel.Location = new System.Drawing.Point(1047, 398);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 40);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmPathSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 450);
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
    }
}