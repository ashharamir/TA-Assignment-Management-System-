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
    public partial class demoDisplay : Form
    {
        int id;
        public demoDisplay(int studentID)
        {
            InitializeComponent();
            id = studentID;
            dataGridView2.DataSource = demoController.GetDemoSchedule(id);
        }

        private void demoDisplay_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //load main form
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
