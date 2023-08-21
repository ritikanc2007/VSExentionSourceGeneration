using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.DynamicForm.CustomControl
{
    partial class MethodSettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMethodName = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtCQRSRequestName = new System.Windows.Forms.TextBox();
            this.txtEntityHidden = new System.Windows.Forms.TextBox();
            this.txtControllerAction = new System.Windows.Forms.TextBox();
            this.txtRoute = new System.Windows.Forms.TextBox();
            this.cmbHTTPAction = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblMethodName
            // 
            this.lblMethodName.AutoSize = true;
            this.lblMethodName.Font = new System.Drawing.Font("Verdana", 8F);
            this.lblMethodName.Location = new System.Drawing.Point(100, 3);
            this.lblMethodName.Name = "lblMethodName";
            this.lblMethodName.Size = new System.Drawing.Size(68, 18);
            this.lblMethodName.TabIndex = 1;
            this.lblMethodName.Text = "Method";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Verdana", 8F);
            this.checkBox1.Location = new System.Drawing.Point(3, 2);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 22);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Query?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtCQRSRequestName
            // 
            this.txtCQRSRequestName.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtCQRSRequestName.Location = new System.Drawing.Point(245, 0);
            this.txtCQRSRequestName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCQRSRequestName.Name = "txtCQRSRequestName";
            this.txtCQRSRequestName.Size = new System.Drawing.Size(199, 27);
            this.txtCQRSRequestName.TabIndex = 3;
            // 
            // txtEntityHidden
            // 
            this.txtEntityHidden.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtEntityHidden.Location = new System.Drawing.Point(450, 0);
            this.txtEntityHidden.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEntityHidden.Name = "txtEntityHidden";
            this.txtEntityHidden.Size = new System.Drawing.Size(9, 27);
            this.txtEntityHidden.TabIndex = 4;
            this.txtEntityHidden.Visible = false;
            this.txtEntityHidden.TextChanged += new System.EventHandler(this.txtEntityHidden_TextChanged);
            // 
            // txtControllerAction
            // 
            this.txtControllerAction.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtControllerAction.Location = new System.Drawing.Point(537, 3);
            this.txtControllerAction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtControllerAction.Name = "txtControllerAction";
            this.txtControllerAction.Size = new System.Drawing.Size(137, 27);
            this.txtControllerAction.TabIndex = 5;
            // 
            // txtRoute
            // 
            this.txtRoute.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtRoute.Location = new System.Drawing.Point(680, 3);
            this.txtRoute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRoute.Name = "txtRoute";
            this.txtRoute.Size = new System.Drawing.Size(90, 27);
            this.txtRoute.TabIndex = 6;
            // 
            // cmbHTTPAction
            // 
            this.cmbHTTPAction.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbHTTPAction.FormattingEnabled = true;
            this.cmbHTTPAction.Location = new System.Drawing.Point(451, 2);
            this.cmbHTTPAction.Margin = new System.Windows.Forms.Padding(4);
            this.cmbHTTPAction.Name = "cmbHTTPAction";
            this.cmbHTTPAction.Size = new System.Drawing.Size(79, 26);
            this.cmbHTTPAction.TabIndex = 7;
            // 
            // MethodSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbHTTPAction);
            this.Controls.Add(this.txtRoute);
            this.Controls.Add(this.txtControllerAction);
            this.Controls.Add(this.txtEntityHidden);
            this.Controls.Add(this.txtCQRSRequestName);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lblMethodName);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MethodSettingsControl";
            this.Size = new System.Drawing.Size(773, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label lblMethodName;
        private CheckBox checkBox1;
        private TextBox txtCQRSRequestName;
        private TextBox txtEntityHidden;
        private TextBox txtControllerAction;
        private TextBox txtRoute;
        private ComboBox cmbHTTPAction;
    }
}
