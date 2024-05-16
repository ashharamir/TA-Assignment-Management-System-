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
    internal class TA_VacancyController
    {
        public static DataTable loadVacancies()
        {
            DataTable scheduleTable = new DataTable();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    string query = @"
                    SELECT v.vacancyID, c.CourseName, i.InstructorName, v.sectionID FROM vacancies v
                    JOIN Course c ON v.courseID = c.courseID
                    JOIN Instructors i ON v.InstructorID = i.InstructorID";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //command.Parameters.AddWithValue("@studentID", studentID);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(scheduleTable);
                        }
                        Console.WriteLine("Vacancies loaded successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading request schedule: " + ex.Message);
            }

            return scheduleTable;
        }
        public static void submitApplication(int vacancyID, int id, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string selectQuery = @"
                        SELECT courseID, sectionID
                        FROM vacancies
                        WHERE vacancyID = @vacancyID";

                    int courseID, sectionID;
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@vacancyID", vacancyID);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                courseID = Convert.ToInt32(reader["courseID"]);
                                sectionID = Convert.ToInt32(reader["sectionID"]);
                            }
                            else
                            {
                                MessageBox.Show("Vacancy ID not found.");
                                return;
                            }
                        }
                    }

                    string insertQuery = @"
                    INSERT INTO Student_requests (applicationID, courseID, studentID, sectionID)
                    VALUES (@applicationID, @courseID, @studentID, @sectionID)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@applicationID", 2);
                        insertCommand.Parameters.AddWithValue("@courseID", courseID);
                        insertCommand.Parameters.AddWithValue("@studentID", id);
                        insertCommand.Parameters.AddWithValue("@sectionID", sectionID);

                        insertCommand.ExecuteNonQuery();
                    }

                    // refresh
                    //dataGridView.DataSource = loadVacancies();

                    MessageBox.Show("Application submitted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting application: " + ex.Message);
            }
        }
    }
}
