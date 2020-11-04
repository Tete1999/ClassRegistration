using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form11 : Form
    {
        private DataBase DDD;
        private string user;
        public Form11(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();

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

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                string course = listBox1.SelectedItem.ToString();
                course = course.Substring(0, course.IndexOf(" "));
                DDD.IncrementDecrementinStudent(user, "RegCred", DDD.getCourseFieldDecimal(course, "Credits"));
                DDD.pushIteminStudent(user, "RC", course);
                DDD.IncrementDecrementinCourse(course, "SeatsAvail", -1);
                DDD.pushIteminCourse(course, "StudentsEnrolled", user);

                listBox3.DataSource = null;
                List<string> lst2 = new List<string>();
                foreach (string s in DDD.getStudentFieldList(user, "RC"))
                {
                    lst2.Add(DDD.CourseToString(s, false));
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
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
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
                    lst2.Add(DDD.CourseToString(s, false));
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12(ref DDD, user);
            form12.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form20 form20 = new Form20(ref DDD, user);
            form20.ShowDialog();

        }
    }
}
