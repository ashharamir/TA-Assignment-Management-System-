using System;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public partial class InstructorForm : Form
    {
        int insID;
        public InstructorForm(int instructorID)
        {
            InitializeComponent();
            LoadInstructorInfo(instructorID);
            LoadStudents(instructorID);
            insID = instructorID;
        }

        private void LoadInstructorInfo(int instructorID)
        {
            string instructorInfo = InstructorController.GetInstructorInfo(instructorID);
            instructorInfoLabel.Text = instructorInfo;
        }

        private void LoadStudents(int instructorID)
        {
            var students = StudentController.GetAllStudents(instructorID);
            dataGridView1.DataSource = students;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InstructorController.requestTA(insID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
