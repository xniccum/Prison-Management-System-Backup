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
    public partial class GuardEditor : Form
    {
        Main_Form parentForm;
        public GuardEditor(Main_Form parent)
        {
            InitializeComponent();
            this.parentForm = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] argList = new String[2];
            argList[0] = textBox1.Text;
            argList[1] = textBox2.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.addGuard", argList))
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

        private void button2_Click(object sender, EventArgs e)
        {
            string[] argList = new String[1];
            argList[0] = textBox3.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.guard_delete", argList))
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
