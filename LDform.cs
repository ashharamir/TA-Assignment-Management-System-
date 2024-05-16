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
    public partial class LDform : Form
    {
        int labDemonstratorID;
        public LDform(int studentID)
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            ldController.LoadDepartments(comboBox1);
            dataGridView1.DataSource = ldController.LoadSchedule(1);
            labDemonstratorID = studentID;

            //ENFORCING ALL ROW SELECTION!! MUST PUT THIS EVERYWHERE ELSE!!!
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.MultiSelect = false;
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                if (column.Name != "sectionID")
                {
                    column.ReadOnly = true; 
                }
            }

        }

       

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //dept
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDept = comboBox1.SelectedItem.ToString();
            ldController.LoadCourses(comboBox2, selectedDept);
        }
        
        //course
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = comboBox2.SelectedItem.ToString();
            Console.WriteLine("in func 1");
            dataGridView2.DataSource = ldController.GetNonClashingSections(labDemonstratorID, selectedCourse);
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LDform_Load(object sender, EventArgs e)
        {

        }

        //for sending LD form!
        private void button1_Click(object sender, EventArgs e)
        {
            string selectedCourse = comboBox2.SelectedItem?.ToString();
            string selectedDept = comboBox1.SelectedItem?.ToString();

            if (selectedCourse == null || selectedDept == null)
            {
                MessageBox.Show("Please select a course and department.");
                return;
            }

            if (!int.TryParse(dataGridView2.SelectedCells[0].Value?.ToString(), out int selectedSection))
            {
                MessageBox.Show("Please select a valid section.");
                return;
            }

            ldController.SubmitLDApplication(selectedDept, selectedCourse, selectedSection, labDemonstratorID);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
