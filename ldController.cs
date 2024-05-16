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
    internal class ldController
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

        public static DataTable LoadSchedule(int studentID)
        {
            DataTable scheduleTable = new DataTable();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    //string query = @"
                    //    SELECT C.courseName, S.startTime, S.endTime
                    //    FROM Schedule S
                    //    JOIN CourseSchedule CS ON S.scheduleID = CS.scheduleID
                    //    JOIN Course C ON CS.courseID = C.courseID
                    //    JOIN Section SE ON CS.sectionID = SE.sectionID
                    //    JOIN Students ST ON SE.sectionID = ST.sectionID
                    //    JOIN Instructors I ON CS.sectionID = I.sectionID AND CS.courseID = I.courseID
                    //    WHERE ST.studentID = @studentID
                    //    ORDER BY S.startTime;
                    //    ";

                    string query = @"
                    SELECT C.courseName, S.startTime, S.endTime
                    FROM Schedule S
                    JOIN CourseSchedule CS ON S.scheduleID = CS.scheduleID
                    JOIN Course C ON CS.courseID = C.courseID
                    JOIN Section SE ON CS.sectionID = SE.sectionID
                    JOIN Students ST ON SE.sectionID = ST.sectionID
                    JOIN Instructors I ON CS.sectionID = I.sectionID AND CS.courseID = I.courseID
                    WHERE ST.studentID = @studentID
                    GROUP BY C.courseName, S.startTime, S.endTime
                    HAVING COUNT(*) >= 1
                    ORDER BY S.startTime;
                ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentID", studentID);

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
                Console.WriteLine("Error loading student schedule: " + ex.Message);
            }

            return scheduleTable;
        }

        public static DataTable GetNonClashingSections(int studentID, string courseName)
        {
            DataTable nonClashingSectionsTable = new DataTable();
            //Console.WriteLine("in func");

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    //returns details of non clashing sections
                    //        string nonClashingSectionsQuery = @"
                    //            SELECT CS.SectionID, S.startTime, S.endTime
                    //            FROM CourseSchedule CS
                    //            INNER JOIN Schedule S ON CS.ScheduleID = S.ScheduleID
                    //            INNER JOIN Course C ON CS.CourseID = C.CourseID
                    //            AND NOT EXISTS (
                    //                SELECT 1
                    //                FROM Students ST
                    //                INNER JOIN CourseSchedule CS2 ON ST.SectionID = CS2.SectionID
                    //                INNER JOIN Schedule S2 ON CS2.ScheduleID = S2.ScheduleID
                    //                WHERE ST.StudentID = @studentID
                    //                AND CS2.SectionID = CS.SectionID
                    //                AND (
                    //                    (S.startTime BETWEEN S2.startTime AND S2.endTime)
                    //                    OR (S.endTime BETWEEN S2.startTime AND S2.endTime)
                    //                    OR (S2.startTime BETWEEN S.startTime AND S.endTime)
                    //                    OR (S2.endTime BETWEEN S.startTime AND S.endTime)
                    //                )
                    //            );
                    //";

                    string nonClashingSectionsQuery = @"
                    SELECT CS.SectionID, S.startTime, S.endTime
                    FROM CourseSchedule CS
                    INNER JOIN Schedule S ON CS.ScheduleID = S.ScheduleID
                    INNER JOIN Course C ON CS.CourseID = C.CourseID
                    LEFT JOIN Students ST ON ST.SectionID = CS.SectionID
                    GROUP BY CS.SectionID, S.startTime, S.endTime
                    HAVING COUNT(ST.StudentID) = 0
                        OR NOT EXISTS (
                            SELECT 1
                            FROM Students ST2
                            INNER JOIN CourseSchedule CS2 ON ST2.SectionID = CS2.SectionID
                            INNER JOIN Schedule S2 ON CS2.ScheduleID = S2.ScheduleID
                            WHERE ST2.StudentID = @studentID
                            AND CS2.SectionID = CS.SectionID
                            AND (
                                (S.startTime BETWEEN S2.startTime AND S2.endTime)
                                OR (S.endTime BETWEEN S2.startTime AND S2.endTime)
                                OR (S2.startTime BETWEEN S.startTime AND S.endTime)
                                OR (S2.endTime BETWEEN S.startTime AND S.endTime)
                            )
                        );
                ";


                    using (SqlCommand nonClashingSectionsCommand = new SqlCommand(nonClashingSectionsQuery, connection))
                    {
                        nonClashingSectionsCommand.Parameters.AddWithValue("@studentID", studentID);
                        nonClashingSectionsCommand.Parameters.AddWithValue("@courseName", courseName);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(nonClashingSectionsCommand))
                        {
                            adapter.Fill(nonClashingSectionsTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving non-clashing sections: " + ex.Message);
            }

            return nonClashingSectionsTable;
        }

        public static void SubmitLDApplication(string selectedDept, string selectedCourse, int selectedSection, int labDemonstratorID)
        {
            // Remove these later
            Console.WriteLine("LD application submitted for " + selectedDept + " " + selectedCourse + " " + selectedSection);
            MessageBox.Show("LD application has been sent.");
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
                VALUES (1, @courseID, @studentID, @sectionID);
            ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Assign values to parameters
                        command.Parameters.AddWithValue("@courseID", courseID);
                        command.Parameters.AddWithValue("@studentID", labDemonstratorID);
                        command.Parameters.AddWithValue("@sectionID", selectedSection);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("LD application has been sent.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting LD application: " + ex.Message);
            }
        }



    }
}
