using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    internal class TA_DashboardController
    {
        public static string GenerateWelcomeMessage(int TA_ID)
        {
            string welcomeMessage = "";

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string query = @"SELECT taName, courseID, studentID
                             FROM TA 
                             WHERE studentID = @TA_ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TA_ID", TA_ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string taName = reader["taName"].ToString();
                                int courseID = (int)reader["courseID"];
                                int studentID = (int)reader["studentID"];
                                welcomeMessage = $"Welcome, {taName}! Course ID: {courseID}, Student ID: {studentID}";
                            }
                            else
                            {
                                MessageBox.Show("TA not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating welcome message: " + ex.Message);
            }

            return welcomeMessage;
        }



        public static List<Student> LoadEnrolledStudents(int TA_ID)
        {
            List<Student> students = new List<Student>();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    // get the section ID and course ID associated with the TA
                    //string sectionCourseQuery = @"
                    //SELECT sectionID, courseID 
                    //FROM TA 
                    //WHERE studentID = @TA_ID";

                    string sectionCourseQuery = @"
                    SELECT sectionID, courseID 
                    FROM TA 
                    WHERE studentID = @TA_ID
                    GROUP BY sectionID, courseID;
                ";


                    int sectionID, courseID;
                    using (SqlCommand sectionCourseCommand = new SqlCommand(sectionCourseQuery, connection))
                    {
                        sectionCourseCommand.Parameters.AddWithValue("@TA_ID", TA_ID);
                        using (SqlDataReader sectionCourseReader = sectionCourseCommand.ExecuteReader())
                        {
                            if (sectionCourseReader.Read())
                            {
                                sectionID = (int)sectionCourseReader["sectionID"];
                                courseID = (int)sectionCourseReader["courseID"];
                            }
                            else
                            {
                                MessageBox.Show("TA not found.");
                                return students;
                            }
                        }
                    }

                    // Query to get enrolled students frm the TA's section and course
                    string query = @"
                    SELECT s.StudentID, s.StudentName, c.CourseID
                    FROM Students s
                    JOIN Section sec ON s.SectionID = sec.SectionID
                    JOIN CourseSchedule cs ON sec.SectionID = cs.SectionID
                    JOIN Course c ON cs.CourseID = c.CourseID
                    WHERE sec.SectionID = @sectionID
                    AND c.CourseID = @courseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sectionID", sectionID);
                        command.Parameters.AddWithValue("@courseID", courseID);

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

        public static void dismissTA(int TA_ID)
        {
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string deleteQuery = @"
                    DELETE FROM TA
                    WHERE studentID = @TA_ID";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TA_ID", TA_ID);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("TA dismissed successfully.");
                }
            }
            catch (Exception ex)
            {
                //
            }
        }


    }
}
