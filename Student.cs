namespace DB_PROJECT_DEMO
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int CourseID { get; set; }

        public Student()
        {
        }

        public Student(int studentID, string studentName, int courseID)
        {
            StudentID = studentID;
            StudentName = studentName;
            CourseID = courseID;
        }
    }
}
