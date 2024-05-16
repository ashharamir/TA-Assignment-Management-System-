using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public static class InstructorController
    {
        public static string GetInstructorName(int instructorID)
        {
            string instructorName = "";

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    string instructorQuery = "SELECT InstructorName FROM Instructors WHERE InstructorID = @instructorID";
                    using (SqlCommand instructorCommand = new SqlCommand(instructorQuery, connection))
                    {
                        instructorCommand.Parameters.AddWithValue("@instructorID", instructorID);
                        instructorName = instructorCommand.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving instructor name: " + ex.Message);
            }

            return "Welcome" + instructorName;
        }

        public static string GetInstructorInfo(int instructorID)
        {
            string instructorInfo = "";

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    string instructorQuery = "SELECT InstructorID, InstructorName FROM Instructors WHERE InstructorID = @instructorID";
                    using (SqlCommand instructorCommand = new SqlCommand(instructorQuery, connection))
                    {
                        instructorCommand.Parameters.AddWithValue("@instructorID", instructorID);
                        SqlDataReader reader = instructorCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            string insId = reader["InstructorID"].ToString();
                            string insName = reader["InstructorName"].ToString();
                            instructorInfo += $"Instructor ID: {insId}" + Environment.NewLine;
                            instructorInfo += $"Instructor Name: {insName}" + Environment.NewLine;
                        }
                        reader.Close();
                    }

                    string taQuery = "SELECT TAName, SectionID FROM TA WHERE SectionID = (SELECT SectionID FROM Instructors WHERE InstructorID = @instructorID)";
                    using (SqlCommand taCommand = new SqlCommand(taQuery, connection))
                    {
                        taCommand.Parameters.AddWithValue("@instructorID", instructorID);
                        SqlDataReader reader = taCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            string taName = reader["TAName"].ToString();
                            instructorInfo += $"TA Name: {taName}" + Environment.NewLine;
                        }
                        else
                        {
                            instructorInfo += "No TA assigned for this section.";
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving instructor information: " + ex.Message);
            }

            return instructorInfo;
        }

        public static void requestTA(int instructorID)
        {
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string query = @"
                    SELECT courseID, sectionID
                    FROM Instructors
                    WHERE instructorID = @instructorID";

                    int courseID, sectionID;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@instructorID", instructorID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                courseID = (int)reader["courseID"];
                                sectionID = (int)reader["sectionID"];
                            }
                            else
                            {
                                throw new Exception("Instructor not found.");
                            }
                        }
                    }

                    string insertQuery = @"
                    INSERT INTO vacancies (courseID, instructorID, sectionID)
                    VALUES (@courseID, @instructorID, @sectionID)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@courseID", courseID);
                        insertCommand.Parameters.AddWithValue("@sectionID", sectionID);
                        insertCommand.Parameters.AddWithValue("@instructorID", instructorID);

                        insertCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("TA request submitted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error requesting TA: " + ex.Message);
            }


        }
    }
}
