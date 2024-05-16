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
    public partial class scheduleDemoForm : Form
    {
        int id;
        public scheduleDemoForm(int TA_ID)
        {
            InitializeComponent();
            id = TA_ID;
            demoController.populateTime(comboBox1, comboBox2);
            demoController.populateVenue(comboBox3);
        }

        private void scheduleDemoForm_Load(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        //submit button
        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve the selected values as strings
            string venue = comboBox3.SelectedItem.ToString(); // Assuming venue is a string
            string startTimeString = comboBox1.SelectedItem.ToString(); // Start time as string
            string endTimeString = comboBox2.SelectedItem.ToString(); // End time as string
            DateTime demoDate = monthCalendar1.SelectionStart;

            // Convert the selected time strings to TimeSpan
            TimeSpan startTime = TimeSpan.Parse(startTimeString);
            TimeSpan endTime = TimeSpan.Parse(endTimeString);



            // Call the controller method with the retrieved values
            demoController controller = new demoController();
            controller.ScheduleDemos(id, venue, startTime, endTime, demoDate);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
