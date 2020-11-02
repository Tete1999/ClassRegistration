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
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            // Read in Students, Faculty, and Admin
            ReaderSFA sfa = new ReaderSFA();
            sfa.readFile("userDB.in", "historyDB.in");
            List<Student> students = sfa.getStudents();
            List<Admin> admin = sfa.getAdmin();
            List<Faculty> faculty = sfa.getFaculty();

            // Read in Courses 
            CourseReader c = new CourseReader("courseDB.in");
            List<Course> courses = c.getCourses();
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////



            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            DataBase DDD = new DataBase("UserDB.in", "courseDB.in", "historyDB.in");

            string qe = DDD.getStudentFieldString("jbiden", "User");
            qe = "Poopdick";
            qe = DDD.getStudentFieldString("jbiden", "User");
            string g = DDD.CourseToString("MTH-145-00");
            Console.WriteLine(g);
            g = g.Substring(0, g.IndexOf(" "));
            Console.WriteLine(g);
            g = DDD.CourseToString("CS-435-01");
            g.Substring(0, g.IndexOf(" "));
            Console.WriteLine(g);
            g = DDD.CourseToString("CS-125-00");
            Console.WriteLine(g);
            g = DDD.CourseToString("ART-123-00");
            Console.WriteLine(g);
            bool tf = DDD.Overlap("CS-435-02", "MTH-145-00");
            //foreach (DataRow dataRow in DDD.CourseHistoryDB.Rows)
            //{
            //    foreach (var item in dataRow.ItemArray)
            //        Console.WriteLine(item);
            //}
            Student2 biden = DDD.getStudentObject("jbiden");
            decimal[] poop = DDD.GetStudentGPA("FLWright");
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(ref DDD));
        }
    }
}
