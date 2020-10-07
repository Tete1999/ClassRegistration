using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace ClassRegistration
{
    class ReaderSFA
    {
      
        private List<Student> students = new List<Student>();
        private List<Faculty> faculty = new List<Faculty>();
        private List<Admin> admin = new List<Admin>();
        private Dictionary<string, string> courseHistory = new Dictionary<string, string>();

        public void readFile(string fname, string cshFile)
        {
            readCourseHistory(cshFile);
            string ln;
            string user;
            string pass;
            string first;
            string middle;
            string last;
            string status;
            string csh = "";
            using (StreamReader file = new StreamReader(fname))
            { 
                while ((ln = file.ReadLine()) != null)
                {
                    user = ln.Substring(0, 10).TrimEnd().ToLower();
                    pass = ln.Substring(11, 10).TrimEnd();
                    first = ln.Substring(22, 15).TrimEnd();
                    middle = ln.Substring(38, 15).TrimEnd();
                    last = ln.Substring(54, 15).TrimEnd();
                    status = ln.Substring(70).TrimEnd();
                 
                    if (status == "faculty")
                    {
                        Faculty f = new Faculty(user, pass, first, middle, last);
                        faculty.Add(f);
                    }
                    else if (status == "admin")
                    {
                        Admin a = new Admin(user, pass, first, middle, last);
                        admin.Add(a);
                    }
                    else
                    {
                        try
                        {
                            csh = courseHistory[user];
                            Student s = new Student(user, pass, first, middle, last, status, csh);
                            students.Add(s);
                        }
                        catch (KeyNotFoundException e)
                        {
                            csh = "No Course History to Show";
                            Student s = new Student(user, pass, first, middle, last, status, csh);
                            students.Add(s);
                        }
                    }
                }
                file.Close();
            }
        }

      
        public List<Student> getStudents() { return students; }
        public List<Admin> getAdmin() { return admin; }

        public List<Faculty> getFaculty() { return faculty; }

        private void readCourseHistory(string fname)
        {

            string ln;
            string user;
            int numCourses;
            string courseHist = "";
            using (StreamReader file = new StreamReader(fname))
            {
                while ((ln = file.ReadLine()) != null)
                {
                    user = ln.Substring(0, 9).TrimEnd().ToLower(); ;
                    numCourses = Int32.Parse(ln.Substring(11, 2));
                    int i = 0;
                    int start = 14;
                    int length = 22;

                    while (i != numCourses)
                    {
                        if (numCourses - i == 1)
                        {
                            courseHist += ln.Substring(start) + "\n";
                            i += 1;
                        }
                        else
                        {
                            courseHist += ln.Substring(start, length) + "\n";
                            start += 24;
                            i += 1;
                        }     
                    }
                    //Console.WriteLine(String.Format(courseHist));
                    
                    courseHistory.Add(user, courseHist);
                    courseHist = "";
                }
            }
        }


    }
}
