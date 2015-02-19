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
    public partial class ScheduleEditor : Form
    {
        Main_Form parentForm;
        Panel currentPanel;
        public ScheduleEditor(Main_Form passedForm)
        {
            this.parentForm = passedForm;
            InitializeComponent();
            fillComboBoxes();
            setAllPanesInvisible();
            currentPanel = shift_IUpanel;
            this.Size = new Size(800, 500);
        }

        private void ScheduleEditor_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void shiftComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = shiftComboBox.SelectedItem.ToString();
            switch (selected) {
                case "Add Shift":
                    setup_shift_add();
                    break;
                case "Update Shift":
                    setup_shift_update();
                    break;
                case "Delete Shift":
                    setup_shift_delete();
                    break;
            }
        }


       private void setup_shift_add()
        {
            focusPanel(shift_IUpanel);
            shift_IU_label1.Visible = false;
            shift_IU_textBox1.Visible = false;
            shift_IU_label4.Text = "Add Shift";
        }

        private void setup_shift_update()
        {
            focusPanel(shift_IUpanel);
            shift_IU_label1.Visible = true;
            shift_IU_textBox1.Visible = true;
            shift_IU_label4.Text = "Update Shift";

        }
        public void setup_shift_delete()
        {
            focusPanel(shift_Dpanel);
            
        }
        private void shift_add()
        {
            string[] argList = new String[2];
            argList[0] = shift_IU_textBox2.Text;
            argList[1] = shift_IU_textBox3.Text;
            
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.shift_add", argList))
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
        public void shift_update(){

            string[] argList = new String[3];
            argList[0] = shift_IU_textBox1.Text;
            argList[1] = shift_IU_textBox2.Text;
            argList[2] = shift_IU_textBox3.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.shift_update", argList))
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
        private void shift_delete()
        {
            string[] argList = new String[1];
            argList[0] = shift_Delete_TextBox1.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.shift_delete", argList))
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
        private void fillComboBoxes()
        {
            schedulesComboBox.Items.AddRange(new Object[] {
               "Add Schedule",
               "Change Schedule",
               "Delete Schedule"
            });
            jobSchedulesComboBox.Items.AddRange(new Object[] {
                "Add Job Schedule",
                "Delete Job Schedule"
            });

            shiftComboBox.Items.AddRange(new Object[] { "Add Shift", "Update Shift", "Delete Shift" });
            guardSchedulesComboBox.Items.AddRange(new Object[] { "Update Guard Schedule" });
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void schedulesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = schedulesComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add Schedule":
                    setup_schedule_add();
                    break;
                case "Change Schedule":
                    setup_schedule_change();
                    break;
                case "Delete Schedule":
                    setup_schedule_delete();
                    break;
            }
        }
        private void Schedule_IUsubmitbutton_Click(object sender, EventArgs e)
        {
            string selected = schedulesComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add Schedule":
                    schedule_add();
                    break;
                case "Change Schedule":
                    schedule_update();
                    break;
            }
        }
        private void setup_schedule_change()
        {
            focusPanel(schedules_IUpanel);
            scheduleadd_label.Visible = false;
            ScheduleID_label.Visible = true;
            ScheduleID_input.Visible = true;
            scheduleupdate_label.Visible = true;
        }
        private void setup_schedule_add()
        {
            focusPanel(schedules_IUpanel);
            scheduleupdate_label.Visible = false;
            scheduleadd_label.Visible = true;
            ScheduleID_label.Visible = false;
            ScheduleID_input.Visible = false;
        }
        private void setup_schedule_delete()
        {

            focusPanel(schedules_Dpanel);
        }
        private void schedule_add()
        {
            string[] argList = new String[7];
            if (string.IsNullOrEmpty(Sunshift_input.Text))
            {
                argList[0] = null;
            }
            else
            {
                argList[0] = Sunshift_input.Text;
            }
            if (string.IsNullOrEmpty(Monshift_input.Text))
            {
                argList[1] = null;
            }
            else
            {
                argList[1] = Monshift_input.Text;
            }
            if (string.IsNullOrEmpty(Tuesshift_input.Text))
            {
                argList[2] = null;
            }
            else
            {
                argList[2] = Tuesshift_input.Text;
            }
            if (string.IsNullOrEmpty(Wedshift_input.Text))
            {
                argList[3] = null;
            }
            else
            {
                argList[3] = Wedshift_input.Text;
            }
            if (string.IsNullOrEmpty(Thursshift_input.Text))
            {
                argList[4] = null;
            }
            else
            {
                argList[4] = Thursshift_input.Text;
            }
            if (string.IsNullOrEmpty(Frishift_input.Text))
            {
                argList[5] = null;
            }
            else
            {
                argList[5] = Frishift_input.Text;
            }
            if (string.IsNullOrEmpty(Satshift_input.Text))
            {
                argList[6] = null;
            }
            else
            {
                argList[6] = Satshift_input.Text;
            }
            try {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.schedule_insert", argList))
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
        private void schedule_update() { 
            string[] argList = new String[8];
            if(string.IsNullOrEmpty(ScheduleID_input.Text)) {
                argList[0] = null;
            }
            else {
                argList[0] = ScheduleID_input.Text;
            }
            if (string.IsNullOrEmpty(Sunshift_input.Text))
            {
                argList[1] = null;
            }
            else
            {
                argList[1] = Sunshift_input.Text;
            }
            if (string.IsNullOrEmpty(Monshift_input.Text))
            {
                argList[2] = null;
            }
            else
            {
                argList[2] = Monshift_input.Text;
            }
            if (string.IsNullOrEmpty(Tuesshift_input.Text))
            {
                argList[3] = null;
            }
            else
            {
                argList[3] = Tuesshift_input.Text;
            }
            if (string.IsNullOrEmpty(Wedshift_input.Text))
            {
                argList[4] = null;
            }else {
                argList[4] = Wedshift_input.Text;
            }
            if (string.IsNullOrEmpty(Thursshift_input.Text)){
                argList[5] = null;
            }else {
                argList[5] = Thursshift_input.Text;
            }
            if (string.IsNullOrEmpty(Frishift_input.Text))
            {
                argList[6] = null;
            }
            else
            {
                argList[6] = Frishift_input.Text;
            }
            if (string.IsNullOrEmpty(Satshift_input.Text))
            {
                argList[7] = null;
            }
            else
            {
                argList[7] = Satshift_input.Text;
            }
            try {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.schedule_update", argList))
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
        private void shift_IU_button1_Click(object sender, EventArgs e) { 
            string selected = shiftComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add Shift":
                    shift_add();
                    break;
                case "Update Shift":
                    shift_update();
                    break;
            }
        }
        private void shift_delete_button1_Click(object sender, EventArgs e)
        {
            shift_delete();
        }
        private void jobSchedulesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = jobSchedulesComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Add Job Schedule":
                    setup_jws_add();
                    break;
                case "Delete Job Schedule":
                    setup_jws_delete();
                    break;
            }
        }
        public void jws_add()
        {
            string[] argList = new String[2];
            argList[0] = jobSchedules_ScheduleID_Iinput1.Text;
            argList[1] = jobSchedules_ScheduleID_Iinput2.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.jws_add", argList))
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
        public void jws_delete()
        {
            string[] argList = new String[2];
            argList[0] = jobSchedule_scheduleID_Dinput1.Text;
            argList[1] = jobSchedule_scheduleID_Dinput2.Text;
            try
            {
               if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.jws_delete", argList))
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
        public void setup_jws_add()
        {
            focusPanel(jobSchedules_IUpanel);
        }
        public void setup_jws_delete()
        {
            focusPanel(jobSchedules_Dpanel);
        }
        private void jobSchedules_IUpanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void jobSchedule_ScheduleID_Dlabel2_Click(object sender, EventArgs e)
        {
        }
        private void jobSchedule_submitbutton_Click(object sender, EventArgs e)
        {
            jws_add();
        }
        private void jobSchedule_Dsubmitbutton_Click(object sender, EventArgs e)
        {
            jws_delete();
        }
        private void guardSchedulesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setup_guardSchedule_update();
        }
        private void guardSchedule_update()
        {
            string[] argList = new String[2];
            argList[0] = guardSchedule_scheduleID_input1.Text;
            argList[1] = guardSchedule_scheduleID_input2.Text;
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.guardSchedule_update", argList))
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
        private void setup_guardSchedule_update()
        {
            focusPanel(guardSchedules_IUpanel);
        }
        private void guardSchedule_Usubmitbutton_Click(object sender, EventArgs e)
        {
            guardSchedule_update();
        }
        private void Schedules_Dsubmitbutton_Click(object sender, EventArgs e)
        {
            string selected = schedulesComboBox.SelectedItem.ToString();
            switch (selected)
            {
                case "Delete Schedule":
                    schedule_delete();
                    break;
                
            }
        }
        private void schedule_delete()
        {
            string[] argList = new String[1];
            if (string.IsNullOrEmpty(Schedules_ScheduleID_Dinput.Text))
            {
                argList[0] = null;
            }
            else
            {
                argList[0] = Schedules_ScheduleID_Dinput.Text;
            }
            try
            {
                if (!this.parentForm.dbHandler.runParamSproc_Boolean("dbo.schedule_delete", argList))
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

        private void setAllPanesInvisible()
        {
            shift_Dpanel.Visible = false;
            shift_IUpanel.Visible = false;
            schedules_Dpanel.Visible = false;
            schedules_IUpanel.Visible = false;
            jobSchedules_IUpanel.Visible = false;
            jobSchedules_Dpanel.Visible = false;
            guardSchedules_IUpanel.Visible = false;

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
    }
}
