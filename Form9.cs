﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ClassRegistration
{
    public partial class Form9 : Form
    {
        private DataBase DDD;
        private string user;
        public Form9(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
            ChangeLabelName(user);
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void ChangeLabelName(string s)
        {
            label1.Text = s;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this course?",
                "Confirm Class Deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string course = listBox1.SelectedItem.ToString();
                course = course.Substring(0, course.IndexOf(" "));
                List<string> stuenrolled = new List<string>(DDD.getCourseFieldList(course, "StudentsEnrolled"));
                foreach (string stu in stuenrolled)
                {
                    DDD.removeIteminStudent(stu, "RC", course);
                    decimal creds = DDD.getCourseFieldDecimal(course, "Credits");
                    DDD.IncrementDecrementinStudent(stu, "RegCred", -1 * creds);
                }
                string fac = DDD.getCourseFieldString(course, "Instructor");
                DDD.removeIteminFaculty(fac, "Courses", course);
                DataRow DR = DDD.CourseDB.Select("CourseCode = '" + course + "'")[0];
                DDD.CourseDB.Rows.Remove(DR);

                List<string> lst = new List<string>();
                foreach (DataRow r in DDD.CourseDB.Rows)
                {
                    lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
                }
                listBox1.DataSource = null;
                listBox1.DataSource = lst;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string course = listBox1.SelectedItem.ToString();
            course = course.Substring(0, course.IndexOf(" "));
            string output = Interaction.InputBox("Input Time Blocks Seperated by Commas", "Change Course Time", "", 100, 100);
            try
            {
                ArrayList arrayList = new ArrayList();
                foreach(string str in output.Split(','))
                {
                    arrayList.Add(Int32.Parse(str));
                }
                DialogResult dialogResult = MessageBox.Show("New Schedule: " +DDD.getSchedule(arrayList),
                    "Confirm New Change",MessageBoxButtons.OKCancel);
                if(dialogResult == DialogResult.OK)
                {
                    DDD.setCourseField<ArrayList>(course, "TimeBlocks", arrayList);
                    List<string> lst = new List<string>();
                    foreach (DataRow r in DDD.CourseDB.Rows)
                    {
                        lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
                    }
                    listBox1.DataSource = null;
                    listBox1.Items.Clear();
                    listBox1.DataSource = lst;
                }
            }
            catch
            {
                MessageBox.Show("Error: Course Schedule Not Updated");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string course = listBox1.SelectedItem.ToString();
            course = course.Substring(0, course.IndexOf(" "));
            Form19 form19 = new Form19(ref DDD, course);
            form19.ShowDialog();

            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
        }
    }
}
