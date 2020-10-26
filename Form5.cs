using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form5 : Form
    {
        private List<Student> students;
        private List<Admin> admin;
        private List<Faculty> faculty;
        private List<Course> courses;
        private Faculty facultyMember;
        public Form5(List<Student> students, List<Admin> admin, List<Faculty> faculty, List<Course> courses, Faculty fac)
        {
            this.students = students;
            this.admin = admin;
            this.faculty = faculty;
            this.courses = courses;
            this.facultyMember = fac;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();

            List<Course> teaching = new List<Course>();
            foreach (Course c in courses)
            {
                if (c.getInstructor().TrimEnd().ToLower() == facultyMember.getUser().ToLower())
                {
                    teaching.Add(c);
                }
            }
            Console.WriteLine(teaching.Count.ToString());
            listBox1.DataSource = null;
            listBox1.DataSource = teaching;


            
        }
        public Faculty getFaculty() { return facultyMember; }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void ChangeLabelName(string s)
        {
            label1.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(courses, students, facultyMember);
            form6.listBox1.DataSource = courses;
            form6.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string course = listBox1.SelectedItem.ToString();
            Form7 form7 = new Form7();
            List<Student> roster = new List<Student>();
            foreach (Student stud in students)
            {
                foreach (Course c in stud.getRegisteredCourses())
                {
                    if (c.getCourseName().ToString().TrimEnd().ToLower() == course.Substring(0, 11).TrimEnd().ToLower())
                    {
                        roster.Add(stud);
                    }
                }
            }
            form7.listBox1.DataSource = roster;
            form7.ShowDialog();
        }
    }
}
