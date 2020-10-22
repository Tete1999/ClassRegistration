using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ClassRegistration
{
    public class Student
    {
        private string user;
        private string pass;
        private string firstName;
        private string middleName;
        private string lastName;
        private string advisor;
        private string courseHistory;
        private List<Course> registeredCourses;
        private string gpa;
       
        public Student()
        {
            this.user = "";
            this.pass = "";
            this.firstName = "";
            this.middleName = "";
            this.lastName = "";
            this.advisor = "";
            this.courseHistory = "";
            registeredCourses = new List<Course>();
            gpa = "";
        }

        public Student(string user, string pass, string firstName, string middleName, string lastName, string advisor,string courseHistory)
        {
            this.user = user;
            this.pass = pass;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.advisor = advisor;
            this.courseHistory = courseHistory;
            registeredCourses = new List<Course>();
            gpa = getTranscriptInfo();
        }

     
        public string getGPA()
        {
            return this.gpa;
        }
        public string getUser() { return this.user; }
        public string getPass() { return this.pass; }
        public string getFirst() { return this.firstName; }
        public string getMiddle() { return this.middleName; }
        public string getLast() { return this.lastName; }
        public string getAdvisor() { return this.advisor; }

        public string getCourseHistory() { return this.courseHistory; }

        public decimal getTotalCredit()
        {
            decimal totalCred = 0;
            List<Course> registered = getRegisteredCourses();
            foreach(Course c in registered)
            {
                totalCred += c.getCredit();
            }
            return totalCred;
        }

        public List<Course>  getRegisteredCourses() { 
            return registeredCourses; }
        public string getFullName() { return (this.firstName + " " + this.middleName + " " + this.lastName); }

        public string getCourseInfo()
        {
            string s = "";
            if (registeredCourses == null) { return "You are not registered for any courses currently\n"; }
            else
            {
                foreach (Course element in registeredCourses)
                {
                    s += element.ToString();
                    return s;
                }
            }
            return s;
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

        private bool isQuality(string grade)
        {
            List<string> letters = new List<string> { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D+", "D", "D-", "F" };
            if (letters.Contains(grade))
                return true;
            return false;
        }

        public string getTranscriptInfo()
        {
            decimal qualityCred = 0;
            decimal qualityPoints = 0;
            decimal totalCred = 0;
            decimal GPA = 0;
            if (courseHistory != "")
            {
                List<decimal> credits = new List<decimal>();
                List<string> grades = new List<string>();
                //Console.WriteLine(courseHistory);
                int numCourses = 0;
                try
                {
                    numCourses = Int32.Parse(courseHistory.Substring(0, 2).Trim());
                    courseHistory = courseHistory.Substring(2);
                    // Console.WriteLine(numCourses);
                }
                catch (System.FormatException e)
                {
                    numCourses = 0;
                }

                
                decimal credit;
                string grade;
                for (int x = 0; x != numCourses; x++)
                {
                    credit = Convert.ToDecimal(courseHistory.Substring(15 + (x * 24) - x, 5).TrimEnd());
                    grade = courseHistory.Substring(20 + (x * 24) - x, 2).TrimEnd();
                    credits.Add(credit);
                    grades.Add(grade);
                }
                for (int i = 0; i < grades.Count(); i++)
                {
                    totalCred += credits[i];
                    if (isQuality(grades[i]))
                    {
                        qualityCred += credits[i];
                        qualityPoints += letterGradeToCredit(grades[i])* credits[i];
                    }

                }
                if (qualityCred != 0M)
                    GPA = qualityPoints / qualityCred;
            }

            return "GPA:\t\t" + GPA.ToString() + "\nQuality Credits:\t" + qualityCred.ToString() + "\nTotal Credits:\t" + totalCred.ToString();
        }

        public void setUser(string user) { this.user = user; }
        public void setPass(string pass) { this.pass = pass; }
        public void setFirst(string firstName) { this.firstName = firstName; }
        public void setMiddle(string middleName) { this.middleName = middleName; }
        public void setLast(string lastName) { this.lastName = lastName; }
        public void setAdvisor(string advisor) { this.advisor = advisor; }

        public void addCourse(Course c) { registeredCourses.Add(c); }
        public void dropCourse(Course c) { registeredCourses.Remove(c); }

        public override string ToString()
        {
            return getFullName();
        }
    }

}