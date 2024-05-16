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
    public partial class TA_VacancyForm : Form
    {
        int id;
        public TA_VacancyForm(int studentID)
        {
            id = studentID;
            InitializeComponent();
            dataGridView2.DataSource = TA_VacancyController.loadVacancies();
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.MultiSelect = false;
           dataGridView2.Columns["vacancyID"].ReadOnly = true;
        }

        private void TA_VacancyForm_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = TA_VacancyController.loadVacancies();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            int vacancyID = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);
            TA_VacancyController.submitApplication(vacancyID, id, dataGridView2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
