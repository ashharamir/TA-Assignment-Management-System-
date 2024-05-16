using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public partial class studentForm : Form
    {
        int studID;
        public studentForm(int studentID)
        {
            studID = studentID;
            InitializeComponent();
            LoadStudentInformation(studID);
            LoadStudentSchedule(studID);

            //enable dashboard if ta

            bool isInTATable = StudentController.IsStudentInTATable(studID);
            button5.Enabled = isInTATable;

            //disable button 1 if student is in TA table
            bool isTA = StudentController.IsStudentInTATable(studID);
            bool isLD = StudentController.isStudentInLDTable(studID);

            button1.Enabled = !isTA && !isLD;

            //disbale button 2 if student is in LD table
            
            button2.Enabled = !isLD && !isTA;

            if (isTA)
            {
                label5.Text = "TA";
            }
            else { label5.Text = "LD";}

        }

        private void LoadStudentInformation(int studID)
        {
            DataTable studentInfo = StudentController.GetStudentInformation(studID);
            if (studentInfo.Rows.Count > 0)
            {
                StudID.Text = studentInfo.Rows[0]["studentID"].ToString();
                StudName.Text = studentInfo.Rows[0]["studentName"].ToString();
                StudSection.Text = studentInfo.Rows[0]["sectionID"].ToString();
            }
        }

        private void LoadStudentSchedule(int studID)
        {
            dataGridView1.DataSource = StudentController.LoadSchedule(studID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ta
            TAform taForm = new TAform(studID);
            taForm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ld
            LDform ldform = new LDform(studID);
            ldform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }

        private void studentForm_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //load ta dashboard
            TA_LandingPage taDashboard = new TA_LandingPage(studID);
            taDashboard.Show();
            LoadStudentInformation(studID);
            LoadStudentSchedule(studID);
            bool isInTATable = StudentController.IsStudentInTATable(studID);
            button5.Enabled = isInTATable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //load demo display
            demoDisplay demoDisplay = new demoDisplay(studID);
            demoDisplay.Show();
        }
    }
}
