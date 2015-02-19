using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMSTest
{
    public partial class UserEditor : Form
    {
        Main_Form parentForm;
        Panel currentPanel;

        public UserEditor(Main_Form passedForm)
        {
            this.parentForm = passedForm;
            InitializeComponent();
            fillComboBoxes();
            setAllPanesInvisible();
            currentPanel = user_IUpanel;
            this.Size = new Size(800, 500);
        }
        private void fillComboBoxes()
        {
            userComboBox.Items.AddRange(new Object[] {
                "Add User", 
                "Update User", 
                "Delete User"
            });
        }
        private void setAllPanesInvisible()
        {
            user_duser_panel.Visible = false;
            user_IUpanel.Visible = false;

        }
        private void focusPanel(Panel desired)
        {
            setAllPanesInvisible();
            desired.Visible = true;
            Point oldLocation = this.currentPanel.Location;
            this.currentPanel.Location = desired.Location;
            desired.Location = oldLocation;
            this.currentPanel = desired;
        }
        
        private void prisonerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = userComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add User":
                    setup_user_add();
                    break;
                case "Update User":
                    setup_user_update();
                    break;
                case "Delete User":
                    setup_user_delete();
                    break;
            }
        }

        private void setup_user_add()
        {
            focusPanel(user_IUpanel);
            user_iu_label.Text = "Add prisoner";
            permissionLabel.Visible = false;
            permission_textbox.Visible = false;
        }

        private void setup_user_update()
        {
            focusPanel(user_IUpanel);
            user_iu_label.Text = "Update prisoner";
            permissionLabel.Visible = true;
            permission_textbox.Visible = true;
        }

        private void setup_user_delete()
        {
            focusPanel(user_duser_panel);
        }

        private void user_add()
        {
            string[] argList = new String[5];
            if (string.IsNullOrEmpty(fname_iu_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[0] = fname_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(mname_iu_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[1] = mname_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(lname_iu_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[2] = lname_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(username_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[3] = username_textbox.Text;
            }
            if (string.IsNullOrEmpty(password_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[4] = password_textbox.Text;
            }
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.pms_registerUser", argList))
                {
                    MessageBox.Show("Invalid Syntax");
                    return;
                }
                MessageBox.Show("Add Successful");
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void user_update()
        {
            string[] argList = new String[6];
            if (string.IsNullOrEmpty(fname_iu_textbox.Text))
            {
                argList[0] = null;
            }
            else
            {
                argList[0] = fname_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(mname_iu_textbox.Text))
            {
                argList[1] = null;
            }
            else
            {
                argList[1] = mname_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(lname_iu_textbox.Text))
            {
                argList[2] = null;
            }
            else
            {
                argList[2] = lname_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(username_textbox.Text))
            {
                argList[3] = null;
            }
            else
            {
                argList[3] = username_textbox.Text;
            }
            if (string.IsNullOrEmpty(password_textbox.Text))
            {
                argList[4] = null;
            }
            else
            {
                argList[4] = password_textbox.Text;
            }
            if (string.IsNullOrEmpty(permission_textbox.Text))
            {
                argList[5] = null;
            }
            else
            {
                argList[5] = permission_textbox.Text;
            }
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.pms_updateUser", argList))
                {
                    MessageBox.Show("Invalid Syntax");
                    return;
                }
                MessageBox.Show("Update Successful");
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void user_delete()
        {
            string[] argList = new String[1];
            if (string.IsNullOrEmpty(prisoner_delete_TextBox1.Text))
            {
                return;
            }
            else
            {
                argList[0] = prisoner_delete_TextBox1.Text;
            }
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.pms_deleteUser", argList))
                {
                    MessageBox.Show("Invalid Syntax");
                    return;
                }
                MessageBox.Show("Delete Successful");
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void user_iu_button_Click(object sender, EventArgs e)
        {
            string selected = userComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add User":
                    user_add();
                    break;
                case "Update User":
                    user_update();
                    break;
            }
        }

        private void prisoner_delete_button1_Click(object sender, EventArgs e)
        {
            user_delete();
        }
    }
}
