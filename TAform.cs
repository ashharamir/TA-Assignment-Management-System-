using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_PROJECT_DEMO
{
    public partial class TAform : Form
    {
        int TA_ID;
        string dept;
        public TAform(int studentID)
        {
            InitializeComponent();
            AdminController.LoadDepartments(deptComboBox);
            TA_ID = studentID;
        }


        private void deptComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDept = deptComboBox.SelectedItem.ToString();
            dept = selectedDept;
            AdminController.LoadCourses(courseComboBox, selectedDept);
        }

        private void courseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = courseComboBox.SelectedItem.ToString();
            AdminController.LoadSections(sectionComboBox, selectedCourse);
        }

        /*private void sectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = courseComboBox.SelectedItem.ToString();
            int selectedSection = Convert.ToInt32(sectionComboBox.SelectedItem);
            AdminForm.
        }*/

        //private void confirmButton_Click(object sender, EventArgs e)
        //{
        //    string selectedCourse = courseComboBox.SelectedItem.ToString();
        //    int selectedSection = Convert.ToInt32(sectionComboBox.SelectedItem);
        //    //if empty fields, show msg box
        //    if (string.IsNullOrEmpty(selectedCourse) || string.IsNullOrEmpty(selectedSection.ToString()) || dept == null)
        //    {
        //        MessageBox.Show("Please fill in all fields.");
        //        return;
        //    }
        //    TAController.SubmitTAApplication("test", selectedCourse, selectedSection, TA_ID);
        //}

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string selectedCourse = courseComboBox.SelectedItem?.ToString();
            string section = sectionComboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCourse) || string.IsNullOrEmpty(section))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(section, out int selectedSection))
            {
                MessageBox.Show("Invalid section.");
                return;
            }

            if (dept == null)
            {
                MessageBox.Show("Please select a department.");
                return;
            }

            TAController.SubmitTAApplication("test", selectedCourse, selectedSection, TA_ID);
        }


        private void TAform_Load(object sender, EventArgs e)
        {

        }

        //navigate to vacancy table
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TA_VacancyForm vacancyForm = new TA_VacancyForm(TA_ID);
            vacancyForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
