using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public static class TAController
    {
        public static void SubmitTAApplication(string selectedDept, string selectedCourse, int selectedSection, int TA_ID)
        {
            //remove these later
            //Console.WriteLine("TA application submitted for " + selectedDept + " " + selectedCourse + " " + selectedSection);
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    int courseID;
                    string courseIdQuery = "SELECT CourseID FROM Course WHERE CourseName = @courseName";
                    using (SqlCommand command = new SqlCommand(courseIdQuery, connection))
                    {
                        command.Parameters.AddWithValue("@courseName", selectedCourse);
                        courseID = (int)command.ExecuteScalar();
                    }

                    string query = @"
                        INSERT INTO Student_requests (applicationID, courseID, studentID, sectionID)
                        VALUES (2, @courseID, @studentID, @sectionID);
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@courseID", courseID);
                        command.Parameters.AddWithValue("@studentID", TA_ID);
                        command.Parameters.AddWithValue("@sectionID", selectedSection);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("TA application has been sent.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting TA application: " + ex.Message);
            }
        }

        public static void LoadDepartments(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string query = "SELECT DeptName FROM Departments";

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

                    string query = @"SELECT SectionName 
                                     FROM Section 
                                     WHERE CourseID = (SELECT CourseID FROM Course WHERE CourseName = @courseName)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@courseName", courseName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string sectionName = reader["SectionName"].ToString();
                                comboBox.Items.Add(sectionName);
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

        public static List<Student> LoadEnrolledStudents(string courseName, string sectionName)
        {
            List<Student> students = new List<Student>();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string query = @"SELECT s.StudentID, s.StudentName 
                                     FROM Students s
                                     JOIN Section sec ON s.SectionID = sec.SectionID
                                     JOIN Course c ON sec.CourseID = c.CourseID
                                     WHERE c.CourseName = @courseName AND sec.SectionName = @sectionName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@courseName", courseName);
                        command.Parameters.AddWithValue("@sectionName", sectionName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student student = new Student();
                                student.StudentID = (int)reader["StudentID"];
                                student.StudentName = reader["StudentName"].ToString();
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
