using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMSTest
{
    public partial class PrisonerEditor : Form
    {
        Main_Form parentForm;
        Panel currentPanel;
        public PrisonerEditor(Main_Form passedForm)
        {
            this.parentForm = passedForm;
            InitializeComponent();
            fillComboBoxes();
            setAllPanesInvisible();
            currentPanel = prisoner_IUpanel;
            this.Size = new Size(800, 500);
            
        }
        private void fillComboBoxes()
        {
            prisonerComboBox.Items.AddRange(new Object[] {
                "Add prisoner", 
                "Update prisoner", 
                "Delete prisoner"
            });
        }
        private void setAllPanesInvisible()
        {
            prisoner_dprisoner_panel.Visible = false;
            prisoner_IUpanel.Visible = false;

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
            string selected = prisonerComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add prisoner":
                    setup_prisoner_add();
                    break;
                case "Update prisoner":
                    setup_prisoner_update();
                    break;
                case "Delete prisoner":
                    setup_prisoner_delete();
                    break;
            }
        }
        private void setup_prisoner_add()
        {
            focusPanel(prisoner_IUpanel);
            prisoner_iu_label.Text = "Add prisoner";
            prisonerID_insert_label.Visible = false;
            prisonerID_iu_textbox.Visible = false;


        }
        private void setup_prisoner_update()
        {
            focusPanel(prisoner_IUpanel);
            prisoner_iu_label.Text = "Update prisoner";
            prisonerID_insert_label.Visible = true;
            prisonerID_iu_textbox.Visible = true;
        }
        private void setup_prisoner_delete()
        {
            focusPanel(prisoner_dprisoner_panel);

        }

        private void prisoner_iu_button_Click(object sender, EventArgs e)
        {
            string selected = prisonerComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add prisoner":
                    prisoner_add();
                    break;
                case "Update prisoner":
                    prisoner_update();
                    break;
            }
        }
        private void prisoner_add()
        {
            string[] argList = new String[6];
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
            if (string.IsNullOrEmpty(crime_iu_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[3] = crime_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(crimedescription_iu_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[4] = crimedescription_iu_textbox.Text;
            }
            if (string.IsNullOrEmpty(prisoner_cell_textbox.Text))
            {
                MessageBox.Show("Please Fill in all entry boxes.");
            }
            else
            {
                argList[5] = prisoner_cell_textbox.Text;
            }
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.addPrisoner", argList))
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
        private void prisoner_update()
        {

        }
    }
}
