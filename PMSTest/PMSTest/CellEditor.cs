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
    public partial class CellEditor : Form
    {
        Main_Form parentForm;
        Panel currentPanel;

        public CellEditor(Main_Form passedForm)
        {
            InitializeComponent();
            this.parentForm = passedForm;
        }


        private void focusPanel(Panel desired)
        {
            desired.Visible = true;
            Point oldLocation = this.currentPanel.Location;
            this.currentPanel.Location = desired.Location;
            desired.Location = oldLocation;
            this.currentPanel = desired;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] argList = new String[2];
            argList[0] = textBox1.Text;
            argList[1] = textBox2.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.movePrisonerToCell", argList))
                {
                    MessageBox.Show("Invalid Syntax");
                    return;
                }
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                MessageBox.Show(E.Message);
                return;
            }
            MessageBox.Show("Success!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] argList = new String[2];
            argList[0] = textBox3.Text;
            argList[1] = textBox4.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.addCell", argList))
                {
                    MessageBox.Show("Invalid Syntax");
                    return;
                }
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                MessageBox.Show(E.Message);
                return;
            }
            MessageBox.Show("Success!");
        }
    }
}
