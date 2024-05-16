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
    internal class TACommitteeController
    {
        public static DataTable LoadRequests()
        {
            DataTable scheduleTable = new DataTable();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    string query = @"
                        select requestID,studentName,courseName,applicationType, Student_requests.sectionID from Student_requests 
                        join Pending_application on Pending_application.applicationID= Student_requests.applicationID
                        join Students on Students.studentID= Student_requests.studentID
                        join Course on Course.courseID= Student_requests.courseID
                        ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //command.Parameters.AddWithValue("@studentID", studentID);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(scheduleTable);
                        }
                        Console.WriteLine("Schedule loaded successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading request schedule: " + ex.Message);
            }

            return scheduleTable;
        }
        public static void submitApplication(int requestID, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    // Retrieve the values for insertion from the Student_requests table based on the requestID
                    string selectQuery = @"
                    SELECT applicationID, courseID, studentID, sectionID
                    FROM Student_requests
                    WHERE requestID = @requestID";

                    string selectTypeQuery = @"
                    SELECT applicationType
                    FROM Pending_application
                    WHERE applicationID = (SELECT applicationID FROM Student_requests WHERE requestID = @requestID)";

                    string applicationType;
                    using (SqlCommand selectTypeCommand = new SqlCommand(selectTypeQuery, connection))
                    {
                        selectTypeCommand.Parameters.AddWithValue("@requestID", requestID);
                        applicationType = (string)selectTypeCommand.ExecuteScalar();
                    }

                    int applicationID, courseID, studentID, sectionID;
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@requestID", requestID);

                        // Execute the query to retrieve values
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                applicationID = Convert.ToInt32(reader["applicationID"]);
                                courseID = Convert.ToInt32(reader["courseID"]);
                                studentID = Convert.ToInt32(reader["studentID"]);
                                sectionID = Convert.ToInt32(reader["sectionID"]);
                            }
                            else
                            {
                                MessageBox.Show("Request ID not found.");
                                return;
                            }
                        }
                    }

                    string NameQuery = "SELECT studentName FROM Students WHERE studentID = @studentID";

                    string Name;
                    using (SqlCommand nameCommand = new SqlCommand(NameQuery, connection))
                    {
                        nameCommand.Parameters.AddWithValue("@studentID", studentID);
                        Name = (string)nameCommand.ExecuteScalar();
                    }

                    if (applicationType == "LD")
                    {
                        string insertQuery = @"
                        INSERT INTO Lab_Assistant (labAssistantName, courseID, studentID, sectionID)
                        VALUES (@labAssistantName, @courseID, @studentID, @sectionID)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            // Change the values of the parameters as needed
                            insertCommand.Parameters.AddWithValue("@labAssistantName", Name);
                            insertCommand.Parameters.AddWithValue("@courseID", courseID);
                            insertCommand.Parameters.AddWithValue("@studentID", studentID);
                            insertCommand.Parameters.AddWithValue("@sectionID", sectionID);

                            insertCommand.ExecuteNonQuery();
                        }

                        // Delete the inserted entry from the Student_requests table
                        string deleteQuery = @"
                        DELETE FROM Student_requests
                        WHERE requestID = @requestID";

                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@requestID", requestID);
                            deleteCommand.ExecuteNonQuery();
                        }
                    }else if(applicationType == "TA")
                    {
                        string insertQuery = @"
                        INSERT INTO TA (taName, courseID, studentID, sectionID)
                        VALUES (@taName, @courseID, @studentID, @sectionID)";


                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            // Change the values of the parameters as needed
                            insertCommand.Parameters.AddWithValue("@taName", Name);
                            insertCommand.Parameters.AddWithValue("@courseID", courseID);
                            insertCommand.Parameters.AddWithValue("@studentID", studentID);
                            insertCommand.Parameters.AddWithValue("@sectionID", sectionID);

                            insertCommand.ExecuteNonQuery();
                        }

                        // Delete the inserted entry from the Student_requests table
                        string deleteQuery = @"
                            DELETE FROM Student_requests
                            WHERE requestID = @requestID";  

                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@requestID", requestID);
                            deleteCommand.ExecuteNonQuery();
                        }
                    }

                    string deleteQuery_ = @"
                            DELETE FROM vacancies
                            WHERE courseID = courseID AND sectionID = sectionID";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery_, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@courseID", courseID);
                        deleteCommand.Parameters.AddWithValue("@sectionID", sectionID);
                        deleteCommand.ExecuteNonQuery();
                    }


                    // update refresh
                    dataGridView.DataSource = TACommitteeController.LoadRequests();

                    MessageBox.Show("application has been sent.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting application: " + ex.Message);
            }
        }


    }


}
