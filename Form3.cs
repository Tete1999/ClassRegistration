﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form3 : Form
    {
        private DataBase DDD;
        private string user;


        public Form3(ref DataBase master, string student)
        {
            this.DDD = master;
            this.user = student;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
            //listBox1.DataSource = courses;
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
            //listBox1.

            listBox3.DataSource = null;
            List<string> lst2 = new List<string>();
            foreach (string s in DDD.getStudentFieldList(user, "RC"))
            {
                lst2.Add(DDD.CourseToString(s, false));
            }
            listBox3.Items.Clear();
            listBox3.DataSource = lst2;
            //listBox3.DataSource = user.
            //List<string> seats = seatsData(courses);

            listBox2.DataSource = DDD.CourseDB;
            listBox2.DisplayMember = "SeatsAvail";

        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string course = listBox1.SelectedItem.ToString();
            course = course.Substring(0, course.IndexOf(" "));
            List<string> regcourses = new List<string>(DDD.getStudentFieldList(user, "RC"));
            for (int i = 0; i < regcourses.Count; i++)
            {
                regcourses[i] = regcourses[i].Substring(0, regcourses[i].Length - 3);
            }
            if (regcourses.Contains(course.Substring(0, course.Length - 3)))
            {
                MessageBox.Show("Error: Course Already Registered");
                flag = true;
            }
            else if (!DDD.HasPrequisitesFullfilled(user, course))
            {
                string prereqcourses = "";

                foreach (string r in DDD.getPrequisites(course))
                    prereqcourses += r + "  ";
                MessageBox.Show("Error: Following are prerequisites: " + prereqcourses);
                flag = true;
            }
            else if (!(DDD.getStudentFieldDecimal(user, "RegCred") + DDD.getCourseFieldDecimal(course, "Credits") <= 5.0M))
            {
                MessageBox.Show("Error: Credit Limit Exceeded");
                flag = true;
            }
            else if (DDD.getCourseFieldInt(course, "SeatsAvail") < 1)
                MessageBox.Show("Error: Class is Full");
            if (!flag && DDD.getCourseFieldInt(course, "SeatsAvail") >= 1)
            {
                //Check For Time Overlaps
                regcourses = new List<string>(DDD.getStudentFieldList(user, "RC"));
                List<string> check = new List<string>(DDD.getStudentFieldList(user, "RC"));
                foreach (string rc in regcourses)
                {
                    if (DDD.Overlap(course, rc))
                        MessageBox.Show("Warning: " + course + " has a time overlap with " + rc);
                }

                //Check For Course Previously Taken
                List<string> CourseHist = new List<string>(DDD.getCourseHistoryFieldList(user, "Course"));
                for (int i = 0; i < CourseHist.Count; i++)
                {
                    CourseHist[i] = CourseHist[i].Substring(0, CourseHist[i].Length - 3);
                }
                List<string> CurrCourses = DDD.getStudentFieldList(user, "CC");
                for (int i = 0; i < CurrCourses.Count; i++)
                {
                    CurrCourses[i] = CurrCourses[i].Substring(0, CurrCourses[i].Length - 3);
                }
                if (CourseHist.Contains(course.Substring(0, course.Length - 3)) || CurrCourses.Contains(course.Substring(0, course.Length - 3)))
                    MessageBox.Show("Warning: Course Taken Before");

                //Add Course
                DDD.IncrementDecrementinStudent(user, "RegCred", DDD.getCourseFieldDecimal(course, "Credits"));
                DDD.pushIteminStudent(user, "RC", course);
                DDD.IncrementDecrementinCourse(course, "SeatsAvail", -1);
                DDD.pushIteminCourse(course, "StudentsEnrolled", user);
                //List<string> cccc = new List<string>(DDD.getCourseFieldList(course, "StudentsEnrolled"));
            }
            listBox3.DataSource = null;
            List<string> lst2 = new List<string>();
            foreach (string s in DDD.getStudentFieldList(user, "RC"))
            {
                lst2.Add(DDD.CourseToString(s,false));
            }
            listBox3.Items.Clear();
            listBox3.DataSource = lst2;

            listBox1.DataSource = null;
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
        }
       
        

        private void button2_Click(object sender, EventArgs e)
        {
            //List<Course> studentsCourses = student.getRegisteredCourses();
            //Course newCourse = new Course();
            //foreach (Course c in courses)
            //{
            //    if (c.getCourseName().TrimEnd() == listBox1.SelectedItem.ToString().Substring(0, 11).TrimEnd())
            //    {
            //        newCourse = c;
            //    }
            //}
            //student.dropCourse(newCourse);
            //listBox2.DataSource = seatsDataDrop(courses);

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                string course = listBox3.SelectedItem.ToString();
                course = course.Substring(0, course.IndexOf(" "));
                DDD.IncrementDecrementinStudent(user, "RegCred", -1 * DDD.getCourseFieldDecimal(course, "Credits"));
                DDD.removeIteminStudent(user, "RC", course);
                DDD.IncrementDecrementinCourse(course, "SeatsAvail", 1);
                DDD.removeIteminCourse(course, "StudentsEnrolled", user);
                listBox3.DataSource = null;
                List<string> lst2 = new List<string>();
                foreach (string s in DDD.getStudentFieldList(user, "RC"))
                {
                    lst2.Add(DDD.CourseToString(s,false));
                }
                listBox3.Items.Clear();
                listBox3.DataSource = lst2;

                listBox1.DataSource = null;
                List<string> lst = new List<string>();
                foreach (DataRow r in DDD.CourseDB.Rows)
                {
                    lst.Add(DDD.CourseToString(r["CourseCode"].ToString(),false));
                }
                listBox1.Items.Clear();
                listBox1.DataSource = lst;
            }
        }

        //public List<Course> getRegisteredCourses()
        //{
        //    return student.getRegisteredCourses();
        //}

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
