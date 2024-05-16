using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public static class AdminController
    {
        public static void LoadDepartments(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    /*string query = "SELECT DeptName FROM Departments";*/
                    string query = "SELECT DeptName FROM DepartmentsView";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string deptName = reader["DeptName"].ToString();
                                comboBox.Items.Add(deptName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments: " + ex.Message);
            }
        }

        public static void LoadCourses(ComboBox comboBox, string deptName)
        {
            comboBox.Items.Clear();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    //string query = @"SELECT CourseName 
                    //                 FROM Course 
                    //                 WHERE DeptID = (SELECT DeptID FROM Departments WHERE DeptName = @deptName)";

                    string query = @"SELECT CourseName 
                                     FROM Course 
                                     WHERE DeptID = (SELECT DeptID FROM Departments WHERE DeptName = @deptName)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@deptName", deptName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string courseName = reader["CourseName"].ToString();
                                comboBox.Items.Add(courseName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message);
            }
        }

        public static void LoadSections(ComboBox comboBox, string courseName)
        {
            comboBox.Items.Clear();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    //string query = @"SELECT SectionID FROM Section";
                    string query = @"SELECT SectionID FROM SectionView";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int sectionID = (int)reader["SectionID"];
                                comboBox.Items.Add(sectionID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sections: " + ex.Message);
            }
        }

        public static List<Student> LoadEnrolledStudents(string courseName, int sectionID)
        {
            List<Student> students = new List<Student>();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string query = @"SELECT s.StudentID, s.StudentName, c.CourseID
                             FROM Students s
                             JOIN Section sec ON s.SectionID = sec.SectionID
                             JOIN CourseSchedule cs ON sec.SectionID = cs.SectionID
                             JOIN Course c ON cs.CourseID = c.CourseID
                             WHERE c.CourseName = @courseName
                             AND sec.SectionID = @sectionID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@courseName", courseName);
                        command.Parameters.AddWithValue("@sectionID", sectionID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student student = new Student();
                                student.StudentID = (int)reader["StudentID"];
                                student.StudentName = reader["StudentName"].ToString();
                                student.CourseID = (int)reader["CourseID"];
                                students.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading enrolled students: " + ex.Message);
            }

            return students;
        }

    }
}
