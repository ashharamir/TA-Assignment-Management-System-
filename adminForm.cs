using System;
using System.Data;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace DB_PROJECT_DEMO
{
    public partial class AdminForm : Form
    {
        /*public AdminForm()
        {
            InitializeComponent();
            AdminController.LoadDepartments(deptComboBox);
        }
*/

        public AdminForm()
        {
            InitializeComponent();
            deptComboBox.SelectedIndexChanged += deptComboBox_SelectedIndexChanged;
            courseComboBox.SelectedIndexChanged += courseComboBox_SelectedIndexChanged;
            sectionComboBox.SelectedIndexChanged += sectionComboBox_SelectedIndexChanged;
            AdminController.LoadDepartments(deptComboBox);
        }

        private void deptComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDept = deptComboBox.SelectedItem.ToString();
            AdminController.LoadCourses(courseComboBox, selectedDept);
        }

        private void courseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("in course func");
            string selectedCourse = courseComboBox.SelectedItem.ToString();
            Console.WriteLine(selectedCourse);
            AdminController.LoadSections(sectionComboBox, selectedCourse);
        }

        private void sectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = courseComboBox.SelectedItem.ToString();
            int selectedSection = Convert.ToInt32(sectionComboBox.SelectedItem);
            string selectedDept_ = deptComboBox.SelectedItem.ToString();
            dataGridView1.DataSource = AdminController.LoadEnrolledStudents(selectedCourse, selectedSection);
            //if (string.IsNullOrEmpty(selectedCourse) || string.IsNullOrEmpty(selectedDept_))
            //{
            //    MessageBox.Show("Please select a course and section.");
            //    return;
            //}
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //load mainMenu
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
