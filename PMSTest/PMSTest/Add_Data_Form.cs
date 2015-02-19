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
    public partial class Add_Data_Form : Form
    {
        string sprocName;
        Main_Form main;
        int numberOfArguments;
        public Add_Data_Form(string sprocName, Main_Form main)

        {
            this.sprocName = sprocName;
            this.numberOfArguments = main.dbHandler.parameterNames[sprocName].Length;
            this.main = main;
            InitializeComponent();
            label1.Text = sprocName;
            fillLabels();


        }

        private void fillLabels()
        {
            if (1 <= numberOfArguments)
                label2.Text = this.main.dbHandler.parameterNames[sprocName][0];
            if (2 <= numberOfArguments)
                label3.Text = this.main.dbHandler.parameterNames[sprocName][1];
            if (3 <= numberOfArguments)
                label4.Text = this.main.dbHandler.parameterNames[sprocName][2];
            if (4 <= numberOfArguments)
                label5.Text = this.main.dbHandler.parameterNames[sprocName][3];
            if (5 <= numberOfArguments)
                label6.Text = this.main.dbHandler.parameterNames[sprocName][4];
        }
        private string[] getInputList()
        {
            string[] argList = new string[5];
            argList[0] = textBox1.Text;
            argList[1] = textBox2.Text;
            argList[2] = textBox3.Text;
            argList[3] = textBox4.Text;
            argList[4] = textBox5.Text;

            return argList;
            //if (1 <= numberOfArguments)
            //if (2 <= numberOfArguments)
            //if (3 <= numberOfArguments)
            //if (4 <= numberOfArguments)
            //if (5 <= numberOfArguments)
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void AddSubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                main.runParamSproc(this.sprocName, getInputList());
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                MessageBox.Show(E.Message);
            }
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
