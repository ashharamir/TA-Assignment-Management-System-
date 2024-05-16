using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    internal class demoController
    {
        public static DataTable GetDemoSchedule(int studentID)
        {
            DataTable demoSchedule = new DataTable();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if(connection.State != ConnectionState.Open)
                    connection.Open();

                    /*string query = @"
                        SELECT d.demoID, d.taID, t.taName, d.venueID, v.venueName, d.StartTime, d.EndTime, d.demoDate
                        FROM Demo d
                        JOIN TA t ON d.taID = t.taID
                        JOIN Venue v ON d.venueID = v.venueID
                        WHERE d.studentID = @StudentID";*/
                    string query = @"
                    SELECT dv.demoID, dv.taID, tav.taName, dv.venueID, dv.StartTime, dv.EndTime, dv.demoDate
                    FROM DemoView dv
                    JOIN TAVenueView tav ON dv.taID = tav.taID
                    WHERE dv.studentID = @StudentID";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(demoSchedule);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error: " + ex.Message);
            }

            return demoSchedule;
        }
        public static void populateTime(ComboBox comboBox1, ComboBox comboBox2)
        {
            List<string> startTimeList = new List<string>
            {
                "8:30:00",
                "10:00:00",
                "11:30:00",
                "13:00:00",
                "14:30:00",
                "16:00:00"
            };

            List<string> endTimeList = new List<string>
            {
                "9:50:00",
                "11:20:00",
                "12:50:00",
                "14:20:00",
                "15:50:00",
                "17:20:00"
            };

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.AddRange(startTimeList.ToArray());
            comboBox2.Items.AddRange(endTimeList.ToArray());
        }

        public static void populateVenue(ComboBox comboBox)
        {
            try
            {
                comboBox.Items.Clear();
                string query = "SELECT venueName FROM venue";
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox.Items.Add(reader["venueName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ScheduleDemos(int studentID_, string venue, TimeSpan startTime, TimeSpan endTime, DateTime demoDate)
        {
            SqlConnection connection = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    // Get venueID based on venue name
                    string taIDQ = @"
                    SELECT taID
                    FROM TA
                    WHERE studentID = @studentID;
                ";
                    int TA_ID = 0;
                    using (SqlCommand command = new SqlCommand(taIDQ, connection))
                    {
                        command.Parameters.AddWithValue("@studentID", studentID_);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            TA_ID = Convert.ToInt32(result);
                        }
                    }

                    using (connection = DBConnection.GetConnection())
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        // Get venueID based on venue name
                        string vQ = @"
                SELECT venueID
                FROM venue
                WHERE venueName = @venueName;
            ";
                        int venueID = 0;
                        using (SqlCommand command = new SqlCommand(vQ, connection))
                        {
                            command.Parameters.AddWithValue("@venueName", venue);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                venueID = Convert.ToInt32(result);
                            }
                        }

                        // Get sectionID and courseID for the TA
                        int sectionID = 0;
                        int courseID = 0;
                        string sectionCourseQuery = @"
                SELECT sectionID, courseID 
                FROM TA 
                WHERE taID = @TA_ID";
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
                                    return;
                                }
                            }
                        }

                        // Get enrolled students for the TA's section and course
                        List<int> studentIDs = new List<int>();
                        string query = @"
                SELECT s.StudentID
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
                                    studentIDs.Add((int)reader["StudentID"]);
                                }
                            }
                        }

                        // Get instructorID
                        int instructorID = 0;
                        string instructorQuery = @"
                SELECT InstructorID
                FROM Instructors
                WHERE sectionID = @sectionID AND courseID = @courseID";
                        using (SqlCommand command = new SqlCommand(instructorQuery, connection))
                        {
                            command.Parameters.AddWithValue("@sectionID", sectionID);
                            command.Parameters.AddWithValue("@courseID", courseID);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                instructorID = Convert.ToInt32(result);
                            }
                        }

                        // Insert DemoSessions entry
                        string insertDemoSessionQuery = @"
                INSERT INTO DemoSessions (taID, venueID, instructorID)
                VALUES (@TA_ID, @venueID, @instructorID)";
                        using (SqlCommand command = new SqlCommand(insertDemoSessionQuery, connection))
                        {
                            command.Parameters.AddWithValue("@TA_ID", TA_ID);
                            command.Parameters.AddWithValue("@venueID", venueID);
                            command.Parameters.AddWithValue("@instructorID", instructorID);
                            command.ExecuteNonQuery();
                        }

                        // Schedule each student's demo
                        TimeSpan demoDuration = endTime - startTime;
                        int intervalMinutes = (int)Math.Floor(demoDuration.TotalMinutes / studentIDs.Count);
                        foreach (int studentID in studentIDs)
                        {
                            TimeSpan demoEndTime = startTime.Add(TimeSpan.FromMinutes(intervalMinutes));
                            string insertDemoQuery = @"
                    INSERT INTO Demo (studentID, taID, venueID, StartTime, EndTime, demoDate)
                    VALUES (@studentID, @taID, @venueID, @startTime, @endTime, @demoDate)";
                            using (SqlCommand insertCommand = new SqlCommand(insertDemoQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@studentID", studentID);
                                insertCommand.Parameters.AddWithValue("@taID", TA_ID);
                                insertCommand.Parameters.AddWithValue("@venueID", venueID);
                                insertCommand.Parameters.AddWithValue("@startTime", startTime);
                                insertCommand.Parameters.AddWithValue("@endTime", demoEndTime);
                                insertCommand.Parameters.AddWithValue("@demoDate", demoDate);
                                insertCommand.ExecuteNonQuery();
                            }
                            startTime = demoEndTime; // Update startTime for the next demo
                        }

                        MessageBox.Show("Demos scheduled successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error hereee!");
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Explicitly ensure connection is closed in the finally block
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        }
}
