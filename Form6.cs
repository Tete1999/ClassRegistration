﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassRegistration
{
   
    public partial class Form6 : Form
    {
        private List<Course> courses;
        private List<Student> students;
        private List<Student> advisees = new List<Student>();
        private Faculty faculty;
        public Form6(List<Course> courses, List<Student> students, Faculty fac)
        {
            this.courses = courses;
            this.students = students;
            this.faculty = fac;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
            foreach( Student stud in students)
            {
                if (stud.getAdvisor().ToLower() == faculty.getUser().ToLower()) {advisees.Add(stud); }
                
            }
            listBox2.DataSource = null;
            listBox2.DataSource = advisees;
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student adv = new Student();
            string s = listBox2.SelectedItem.ToString();
            Form8 form8 = new Form8();
            form8.ChangeLabelName(s);
            foreach(Student stud in students)
            {
                if (stud.getFullName().TrimEnd().ToLower() == s.TrimEnd().ToLower())
                {
                    adv = stud;
                    ArrayList ch = new ArrayList();
                    form8.listBox1.DataSource = adv.getRegisteredCourses();
                    foreach(string crse in stud.getCourseHistory().Split('\n'))
                    {
                        
                        if (crse.Contains("S08"))
                        {
                            ch.Add(crse);
                        }
                    }
                    form8.listBox2.DataSource = ch;
                }
            }
            
            foreach (Course c in adv.getRegisteredCourses())
            {
                foreach (Course c2 in adv.getRegisteredCourses())
                {
                    if (c.Overlap(c2))
                    {
                        form8.ChangeLabelName2("Time Conflict Detected!");
                        break;
                    }
                }
            }
            form8.ShowDialog();
            //form8.listBox1.DataSource = adv.getRegisteredCourses();

        }
    }
}
