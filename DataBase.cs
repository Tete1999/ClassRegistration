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
    public class Student2
    {
        public string user;
        public string pass;
        public string firstName;
        public string middleName;
        public string lastName;
        public string advisorUser;
        public decimal RegCredits;
        public List<string> CurrentCourses;
        public List<string> RegisteredCourses;

        public Student2(string username, string password, string first, string middle, string last, string advisor,
            decimal credits, List<string> CC, List<string> RC)
        {
            user = username;
            pass = password;
            firstName = first;
            middleName = middle;
            lastName = last;
            advisorUser = advisor;
            RegCredits = credits;
            CurrentCourses = CC;
            RegisteredCourses = RC;
        }

    }
    class DataBase
    {
        //Private Attributes
        private DataTable StudentDB;
        private DataTable FacultyDB;
        private DataTable AdminDB;
        private DataTable CourseDB;
        public DataTable CourseHistoryDB;

        ////////////// Constructor /////////////////////////////////////////////
        public DataBase(string SFA_File, string Course_File, string CSH_File)
        {
            DataTable[] tmp =  getSFA(SFA_File);
            StudentDB = tmp[0];
            FacultyDB = tmp[1];
            AdminDB = tmp[2];
            CourseDB = GetCourses(Course_File);
            CourseHistoryDB= GetCourseHistory(CSH_File);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////// Private Functions ///////////////////////////////////////////////
        private DataTable CreateStudentDB()
        {
            DataTable student = new DataTable();
            student.Columns.Add("User", typeof(string));
            student.Columns.Add("Pass", typeof(string));
            student.Columns.Add("First", typeof(string));
            student.Columns.Add("Middle", typeof(string));
            student.Columns.Add("Last", typeof(string));
            student.Columns.Add("AdvisorUser", typeof(string));
            student.Columns.Add("RegCred", typeof(decimal));
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
            CourseHistory.Columns.Add("Credit", typeof(decimal));
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
                        STU.Rows.Add(user, pass, first, middle, last, status,0.0 ,emptyList, emptyList);
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
                    for (int i=0; i < numCourses; i++)
                    {
                        course = ln.Substring(24*i, 11).Trim();
                        term = ln.Substring((24 * i) +12, 3).Trim();
                        credit = Decimal.Parse(ln.Substring((24 * i)+16, 4).Trim());
                        //grade = ln.Substring((24 * i)+21, 2).Trim();
                        grade = ln.Substring((24 * i) + 21);
                        grade = grade.Substring(0, grade.IndexOf(" ")  < 1 ? 1 : grade.IndexOf(" ")).Trim();
                        CSH.Rows.Add(user, course, term, credit, grade);
                    }
                }
            }
            
            return CSH;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////





        /////////////////////////////////////////////////////////////////////////////////////////////////
        //Returns a DataRow/ArrayList from Database from applicable function with primary key parameters/
        private DataRow getStudentRow(string username)
        {
            DataRow DR;
            string criteria = "User = '" + username + "'";
            DR = StudentDB.Select(criteria)[0];
            return DR;

        }

        public ArrayList getStudentArrayList(string username)
        {
            DataRow DR = getStudentRow(username);
            ArrayList lst = new ArrayList();
            for (int i = 0; i < DR.ItemArray.Length; i++)
                lst.Add(DR.ItemArray[i]);
            return lst;
        }

        private DataRow getFacultyRow(string username)
        {
            DataRow DR;
            string criteria = "User = '" + username + "'";
            DR = FacultyDB.Select(criteria)[0];
            return DR;

        }

        public ArrayList getFacultyArrayList(string username)
        {
            DataRow DR = getFacultyRow(username);
            ArrayList lst = new ArrayList();
            for (int i = 0; i < DR.ItemArray.Length; i++)
                lst.Add(DR.ItemArray[i]);
            return lst;
        }

        private DataRow getAdminRow(string username)
        {
            DataRow DR;
            string criteria = "User = '" + username + "'";
            DR = AdminDB.Select(criteria)[0];
            return DR;

        }

        public ArrayList getAdminArrayList(string username)
        {
            DataRow DR = getAdminRow(username);
            ArrayList lst = new ArrayList();
            for (int i = 0; i < DR.ItemArray.Length; i++)
                lst.Add(DR.ItemArray[i]);
            return lst;
        }

        private DataRow getCourseRow(string coursecode)
        {
            DataRow DR;
            string criteria = "CourseCode = '" + coursecode + "'";
            DR = CourseDB.Select(criteria)[0];
            return DR;

        }

        public ArrayList getCourseArrayList(string username)
        {
            DataRow DR = getCourseRow(username);
            ArrayList lst = new ArrayList();
            for (int i = 0; i < DR.ItemArray.Length; i++)
                lst.Add(DR.ItemArray[i]);
            return lst;
        }

        private DataRow[] getCourseHistoryRow(string username)
        {
            DataRow[] DR;
            string criteria = "User = '" + username + "'";
            DR = CourseHistoryDB.Select(criteria);
            return DR;
        }

        public ArrayList getCourseHistoryArrayList(string username)
        {
            DataRow[] DR = getCourseHistoryRow(username);
            ArrayList lst = new ArrayList();
            foreach (DataRow row in DR)
            {
                ArrayList tmp = new ArrayList();

            }
            return lst;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////




        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////
        // Get and set functions for databases given user and applicable column /////////////////////////
        public T getStudentField<T>(string user, string column)
        {
            DataRow DR = getStudentRow(user);
            T data = DR.Field<T>(column);
            return data;
        }

        public T getStudentField<T>(string user, int column)
        {
            DataRow DR = getStudentRow(user);
            T data = DR.Field<T>(column);
            return data;
        }

        public void setStudentField<T>(string user, string column, T data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            StudentDB.Rows[index].SetField(column, data);
        }

        public void setStudentField<T>(string user, int column, T data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            StudentDB.Rows[index].SetField(column, data);
        }

        public T getFacultyField<T>(string user, string column)
        {
            DataRow DR = getFacultyRow(user);
            T data = DR.Field<T>(column);
            return data;
        }

        public T getFacultyField<T>(string user, int column)
        {
            DataRow DR = getFacultyRow(user);
            T data = DR.Field<T>(column);
            return data;
        }

        public void setFacultyField<T>(string user, string column, T data)
        {
            DataRow DR = getFacultyRow(user);
            int index = FacultyDB.Rows.IndexOf(DR);
            FacultyDB.Rows[index].SetField(column, data);
        }

        public void setFacultyField<T>(string user, int column, T data)
        {
            DataRow DR = getFacultyRow(user);
            int index = FacultyDB.Rows.IndexOf(DR);
            FacultyDB.Rows[index].SetField(column, data);
        }

        public T getAdminField<T>(string user, string column)
        {
            DataRow DR = getAdminRow(user);
            T data = DR.Field<T>(column);
            return data;
        }

        public T getAdminField<T>(string user, int column)
        {
            DataRow DR = getAdminRow(user);
            T data = DR.Field<T>(column);
            return data;
        }

        public void setAdminField<T>(string user, string column, T data)
        {
            DataRow DR = getAdminRow(user);
            int index = AdminDB.Rows.IndexOf(DR);
            AdminDB.Rows[index].SetField(column, data);
        }

        public void setAdminField<T>(string user, int column, T data)
        {
            DataRow DR = getAdminRow(user);
            int index = AdminDB.Rows.IndexOf(DR);
            AdminDB.Rows[index].SetField(column, data);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////// 
        /////////////////////////////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////////////////////////////
        // Used for adding and removing strings from lists within DataBases (Current/Registered Courses)/
        public void pushCourseinStudent(string user, string column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            StudentDB.Rows[index].SetField(column, lst);
        }

        public void pushCourseinStudent(string user, int column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            StudentDB.Rows[index].SetField(column, lst);
        }

        public void removeCourseinStudent(string user, string column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            StudentDB.Rows[index].SetField(column, lst);
        }

        public void removeCourseinStudent(string user, int column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            StudentDB.Rows[index].SetField(column, lst);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////////////////////////////
        // Used for incrementing and decrementing ints and decimals in databases ////////////////////////
        public void IncrementDecrementinStudent(string user, string column, int data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            int item = DR.Field<int>(column);
            item = item + data;
            StudentDB.Rows[index].SetField(column, item);
        }

        public void IncrementDecrementinStudent(string user, int column, int data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            int item = DR.Field<int>(column);
            item = item + data;
            StudentDB.Rows[index].SetField(column, item);
        }

        public void IncrementDecrementinStudent(string user, string column, decimal data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            decimal item = DR.Field<decimal>(column);
            item = item + data;
            StudentDB.Rows[index].SetField(column, item);
        }

        public void IncrementDecrementinStudent(string user, int column, decimal data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            decimal item = DR.Field<decimal>(column);
            item = item + data;
            StudentDB.Rows[index].SetField(column, item);
        }

        public void IncrementDecrementinCourse(string coursecode, string column, int data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = CourseDB.Rows.IndexOf(DR);
            int item = DR.Field<int>(column);
            item = item + data;
            CourseDB.Rows[index].SetField(column, item);
        }

        public void IncrementDecrementinCourse(string coursecode, int column, int data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = CourseDB.Rows.IndexOf(DR);
            int item = DR.Field<int>(column);
            item = item + data;
            CourseDB.Rows[index].SetField(column, item);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////


        private bool isQuality(string grade)
        {
            List<string> letters = new List<string> { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D+", "D", "D-", "F" };
            if (letters.Contains(grade))
                return true;
            return false;
        }

        private decimal letterGradeToCredit(string grade)
        {
            switch (grade)
            {
                case "A":
                    return 4.0M;
                case "A-":
                    return 3.7M;
                case "B+":
                    return 3.3M;
                case "B":
                    return 3.0M;
                case "B-":
                    return 2.7M;
                case "C+":
                    return 2.3M;
                case "C":
                    return 2.0M;
                case "C-":
                    return 1.7M;
                case "D+":
                    return 1.2M;
                case "D":
                    return 1.0M;
                case "D-":
                    return 0.7M;
                default:
                    return 0M;
            }
        }

        public decimal[] GetStudentGPA(string user)
        {
            decimal[] lst = new decimal[3];

            decimal qualityCred = 0;
            decimal qualityPoints = 0;
            decimal totalCred = 0;
            decimal GPA = 0;

            List<string> courses = new List<string>();
            List<decimal> credits = new List<decimal>();
            List<string> grades = new List<string>();


            DataRow[] rows = getCourseHistoryRow(user);
            foreach (DataRow row in rows)
            {
                string crs = row.Field<string>("Course");
                crs = crs.Substring(0, crs.Length - 3);
                courses.Add(crs);

                decimal creds = row.Field<decimal>("Credit");
                credits.Add(creds);

                string grde = row.Field<string>("Grade");
                if (grde.Contains("R"))
                    grde = grde.Substring(1);
                grades.Add(grde);

            }

            List<string> tmp = new List<string>();
            List<string> NoCreds = new List<string>(){"U", "W", "X", "O", "I", "EQ"};

            for (int i= courses.Count()-1; i >= 0; i--)
            {
                if (!NoCreds.Contains(grades[i]))
                {
                    totalCred += credits[i];
                    if (!tmp.Contains(courses[i]))
                    {
                        tmp.Add(courses[i]);
                        if (isQuality(grades[i]))
                        {
                            qualityCred += credits[i];
                            qualityPoints += letterGradeToCredit(grades[i]) * credits[i];
                        }
                    }
                }

            }
            if (qualityCred != 0M)
                GPA = qualityPoints / qualityCred;

            lst[0] = GPA;
            lst[1] = qualityCred;
            lst[2] = totalCred;

            return lst;
        }

    }


}
