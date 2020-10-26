using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace ClassRegistration
{
    class DataBase
    {
        private DataTable StudentDB;
        private DataTable FacultyDB;
        private DataTable AdminDB;
        private DataTable CourseDB;
        private DataTable CourseHistoryDB;


        public DataBase(string SFA_File, string Course_File, string CSH_File)
        {
            DataTable[] tmp =  getSFA(SFA_File);
            StudentDB = tmp[0];
            FacultyDB = tmp[1];
            AdminDB = tmp[2];
            CourseDB = GetCourses(Course_File);
            CourseHistoryDB= GetCourseHistory(CSH_File);
        }

        private DataTable CreateStudentDB()
        {
            DataTable student = new DataTable();
            student.Columns.Add("User", typeof(string));
            student.Columns.Add("Pass", typeof(string));
            student.Columns.Add("First", typeof(string));
            student.Columns.Add("Middle", typeof(string));
            student.Columns.Add("Last", typeof(string));
            student.Columns.Add("AdvisorUser", typeof(string));
            student.Columns.Add("CC", typeof(List<string>));
            student.Columns.Add("RC", typeof(List<string>));
            return student;
        }
        private DataTable CreateFacultyDB()
        {
            DataTable AllFaculty = new DataTable();
            AllFaculty.Columns.Add("User", typeof(string));
            AllFaculty.Columns.Add("Pass", typeof(string));
            AllFaculty.Columns.Add("First", typeof(string));
            AllFaculty.Columns.Add("Middle", typeof(string));
            AllFaculty.Columns.Add("Last", typeof(string));
            AllFaculty.Columns.Add("AdviseeUsers", typeof(List<string>));
            AllFaculty.Columns.Add("Courses", typeof(List<string>));
            return AllFaculty;
        }

        private DataTable CreateAdminDB()
        {
            DataTable AllAdmins = new DataTable();
            AllAdmins.Columns.Add("User", typeof(string));
            AllAdmins.Columns.Add("Pass", typeof(string));
            AllAdmins.Columns.Add("First", typeof(string));
            AllAdmins.Columns.Add("Middle", typeof(string));
            AllAdmins.Columns.Add("Last", typeof(string));
            return AllAdmins;
        }

        private DataTable CreateCourseDB()
        {
            DataTable Course = new DataTable();
            Course.Columns.Add("CourseCode", typeof(string));
            Course.Columns.Add("CourseName", typeof(string));
            Course.Columns.Add("Instructor", typeof(string));
            Course.Columns.Add("Credits", typeof(decimal));
            Course.Columns.Add("SeatsAvail", typeof(int));
            Course.Columns.Add("NumBlocks", typeof(int));
            Course.Columns.Add("TimeBlocks", typeof(ArrayList));
            Course.Columns.Add("StudentsEnrolled", typeof(List<string>));
            return Course;
        }

        private DataTable CreateCourseHistoryDB()
        {
            DataTable CourseHistory = new DataTable();
            CourseHistory.Columns.Add("User", typeof(string));
            CourseHistory.Columns.Add("Course", typeof(string));
            CourseHistory.Columns.Add("Term", typeof(string));
            CourseHistory.Columns.Add("Credits", typeof(decimal));
            CourseHistory.Columns.Add("Grade", typeof(string));
            return CourseHistory;
        }
        private DataTable[] getSFA(string SFA_File)
        {
            DataTable STU = CreateStudentDB();
            DataTable FAC = CreateFacultyDB();
            DataTable ADMIN = CreateAdminDB();
            DataTable[] returnTable = new DataTable[3];

            string ln;
            string user;
            string pass;
            string first;
            string middle;
            string last;
            string status;
            List<string> emptyList = new List<string>();

            using (StreamReader file = new StreamReader(SFA_File))
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
                        FAC.Rows.Add(user,pass,first,middle,last,emptyList,emptyList);
                    }
                    else if (status == "admin")
                    {
                        ADMIN.Rows.Add(user, pass, first, middle, last);
                    }
                    else
                    {
                        STU.Rows.Add(user, pass, first, middle, last, status,emptyList, emptyList);
                    }
                }
                file.Close();
            }
            returnTable[0] = STU;
            returnTable[1] = FAC;
            returnTable[2] = ADMIN;
            return returnTable;
        }

        private ArrayList append(string tb)
        {
            ArrayList timeBlocks = new ArrayList();
            string[] tmp = tb.Split(' ');
            timeBlocks.Clear();
            foreach (string element in tmp)
            {
                timeBlocks.Add(int.Parse(element));
            }
            return timeBlocks;
        }

        private DataTable GetCourses(string Course_File)
        {
            string courseName;
            string title;
            string instructor;
            decimal credit;
            int seats;
            int numBlocks;
            List<string> emptyList = new List<string>();
            ArrayList timeBlocks;

            DataTable Courses = CreateCourseDB();
            StreamReader file = new StreamReader(Course_File);

            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                courseName = ln.Substring(0, 11);
                title = ln.Substring(11, 15);
                instructor = ln.Substring(27, 10);
                credit = decimal.Parse(ln.Substring(38, 4));
                seats = int.Parse(ln.Substring(43, 3));
                numBlocks = int.Parse(ln.Substring(47, 1));
                timeBlocks = append(ln.Substring(49).TrimEnd());
                Courses.Rows.Add(courseName, title, instructor, credit, seats, numBlocks, timeBlocks, emptyList);
            }
            file.Close();

            return Courses;
        }


        private DataTable GetCourseHistory(string CSH_File)
        {
            DataTable CSH = CreateCourseHistoryDB();

            string ln;
            string user;
            int numCourses;
            string course;
            string term;
            decimal credit;
            string grade;

            using (StreamReader file = new StreamReader(CSH_File))
            {
                while ((ln = file.ReadLine()) != null)
                {
                    user = ln.Substring(0, 9).TrimEnd().ToLower();
                    numCourses = Int32.Parse(ln.Substring(11, 2));
                    ln = " " + ln.Substring(14);
                    for (int i=0; i < numCourses-1; i++)
                    {
                        course = ln.Substring(24*i, 11).Trim();
                        term = ln.Substring((24 * i) +12, 3).Trim();
                        credit = Decimal.Parse(ln.Substring((24 * i)+16, 4).Trim());
                        grade = ln.Substring((24 * i)+21, 3).Trim();
                        CSH.Rows.Add(user, course, term, credit, grade);
                    }
                }
            }
            
            return CSH;
        }

    }


}
