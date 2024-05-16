using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public partial class instructorFormLogin : Form
    {
        public instructorFormLogin()
        {
            InitializeComponent();
        }

        private void btnInstructorLogin_Click(object sender, EventArgs e)
        {
            string instructorIDText = txtInstructorID.Text.Trim();
            string password = txtInstructorPassword.Text;

            if (string.IsNullOrEmpty(instructorIDText) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Instructor ID and Password");
                return;
            }

            if (!int.TryParse(instructorIDText, out int instructorID))
            {
                MessageBox.Show("Invalid Instructor ID");
                return;
            }

            if (ValidateInstructorLogin(instructorID, password))
            {
                InstructorForm instructorForm = new InstructorForm(instructorID);
                instructorForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Instructor ID or Password");
            }
        }


        private bool ValidateInstructorLogin(int instructorID, string password)
        {
            string query = "SELECT COUNT(*) FROM facultyLogin WHERE instructorID = @instructorID AND password = @Password";
            using (SqlConnection connection = DBConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@instructorID", instructorID);
                command.Parameters.AddWithValue("@Password", password);


                if (connection.State != ConnectionState.Open)
                    connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }

        }

        private void instructorFormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
