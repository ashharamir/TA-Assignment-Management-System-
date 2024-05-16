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
    public partial class studentLoginForm : Form
    {
        public studentLoginForm()
        {
            InitializeComponent();
        }

        private void btnStudentLogin_Click(object sender, EventArgs e)
        {
            string studentIDText = txtStudentID.Text.Trim();
            string password = txtStudentPassword.Text;

            if (string.IsNullOrEmpty(studentIDText) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Student ID and Password");
                return;
            }

            if (!int.TryParse(studentIDText, out int studentID))
            {
                MessageBox.Show("Invalid Student ID");
                return;
            }

            if (ValidateStudentLogin(studentID, password))
            {
                studentForm studentForm = new studentForm(studentID);
                studentForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Student ID or Password");
            }
        }


        private bool ValidateStudentLogin(int studentID, string password)
        {
            string query = "SELECT COUNT(*) FROM studentLogin WHERE studentID = @StudentID AND password = @Password";
            using (SqlConnection connection = DBConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@Password", password);


                if (connection.State != ConnectionState.Open)
                    connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0; 
            }

        }

        private void studentLoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
