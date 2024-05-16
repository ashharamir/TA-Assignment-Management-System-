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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //STUDENT
        private void button1_Click(object sender, EventArgs e)
        {
            studentLoginForm studentLoginForm = new studentLoginForm();
            studentLoginForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //instructor form
            instructorFormLogin instructorFormLogin = new instructorFormLogin();
            instructorFormLogin.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //adminForm
            AdminForm adm = new AdminForm();
            adm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TA_CommitteeForm tacomm = new TA_CommitteeForm();
            tacomm.Show();
            this.Hide();
        }
    }
}
