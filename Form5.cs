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
        private DataBase DDD;
        private string user;
        //private List<Student> students;
        //private List<Admin> admin;
        //private List<Faculty> faculty;
        //private List<Course> courses;
        //private Faculty facultyMember;

        public Form5(ref DataBase master,  string user)
        {
            this.DDD = master;
            this.user = user;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();

            List<string> teaching = new List<string>();
            foreach (string c in DDD.getFacultyFieldList(user,"Courses"))
            {
                teaching.Add(DDD.CourseToString(c));
            }
            listBox1.DataSource = null;
            listBox1.DataSource = teaching;



        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void ChangeLabelName(string s)
        {
            label1.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(ref DDD, user);
            //Form6 form6 = new Form6(courses, students, facultyMember);
            //form6.listBox1.DataSource = courses;
            form6.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string course = listBox1.SelectedItem.ToString();
                course = course.Substring(0, course.IndexOf(" "));
                Form7 form7 = new Form7();
                List<string> roster = new List<string>(DDD.getCourseFieldList(course, "StudentsEnrolled"));
                form7.listBox1.DataSource = null;
                form7.listBox1.DataSource = roster;
                form7.ShowDialog();
            }
        }
    }
}
