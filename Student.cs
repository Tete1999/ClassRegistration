﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private List<Course> registeredCourses;

        public Student(string user, string pass, string firstName, string middleName, string lastName, string advisor)
        {
            this.user = user.ToLower();
            this.pass = pass;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.advisor = advisor;
        }

        public string getUser() { return this.user; }
        public string getPass() { return this.pass; }
        public string getFirst() { return this.firstName; }
        public string getMiddle() { return this.middleName; }
        public string getLast() { return this.lastName; }
        public string getAdvisor() { return this.advisor; }

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


        public void setUser(string user) { this.user = user; }
        public void setPass(string pass) { this.pass = pass; }
        public void setFirst(string firstName) { this.firstName = firstName; }
        public void setMiddle(string middleName) { this.middleName = middleName; }
        public void setLast(string lastName) { this.lastName = lastName; }
        public void setAdvisor(string advisor) { this.advisor = advisor; }

        public void addCourse(Course c) { registeredCourses.Add(c); }
    }

}