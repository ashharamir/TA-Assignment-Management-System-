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
    public partial class TA_CommitteeForm : Form
    {
        public TA_CommitteeForm()
        {
            InitializeComponent();
            dataGridView2.DataSource = TACommitteeController.LoadRequests();
            //THIS IS TO ENFORCE THAT ONLY THE ID IS SELECTABLE!!! MUST PUT THIS EVERYWHERE ELSE!!!
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.MultiSelect = false;
            dataGridView2.Columns["requestID"].ReadOnly = true;
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            int requestID = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);
            TACommitteeController.submitApplication(requestID, dataGridView2);
        }

        private void TA_CommitteeForm_Load(object sender, EventArgs e)
        {
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
