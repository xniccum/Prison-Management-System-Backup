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
    public partial class JobEditor : Form
    {
        Main_Form parentForm;
        public JobEditor(Main_Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] argList = new String[3];
            argList[0] = textBox1.Text;
            argList[1] = textBox2.Text;
            argList[2] = textBox3.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.createJob", argList))
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
            argList[0] = textBox4.Text;
            argList[1] = textBox5.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.job_update", argList))
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

        private void button3_Click(object sender, EventArgs e)
        {
            string[] argList = new String[1];
            argList[0] = textBox6.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.job_delete", argList))
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

        private void button4_Click(object sender, EventArgs e)
        {
            string[] argList = new String[2];
            argList[0] = textBox7.Text;
            argList[1] = textBox8.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.pwj_add", argList))
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

        private void button5_Click(object sender, EventArgs e)
        {
            string[] argList = new String[2];
            argList[0] = textBox10.Text;
            argList[1] = textBox9.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.pwj_delete", argList))
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
