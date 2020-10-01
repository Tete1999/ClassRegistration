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
           sfa.readFile("C:/Users/Talal/Desktop/SE Fresh Attempt/ClassRegistration/sfa.txt");
           List<Student> students = sfa.getStudents();
           List<Admin> admin = sfa.getAdmin();
           List<Faculty> faculty = sfa.getFaculty();

            // Read in Courses 
            CourseReader c = new CourseReader("C:/Users/Talal/Desktop/SE Fresh Attempt/ClassRegistration/courses.txt");
            List<Course> courses = c.getCourses();
           

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(students, admin,  faculty, courses));
        }
    }
}
