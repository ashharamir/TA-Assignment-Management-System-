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
    public partial class TA_LandingPage : Form
    {
        int id;
        public TA_LandingPage(int TA_ID)
        {
            id = TA_ID;
            InitializeComponent();
            dataGridViewStudents.DataSource = TA_DashboardController.LoadEnrolledStudents(id);
            lblWelcome.Text = TA_DashboardController.GenerateWelcomeMessage(id);
        }

        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TA_LandingPage_Load(object sender, EventArgs e)
        {

        }

        //resign
        private void btnResignTA_Click(object sender, EventArgs e)
        {
            TA_DashboardController.dismissTA(id);
            dataGridViewStudents.DataSource = TA_DashboardController.LoadEnrolledStudents(id);
            lblWelcome.Text = TA_DashboardController.GenerateWelcomeMessage(id);
            this.Close();
        }

        private void btnScheduleDemo_Click(object sender, EventArgs e)
        {
            //demoform
            scheduleDemoForm demoForm = new scheduleDemoForm(id);
            demoForm.Show();
        }
    }
}
