using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace ClassRegistration
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           // Read in Students, Faculty, and Admin
           ReaderSFA sfa = new ReaderSFA();
           sfa.readFile("userDB.in", "historyDB.in");
           List<Student> students = sfa.getStudents();
           List<Admin> admin = sfa.getAdmin();
            List<Faculty> faculty = sfa.getFaculty();

            // Read in Courses 
            CourseReader c = new CourseReader("courseDB.in");
            List<Course> courses = c.getCourses();
            //Console.WriteLine(courses[0].ToString());
            //Console.WriteLine(courses[1].ToString()); //
            //Console.WriteLine(courses[2].ToString()); //
            //Console.WriteLine(courses[3].ToString());
            //Console.WriteLine(courses[4].ToString());
            //Console.WriteLine(courses[2].Overlap(courses[3]).ToString());

            DataBase DDD = new DataBase("sfa.txt", "courses.txt", "csh.txt");

            //foreach (DataRow dataRow in DDD.CourseHistoryDB.Rows)
            //{
            //    foreach (var item in dataRow.ItemArray)
            //        Console.WriteLine(item);
            //}
            decimal[] poop = DDD.GetStudentGPA("FLWright");

            //DataRow DR = DDD.getStudentRow("JBiden");
            //string qwert = DR.Field<string>("Pass");
            //DDD.setStudentField<string>("Jbiden", "Pass", "DickFart");
            //DR = DDD.getStudentRow("JBiden");
            //qwert = DR.Field<string>("Pass");

            ArrayList lst = new ArrayList();
            //for (int i = 0; i < DR.ItemArray.Length; i++)
            //    lst.Add(DR.ItemArray[i]);
            //lst.ToArray();
            
            //string r = new Student2().user;
            //string peepeepoopoo = lst[8].ToString();
            //DataTable student = new DataTable();
            //student.Columns.Add("ID", typeof(int));
            //student.Columns.Add("User", typeof(string));
            //student.Columns.Add("Pass", typeof(string));
            //student.Columns.Add("First", typeof(string));
            //student.Columns.Add("Middle", typeof(string));
            //student.Columns.Add("Last", typeof(string));
            //student.Columns.Add("AdvisorUser", typeof(string));
            //student.Columns.Add("CC", typeof(List<string>));
            //student.Columns.Add("RC", typeof(List<string>));
            ////Interate through students
            //List<string> ne = new List<string>{};
            //student.Rows.Add(70, "JBiden","Delaware","Joe","Robinette","Biden","BObama", ne, ne);
            //student.Rows.Add(27, "JBiden", "Delaware", "Joe", "Robinette", "Biden", "BObama", ne, ne);
            //int qw = ne.Capacity;
            //ne.Add("ppoopp");
            //DataRow[] l= student.Select("User = 'JBiden'");
            //int r = l.Length;

            ////DataTable toop = new Student().CreateStudentDB();
            ////string a = toop.Rows[0].Field<string>("Pass");

            //DataSet set = new DataSet();
            //set.Tables.Add("STU");

            ////return students
            //string a= student.Rows[0].Field<string>("Pass");
            //student.Rows[0].SetField("Pass", "DickFart");
            //a = student.Rows[0].Field<string>("Pass");

            //int b = student.Rows[0].Field<int>("ID");
            //student.Rows[0].SetField("ID", b - 1);
            //string g= student.Select()[0]["ID"].ToString();
            //b = student.Rows[0].Field<int>("ID");


            //DataTable CourseHistory = new DataTable();
            //CourseHistory.Columns.Add("User", typeof(string));
            //CourseHistory.Columns.Add("Course", typeof(string));
            //CourseHistory.Columns.Add("Term", typeof(string));
            //CourseHistory.Columns.Add("Credits", typeof(decimal));
            //CourseHistory.Columns.Add("Grade", typeof(string));

            //DataTable Course = new DataTable();
            //Course.Columns.Add("CourseCode", typeof(string));
            //Course.Columns.Add("CourseName", typeof(string));
            //Course.Columns.Add("Faculty", typeof(string));
            //Course.Columns.Add("Credits", typeof(decimal));
            //Course.Columns.Add("SeatsAvail", typeof(int));
            //Course.Columns.Add("TimeBlocks", typeof(List<int>));
            //Course.Columns.Add("StudentsEnrolled", typeof(List<string>));

            //DataTable AllFaculty = new DataTable();
            //AllFaculty.Columns.Add("ID", typeof(int));
            //AllFaculty.Columns.Add("User", typeof(string));
            //AllFaculty.Columns.Add("Pass", typeof(string));
            //AllFaculty.Columns.Add("First", typeof(string));
            //AllFaculty.Columns.Add("Middle", typeof(string));
            //AllFaculty.Columns.Add("Last", typeof(string));
            //AllFaculty.Columns.Add("AdviseeUsers", typeof(List<string>));

            //DataTable AllAdmins = new DataTable();
            //AllAdmins.Columns.Add("ID", typeof(int));
            //AllAdmins.Columns.Add("User", typeof(string));
            //AllAdmins.Columns.Add("Pass", typeof(string));
            //AllAdmins.Columns.Add("First", typeof(string));
            //AllAdmins.Columns.Add("Middle", typeof(string));
            //AllAdmins.Columns.Add("Last", typeof(string));


            //students[0].getTranscriptInfo();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(students, admin,  faculty, courses));
        }
    }
}
