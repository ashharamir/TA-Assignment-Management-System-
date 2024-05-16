using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public class StudentController
    {

        public static bool IsStudentInTATable(int studentID)
        {
            bool isInTable = false;

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM TA WHERE studentID = @StudentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentID);
                        int count = (int)command.ExecuteScalar();
                        isInTable = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return isInTable;
        }

        public static bool isStudentInLDTable(int studentID)
        {
            bool isInTable = false;

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string query = "SELECT COUNT(*) FROM Lab_Assistant WHERE studentID = @StudentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentID);
                        int count = (int)command.ExecuteScalar();
                        isInTable = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return isInTable;
        }

        public static List<Student> GetAllStudents(int instructorID)
        {
            List<Student> studentList = new List<Student>();

            try
            {
                // Open a connection
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    // Open the connection explicitly if it's not already open
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    // Query to retrieve all students enrolled with the specified instructor
                    //string query = @"
                    //    SELECT s.studentID, s.studentName, i.courseID
                    //    FROM Students s
                    //    JOIN Instructors i ON s.sectionID = i.sectionID
                    //    WHERE i.InstructorID = @instructorID";

                    string query = @"
                    SELECT s.studentID, s.studentName, i.courseID
                    FROM Students s
                    JOIN Instructors i ON s.sectionID = i.sectionID
                    WHERE i.InstructorID = @instructorID
                    GROUP BY s.studentID, s.studentName, i.courseID
                    HAVING COUNT(s.studentID) = 1;
                ";

                    // Execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter for instructor ID
                        command.Parameters.AddWithValue("@instructorID", instructorID);

                        // Execute reader to get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Iterate through the result set
                            while (reader.Read())
                            {
                                // Create a new Student object and populate its properties
                                Student student = new Student();
                                student.StudentID = (int)reader["StudentID"];
                                student.StudentName = reader["StudentName"].ToString();
                                student.CourseID = (int)reader["CourseID"];

                                // Add the Student object to the list
                                studentList.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving students: " + ex.Message);
            }

            return studentList;
        }

        public static DataTable GetStudentInformation(int studentID)
        {
            DataTable studentInfo = new DataTable();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    string query = "SELECT studentID, studentName, sectionID FROM Students WHERE studentID = @studentID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentID", studentID);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(studentInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving student information: " + ex.Message);
            }

            return studentInfo;
        }

        public static DataTable LoadSchedule(int studentID)
        {
            DataTable scheduleTable = new DataTable();

            try
            {
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    string query = @"
                        SELECT S.startTime, S.endTime, C.courseName, I.InstructorName
                        FROM Schedule S
                        JOIN CourseSchedule CS ON S.scheduleID = CS.scheduleID
                        JOIN Course C ON CS.courseID = C.courseID
                        JOIN Section SE ON CS.sectionID = SE.sectionID
                        JOIN Students ST ON SE.sectionID = ST.sectionID
                        JOIN Instructors I ON CS.sectionID = I.sectionID AND CS.courseID = I.courseID
                        WHERE ST.studentID = @studentID
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading student schedule: " + ex.Message);
            }

            return scheduleTable;
        }

    }
}
