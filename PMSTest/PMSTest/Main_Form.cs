using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace PMSTest
{
    public partial class Main_Form : Form
    {
        public SQLhandler dbHandler;
        public Form inputForm;
        public Form ScheduleEditorForm;
        public Form PrisonerEditorForm;
        public Main_Form()
        {
            InitializeComponent();
            dbHandler = new SQLhandler();
            //fillComboBox();
            scheduleButton.Visible = false;
            prisonerButton.Visible = false;
            if (dbHandler.isConnected())
            {
                MessageBox.Show("Connected to database");
            }
            else
            {
                MessageBox.Show("Unable to connect");
            }
            
        }

        //private void fillComboBox()
        //{
        //    comboBox1.Items.AddRange(new Object[] {
        //        "Show Altercations",
        //        "Show All Cells",
        //        "Show All Prisoners",
        //        "Show Guard Schedule",
        //        "Show All Guards",
        //        "Show All Shifts",
        //        "Show All Users",
        //        "Show Prisoners Working Jobs",
        //        "Show All Schedules",
        //        "Show Job Schedules",
        //        "Get Prisoner",
        //        "Add Altercation",
        //        "Add Cell",
        //        "Add Guard",
        //        "Add Prisoner",
        //        "Register User",
        //        "Create Job",
        //        "Delete Prisoner",
        //        "Delete User",
        //        "Move Prisoner"
                
        //    });
        //}

        //login button
        private void button1_Click(object sender, EventArgs e)
        {
            if(dbHandler.isConnected()){
                if(dbHandler.isUserLoggedIn()) {
                    MessageBox.Show("Already Logged In");
                } else {
                    if(dbHandler.verifyUsernamePassword(textBox1.Text,textBox2.Text))
                    {
                        if (dbHandler.checkUsernamePermissions(textBox1.Text) < 1)
                        {
                            MessageBox.Show("You do not have permission to use this software. \nPlease contact a warden or a guard.");
                            dbHandler.logOut();
                        }
                        else if (dbHandler.checkUsernamePermissions(textBox1.Text) == 1)
                        {
                            MessageBox.Show("Guard Login Accepted");
                            setGuardInterface();
                        }
                        else
                        {
                        MessageBox.Show("Warden Login Accepted");
                        setWardenInterface();
                      
                        

                        }
                    }
                    else 
                    {
                        MessageBox.Show("Username Or Password Incorrect");
                    }
                }
            }
            else 
            {
                MessageBox.Show("Not connected to Database");
            }
            

        }

        private void setGuardInterface()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new Object[] {
                "Show Altercations",
                "Show All Cells",
                "Show All Prisoners",
                "Show Guard Schedule",
                "Show All Guards",
                "Show All Shifts",
                "Show All Users",
                "Show Prisoners Working Jobs",
                "Show All Schedules",
                "Show Job Schedules",
                "Show Cell Histories",
                "Get Prisoner"
            });
            scheduleButton.Visible = false;
            prisonerButton.Visible = false;
        }
        private void setWardenInterface()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new Object[] {
                "Show Altercations",
                "Show All Cells",
                "Show All Prisoners",
                "Show Guard Schedule",
                "Show All Guards",
                "Show All Shifts",
                "Show All Users",
                "Show Prisoners Working Jobs",
                "Show All Schedules",
                "Show Job Schedules",
                "Show Cell Histories",
                "Get Prisoner",
                "Add Altercation",
                "Add Cell",
                "Add Guard",
                "Add Prisoner",
                "Register User",
                "Create Job",
                "Delete Prisoner",
                "Delete User",
                "Move Prisoner"
                
            });
            scheduleButton.Visible = true;
            prisonerButton.Visible = true;
        }
        private void setLoggedOutInterface()
        {
            comboBox1.Items.Clear();
            scheduleButton.Visible = false;
            prisonerButton.Visible = false;
        }



        //Depricated. Will be removed. 
        private void run_sproc(string s)
        {
            //DataTable table2 = new DataTable();
            //SqlDataReader reader;
            //using ( var command = new SqlCommand(s,cnn)
            //{ 
            //    CommandType = System.Data.CommandType.StoredProcedure 
            //} ) 
            //{
            //    reader = command.ExecuteReader(); 
            //}
            //table2.Load(reader);
            //dataGridView1.DataSource = table2;
            //reader.Close();
        }

        //logout button
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dbHandler.logOut())
            {
                MessageBox.Show("Logged Out");
                setLoggedOutInterface();
            }
            //try
            //{
            //    cnn.Close();
            //    connetionString = null;
            //    cnn = null;
            //    dataGridView1.DataSource = null;
            //    dataGridView1.Rows.Clear();
            //    dataGridView1.Refresh();    
                
            //}
            //catch (Exception ex) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = "dbo.pms_getAllPrisoners";
            run_sproc(s);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //"dbo.pms_guards";
            if(dbHandler.isUserLoggedIn())
                dataGridView1.DataSource = dbHandler.getGuardsDataTable();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "dbo.pms_getGuardSchedule";
            run_sproc(s);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            executeQuery();
        }

        public void runParamSproc(string name, string[] data){
            dataGridView1.DataSource = this.dbHandler.runParamSproc_Datatable(name, data);


         }

        private void executeQuery()
        {
            string selection = "";
            try
            {
               selection = comboBox1.SelectedItem.ToString();
            }
            catch
            {
                return;
            }
            if (!dbHandler.isUserLoggedIn())
            {
                MessageBox.Show("Please Log In First");
                return;
            }


            //This try - catch section brings up error messages if there is incorrect syntax. 
            try
            {
                switch (selection)
                {
                    case "Get Prisoner":
                        inputForm = new Add_Data_Form("dbo.pms_getPrisoner", this);
                        inputForm.Show();
                        break;
                    case "Add Cell":
                        inputForm = new Add_Data_Form("dbo.addCell", this);
                        inputForm.Show();
                        break;
                    case "Add Guard":
                        inputForm = new Add_Data_Form("dbo.addGuard", this);
                        inputForm.Show();
                        break;
                    case "Add Prisoner":
                        inputForm = new Add_Data_Form("dbo.addPrisoner", this);
                        inputForm.Show();
                        break;
                    case "Create Job":
                        inputForm = new Add_Data_Form("dbo.createJob", this);
                        inputForm.Show();
                        break;
                    case "Show Altercations":
                        dataGridView1.DataSource = dbHandler.getAltercationTable();
                        break;
                    case "Show All Cells":
                        dataGridView1.DataSource = dbHandler.getCellTable();
                        break;
                    case "Show All Prisoners":
                        dataGridView1.DataSource = dbHandler.getPrisonersTable();
                        break;
                    case "Show Guard Schedule":
                        dataGridView1.DataSource = dbHandler.runSproc("dbo.pms_getGuardSchedule");
                        break;
                    case "Show All Guards":
                        dataGridView1.DataSource = dbHandler.getGuardsDataTable();
                        break;
                    case "Show All Shifts":
                        dataGridView1.DataSource = dbHandler.getShiftsTable();
                        break;
                    case "Show All Users":
                        dataGridView1.DataSource = dbHandler.getAllUsersTable();
                        break;
                    case "Delete Prisoner":
                        inputForm = new Add_Data_Form("dbo.pms_deletePrisoner", this);
                        inputForm.Show();
                        break;
                    case "Delete User":
                        inputForm = new Add_Data_Form("dbo.pms_deleteUser", this);
                        inputForm.Show();
                        break;
                    case "Move Prisoner":
                        inputForm = new Add_Data_Form("dbo.movePrisonerToCell", this);
                        inputForm.Show();
                        break;
                    case "Register User":
                        inputForm = new Add_Data_Form("dbo.pms_registerUser", this);
                        inputForm.Show();
                        break;
                    case "Add Altercation":
                        inputForm = new Add_Data_Form("dbo.pms_addAltercation", this);
                        inputForm.Show();
                        break;
                    case "Show Prisoners Working Jobs":
                        dataGridView1.DataSource = dbHandler.runSproc("dbo.pwj_view");
                        break;
                    case "Show All Schedules":
                        dataGridView1.DataSource = dbHandler.runSproc("dbo.schedule_view");
                        break;
                    case "Show Job Schedules":
                        dataGridView1.DataSource = dbHandler.runSproc("dbo.jws_view");
                        break;
                    case "Show Cell Histories":
                        dataGridView1.DataSource = dbHandler.runSproc("dbo.pms_cellhistory_view");
                        break;



                
                
                }
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                MessageBox.Show(E.Message);
            }
            
        }

        private void scheduleButton_Click(object sender, EventArgs e)
        {
            if (!dbHandler.isUserLoggedIn())
            {
                MessageBox.Show("Please Log In First");
                return;
            }
            this.ScheduleEditorForm = new ScheduleEditor(this);
            this.ScheduleEditorForm.Show();
        }

        private void prisonerButton_Click(object sender, EventArgs e)
        {
            if (!dbHandler.isUserLoggedIn())
            {
                MessageBox.Show("Please Log In First");
                return;
            }
            this.PrisonerEditorForm = new PrisonerEditor(this);
            this.PrisonerEditorForm.Show();
        }

        
    }
}
