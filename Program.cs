using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
           sfa.readFile("sfa.txt", "csh.txt"); //Read in account info and course history
           List<Student> students = sfa.getStudents(); //apppend student account objects to list
           List<Admin> admin = sfa.getAdmin(); //apppend admin account objects to list
            List<Faculty> faculty = sfa.getFaculty(); //apppend student account objects to list

            // Read in Courses 
            CourseReader c = new CourseReader("courses.txt"); //read in course objects
            List<Course> courses = c.getCourses(); //apppend course objects to list

            //************ Code Tessting Area **********************//
            //Console.WriteLine(courses[0].ToString());

            //students[0].getTranscriptInfo();


            //******************************************************//


            //GUI STUFF
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(students, admin,  faculty, courses));
        }
    }
}
