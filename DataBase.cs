using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Net;
using System.Windows.Forms;

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

    public class Faculty2
    {
        public string user;
        public string pass;
        public string firstName;
        public string middleName;
        public string lastName;
        public List<string> AdviseeUsers;
        public List<string> Courses;

        public Faculty2(string username, string password, string first, string middle, string last,
            List<string> CC, List<string> RC)
        {
            user = username;
            pass = password;
            firstName = first;
            middleName = middle;
            lastName = last;
            AdviseeUsers = CC;
            Courses = RC;
        }

    }

    public class Admin2
    {
        public string user;
        public string pass;
        public string firstName;
        public string middleName;
        public string lastName;

        public Admin2(string username, string password, string first, string middle, string last)
        {
            user = username;
            pass = password;
            firstName = first;
            middleName = middle;
            lastName = last;
        }

    }

    public class Course2
    {
        public string coursecode;
        public string coursename;
        public string faculty;
        public decimal credits;
        public int seats;
        ArrayList TimeBlocks;
        List<string> StudentsEnrolled;

        public Course2(string cc, string cn, string fac, decimal cred, int seat, ArrayList TB, List<string> SE)
        {
            coursecode = cc;
            coursename= cn;
            faculty = fac;
            credits = cred;
            seats = seat;
            TimeBlocks = TB;
            StudentsEnrolled= SE;
        }
    }

    public class DataBase
    {
        //Private Attributes
        public DataTable StudentDB;
        public DataTable FacultyDB;
        public DataTable AdminDB;
        public DataTable CourseDB;
        public DataTable CourseHistoryDB;

        ////////////// Constructor /////////////////////////////////////////////
        public DataBase(string SFA_File, string Course_File, string CSH_File)
        {
            DataTable[] tmp = getSFA(SFA_File);
            StudentDB = tmp[0];
            FacultyDB = tmp[1];
            AdminDB = tmp[2];
            CourseDB = GetCourses(Course_File);
            CourseHistoryDB = GetCourseHistory(CSH_File);
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
                        FAC.Rows.Add(user, pass, first, middle, last, emptyList, emptyList);
                    }
                    else if (status == "admin")
                    {
                        ADMIN.Rows.Add(user, pass, first, middle, last);
                    }
                    else
                    {
                        STU.Rows.Add(user, pass, first, middle, last, status, 0.0, emptyList, emptyList);
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
                courseName = ln.Substring(0, 11).Trim();
                title = ln.Substring(11, 15).Trim();
                instructor = ln.Substring(27, 10).Trim();
                credit = decimal.Parse(ln.Substring(38, 4));
                seats = int.Parse(ln.Substring(43, 3));
                numBlocks = int.Parse(ln.Substring(47, 1));
                timeBlocks = append(ln.Substring(49).Trim());
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
                    for (int i = 0; i < numCourses; i++)
                    {
                        course = ln.Substring(24 * i, 11).Trim();
                        term = ln.Substring((24 * i) + 12, 3).Trim();
                        credit = Decimal.Parse(ln.Substring((24 * i) + 16, 4).Trim());
                        grade = ln.Substring((24 * i)+21, 2).Trim();
                        //grade = ln.Substring((24 * i) + 21);
                        //grade = grade.Substring(0, grade.IndexOf(" ") < 1 ? 1 : grade.IndexOf(" ")).Trim();
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

        public Student2 getStudentObject(string username)
        {
            DataRow DR = getStudentRow(username);
            ArrayList lst = new ArrayList();
            string user = DR.Field<string>(0);
            string pass = DR.Field<string>(1);
            string firstName = DR.Field<string>(2);
            string middleName = DR.Field<string>(3);
            string lastName = DR.Field<string>(4);
            string advisorUser = DR.Field<string>(5);
            decimal RegCredits = DR.Field<decimal>(6);
            List<string> CurrentCourses = DR.Field<List<string>>(7);
            List<string> RegisteredCourses = DR.Field<List<string>>(8);

            return new Student2(user, pass, firstName, middleName, lastName, advisorUser,
            RegCredits, CurrentCourses, RegisteredCourses);
        }
        public Faculty2 getFacultyObject(string username)
        {
            DataRow DR = getFacultyRow(username);
            ArrayList lst = new ArrayList();
            string user = DR.Field<string>(0);
            string pass = DR.Field<string>(1);
            string firstName = DR.Field<string>(2);
            string middleName = DR.Field<string>(3);
            string lastName = DR.Field<string>(4);
            List<string> adviseeUsers = DR.Field<List<string>>(5);
            List<string> courses = DR.Field<List<string>>(6);

            return new Faculty2(user, pass, firstName, middleName, lastName, adviseeUsers, courses);
        }

        public Admin2 getAdminObject(string username)
        {
            DataRow DR = getFacultyRow(username);
            ArrayList lst = new ArrayList();
            string user = DR.Field<string>(0);
            string pass = DR.Field<string>(1);
            string firstName = DR.Field<string>(2);
            string middleName = DR.Field<string>(3);
            string lastName = DR.Field<string>(4);
            
            return new Admin2(user, pass, firstName, middleName, lastName);
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
        public string getStudentFieldString(string user, string column)
        {
            DataRow DR = getStudentRow(user);
            string data = DR.Field<string>(column);
            return data;
        }

        public List<string> getStudentFieldList(string user, string column)
        {
            DataRow DR = getStudentRow(user);
            List<string> data = new List<string>();
            foreach (string s in DR.Field<List<string>>(column))
                data.Add(s);
            return data;
        }

        public int getStudentFieldInt(string user, string column)
        {
            DataRow DR = getStudentRow(user);
            int data = DR.Field<int>(column);
            return data;
        }

        public decimal getStudentFieldDecimal(string user, string column)
        {
            DataRow DR = getStudentRow(user);
            decimal data = DR.Field<decimal>(column);
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

        public string getFacultyFieldString(string user, string column)
        {
            DataRow DR = getFacultyRow(user);
            string data = DR.Field<string>(column);
            return data;
        }

        public List<string> getFacultyFieldList(string user, string column)
        {
            DataRow DR = getFacultyRow(user);
            List<string> data = new List<string>();
            foreach (string s in DR.Field<List<string>>(column))
                data.Add(s);
            return data;
        }

        public int getFacultyFieldInt(string user, string column)
        {
            DataRow DR = getFacultyRow(user);
            int data = DR.Field<int>(column);
            return data;
        }

        public decimal getFacultyFieldDecimal(string user, string column)
        {
            DataRow DR = getFacultyRow(user);
            decimal data = DR.Field<decimal>(column);
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

        public string getAdminFieldString(string user, string column)
        {
            DataRow DR = getAdminRow(user);
            string data = DR.Field<string>(column);
            return data;
        }

        public List<string> getAdminFieldList(string user, string column)
        {
            DataRow DR = getAdminRow(user);
            List<string> data = new List<string>();
            foreach (string s in DR.Field<List<string>>(column))
                data.Add(s);
            return data;
        }
        public int getAdminFieldInt(string user, string column)
        {
            DataRow DR = getAdminRow(user);
            int data = DR.Field<int>(column);
            return data;
        }

        public decimal getAdminFieldDecimal(string user, string column)
        {
            DataRow DR = getAdminRow(user);
            decimal data = DR.Field<decimal>(column);
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

        public string getCourseFieldString(string coursecode, string column)
        {
            DataRow DR = getCourseRow(coursecode);
            string data = DR.Field<string>(column);
            return data;
        }

        public List<string> getCourseFieldList(string coursecode, string column)
        {
            DataRow DR = getCourseRow(coursecode);
            List<string> data = new List<string>();
            foreach (string s in DR.Field<List<string>>(column))
                data.Add(s);
            return data;
        }

        public int getCourseFieldInt(string coursecode, string column)
        {
            DataRow DR = getCourseRow(coursecode);
            int data = DR.Field<int>(column);
            return data;
        }

        public decimal getCourseFieldDecimal(string coursecode, string column)
        {
            DataRow DR = getCourseRow(coursecode);
            decimal data = DR.Field<decimal>(column);
            return data;
        }

        public ArrayList getCourseFieldArrayList(string coursecode, string column)
        {
            DataRow DR = getCourseRow(coursecode);
            ArrayList data = new ArrayList();
            foreach (object var in DR.Field<ArrayList>(column))
                data.Add(var);
            return data;
        }

        public void setCourseField<T>(string coursecode, string column, T data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = AdminDB.Rows.IndexOf(DR);
            CourseDB.Rows[index].SetField(column, data);
        }

        public void setCourseField<T>(string coursecode, int column, T data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = AdminDB.Rows.IndexOf(DR);
            CourseDB.Rows[index].SetField(column, data);
        }

        public List<string> getCourseHistoryFieldList(string user, string column)
        {
            List<string> lst = new List<string>();
            DataRow[] DRlst = CourseHistoryDB.Select("User = '" + user + "'");
            foreach (DataRow r in DRlst)
                lst.Add(r.Field<string>(column));
            return lst;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////// 
        /////////////////////////////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////////////////////////////
        // Used for adding and removing strings from lists within DataBases (Current/Registered Courses)/
        public void pushIteminStudent(string user, string column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            StudentDB.Rows[index].SetField(column, lst);
        }

        public void pushIteminStudent(string user, int column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            StudentDB.Rows[index].SetField(column, lst);
        }

        public void removeIteminStudent(string user, string column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            StudentDB.Rows[index].SetField(column, lst);
        }

        public void removeIteminStudent(string user, int column, string data)
        {
            DataRow DR = getStudentRow(user);
            int index = StudentDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            StudentDB.Rows[index].SetField(column, lst);
        }

        public void pushIteminFaculty(string user, string column, string data)
        {
            DataRow DR = getFacultyRow(user);
            int index = FacultyDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            FacultyDB.Rows[index].SetField(column, lst);
        }

        public void pushIteminFaculty(string user, int column, string data)
        {
            DataRow DR = getFacultyRow(user);
            int index = FacultyDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            FacultyDB.Rows[index].SetField(column, lst);
        }

        public void removeIteminFaculty(string user, string column, string data)
        {
            DataRow DR = getFacultyRow(user);
            int index = FacultyDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            FacultyDB.Rows[index].SetField(column, lst);
        }

        public void removeIteminFaculty(string user, int column, string data)
        {
            DataRow DR = getFacultyRow(user);
            int index = FacultyDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            FacultyDB.Rows[index].SetField(column, lst);
        }

        public void pushIteminCourse(string coursecode, string column, string data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = CourseDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            CourseDB.Rows[index].SetField(column, lst);
        }

        public void pushIteminCourse(string coursecode, int column, string data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = CourseDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Add(data);
            CourseDB.Rows[index].SetField(column, lst);
        }

        public void removeIteminCourse(string coursecode, string column, string data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = CourseDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            CourseDB.Rows[index].SetField(column, lst);
        }

        public void removeIteminCourse(string coursecode, int column, string data)
        {
            DataRow DR = getCourseRow(coursecode);
            int index = CourseDB.Rows.IndexOf(DR);
            List<string> lst = DR.Field<List<string>>(column);
            lst.Remove(data);
            CourseDB.Rows[index].SetField(column, lst);
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



        /////////////////////////////////////////////////////////////////////////////////////////////////
        // Functions used to interpret a students GPA ///////////////////////////////////////////////////
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
            List<string> NoCreds = new List<string>() { "U", "W", "X", "O", "I", "EQ" };

            for (int i = courses.Count() - 1; i >= 0; i--)
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
        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////Functions that prints courses and interact with timeblocks////////////////////////////////
        private static string sum_up(int target)
        {
            string code = "";
            List<int> numbers = new List<int>() { 1, 2, 4, 8, 16 };
            sum_up_recursive(numbers, target, new List<int>(), ref code);
            return code;
        }

        private static void sum_up_recursive(List<int> numbers, int target, List<int> partial, ref string code)
        {
            int s = 0;
            foreach (int x in partial) s += x;

            if (s == target)
                code = getDays(partial);

            if (s >= target)
                return;

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new List<int>();
                int n = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

                List<int> partial_rec = new List<int>(partial);
                partial_rec.Add(n);
                sum_up_recursive(remaining, target, partial_rec, ref code);
            }
        }

        private static string getDays(List<int> lst)
        {
            string str = "";
            if (lst.Contains(1))
                str += "M";
            if (lst.Contains(2))
                str += "T";
            if (lst.Contains(4))
                str += "W";
            if (lst.Contains(8))
                str += "R";
            if (lst.Contains(16))
                str += "F";
            while (str.Length <= 4)
            {
                str += " ";
            }
            return str;
        }

        private static string getTime(int t)
        {
            string AMPM = "AM";
            float time = t;
            time = time / 2;
            System.TimeSpan s = System.TimeSpan.FromSeconds(time * 3600);
            if (s.Hours >= 12)
            {
                AMPM = "PM";
                if (s.Hours > 12)
                    s = s.Subtract(System.TimeSpan.FromSeconds(12 * 3600));
            }
            string min = (s.Minutes).ToString();
            if (min.Length != 2)
                min += "0";
            string hours = (s.Hours).ToString();
            if (hours.Length != 2)
                hours = "0" + hours;
            return hours + ":" + min + AMPM;
        }

        public string getSchedule(string coursecode)
        {
            ArrayList timeBlocks = getCourseFieldArrayList(coursecode, "TimeBlocks");
            string output = "";

            foreach (int ddttl in timeBlocks)
            {
                //Console.WriteLine(ddttl);
                output += sum_up(ddttl / 1000) + getTime((ddttl / 10) % 100) + "-" + getTime(((ddttl / 10) % 100) + ((ddttl % 10))) + "  ";
            }
            return output;

        }

        public string getSchedule(ArrayList timeBlocks)
        {
            string output = "";

            foreach (int ddttl in timeBlocks)
            {
                //Console.WriteLine(ddttl);
                output += sum_up(ddttl / 1000) + getTime((ddttl / 10) % 100) + "-" + getTime(((ddttl / 10) % 100) + ((ddttl % 10))) + "  ";
            }
            return output;

        }

        private bool TimeSpan_Overlap(TimeSpan start1, TimeSpan finish1, TimeSpan start2, TimeSpan finish2)
        {
            if (start1 == start2 || finish1 == finish2)
                return true;

            if ((start1 < start2 && start2 < finish1) || (start1 < finish2 && finish2 < finish1))
                return true;

            if ((start2 < start1 && start1 < finish2) || (start2 < finish1 && finish1 < finish2))
                return true;
            else
                return false;

        }

        public bool Overlap(string coursecode1, string coursecode2)
        {
            ArrayList tb1 = getCourseFieldArrayList(coursecode1, "TimeBlocks");
            ArrayList tb2 = getCourseFieldArrayList(coursecode2, "TimeBlocks");
            foreach (int ddttl1 in tb1)
            {
                char[] dayarray1 = sum_up(ddttl1 / 1000).Trim().ToCharArray();
                foreach (int ddttl2 in tb2)
                {
                    char[] dayarray2 = sum_up(ddttl2 / 1000).Trim().ToCharArray();
                    foreach (char day1 in dayarray1)
                    {
                        TimeSpan start1 = DateTime.Parse(getTime((ddttl1 / 10) % 100)).TimeOfDay;
                        TimeSpan finish1 = DateTime.Parse(getTime(((ddttl1 / 10) % 100) + ((ddttl1 % 10)))).TimeOfDay;
                        foreach (char day2 in dayarray2)
                        {
                            if (day1 == day2)
                            {
                                TimeSpan start2 = DateTime.Parse(getTime((ddttl2 / 10) % 100)).TimeOfDay;
                                TimeSpan finish2 = DateTime.Parse(getTime(((ddttl2 / 10) % 100) + ((ddttl2 % 10)))).TimeOfDay;
                                if (TimeSpan_Overlap(start1, finish1, start2, finish2))
                                    return true;
                            }

                        }
                    }
                }
            }
            return false;
        }

        public string CourseToString(string coursecode, bool showseats = true)
        {
            DataRow DR = getCourseRow(coursecode);
            string g = DR.Field<string>("CourseName");
            string name = getCourseFieldString(coursecode, "CourseName").PadRight(12);
            string instructor = getCourseFieldString(coursecode, "Instructor").PadRight(8);
            string credits = getCourseFieldDecimal(coursecode, "Credits").ToString().PadRight(5);
            string seats = getCourseFieldInt(coursecode, "SeatsAvail").ToString().PadRight(4);
            string timeblocks = getSchedule(coursecode);

            return coursecode.PadRight(12) + name + instructor + credits  + (showseats ? seats: "") + timeblocks;
        }

        public List<string> CourseHistoryToList(string user)
        {
            List<string> lst = new List<string>();
            DataRow[] DR = CourseHistoryDB.Select("User = '" + user + "'");
            foreach(DataRow row in DR)
            {
                string coursecode = row.Field<string>("Course");
                string Term = row.Field<string>("Term");
                string Credit = row.Field<decimal>("Credit").ToString();
                string Grade = row.Field<string>("Grade");
                lst.Add(coursecode.PadRight(20) + Term.PadRight(20) + Credit.PadRight(20) + Grade);
            }
            return lst;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////
    }

}
