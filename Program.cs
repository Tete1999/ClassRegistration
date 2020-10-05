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
<<<<<<< Updated upstream
 
=======
           sfa.readFile("C:/Users/zmorr/Source/Repos/ClassRegistration/sfa.txt");
>>>>>>> Stashed changes
           List<Student> students = sfa.getStudents();
           List<Admin> admin = sfa.getAdmin();
           List<Faculty> faculty = sfa.getFaculty();

            // Read in Courses 
<<<<<<< Updated upstream
      
=======
            CourseReader c = new CourseReader("C:/Users/zmorr/Source/Repos/ClassRegistration/courses.txt");
>>>>>>> Stashed changes
            List<Course> courses = c.getCourses();
            Console.WriteLine(courses[0].ToString());
            Console.WriteLine(courses[1].ToString()); //
            Console.WriteLine(courses[2].ToString()); //
            Console.WriteLine(courses[3].ToString());
            Console.WriteLine(courses[4].ToString());
            Console.WriteLine(courses[2].Overlap(courses[3]).ToString());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(students, admin,  faculty, courses));
        }
    }
}
