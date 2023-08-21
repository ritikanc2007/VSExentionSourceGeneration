using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1.DynamicForm.CustomControl
{
    public partial class MethodSettingsControl : UserControl
    {
        public string EntityName { get; set; } = "";

        bool isFormloading = false;
        public MethodSettingsControl(string method, string route, string httpAction)
        {


            InitializeComponent();
            lblMethodName.Text = method;
            EntityHiddenTextBox = txtEntityHidden;
            CQRSRequestNameTextBox = txtCQRSRequestName;
           
            //cmbHTTPAction.SelectedIndex = 0;
            txtControllerAction.Text = lblMethodName.Text;

            cmbHTTPAction.Items.Add("GET");
            cmbHTTPAction.Items.Add("POST");
            cmbHTTPAction.Items.Add("PUT");
            cmbHTTPAction.Items.Add("DELETE");



            cmbHTTPAction.SelectedItem= httpAction;
            txtRoute.Text = route;
            
            

            isFormloading=true;
            if (httpAction =="GET")
                checkBox1.Checked=true;
            checkBox1_CheckedChanged(this, null);
            isFormloading=false;
        }

        public TextBox EntityHiddenTextBox { get; set; }
        public TextBox CQRSRequestNameTextBox { get; set; }

        public bool IsCQRSQuery{ get { return checkBox1.Checked; } }


        // Controller Properties
        public string HttpAction { get { return cmbHTTPAction.Text; } }

        public string ControllerMethod { get { return txtControllerAction.Text; } }

        public string Route { get { return txtRoute.Text; } }

        //END
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!isFormloading)
            {
                if (checkBox1.Checked)
                {
                    txtCQRSRequestName.Text = lblMethodName.Text + EntityHiddenTextBox.Text + "Query";
                    cmbHTTPAction.SelectedItem="GET";
                }
                else
                {
                    txtCQRSRequestName.Text = lblMethodName.Text +EntityHiddenTextBox.Text+ "Command";
                    cmbHTTPAction.SelectedItem="POST";
                }
            }
        }

        private void txtEntityHidden_TextChanged(object sender, EventArgs e)
        {
            checkBox1_CheckedChanged(this, null);
        }
    }
}
