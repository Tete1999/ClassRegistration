using System;
using System.IO;
using System.Collections.Generic;

namespace ClassRegistration
{
    class ReaderSFA
    {
        private string user;
        private string pass;
        private string first;
        private string middle;
        private string last;
        private string status;
        private List<Student> students = new List<Student>();
        private List<Faculty> faculty = new List<Faculty>();
        private List<Admin> admin = new List<Admin>();

        public void readFile(string fname)
        {
            using (StreamReader file = new StreamReader(fname))
            {

                string ln;
               
                while ((ln = file.ReadLine()) != null)
                {
                    user = ln.Substring(0, 10).TrimEnd(); 
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
                        Student s = new Student(user, pass, first, middle, last, status);
                        students.Add(s);
                    }
                }
                file.Close();
            }
        }

        public List<Student> getStudents() { return students; }
        public List<Admin> getAdmin() { return admin; }

        public List<Faculty> getFaculty() { return faculty; }


    }
}
