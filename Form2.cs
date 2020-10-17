using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace ClassRegistration
{


    public partial class Form2 : Form
    {
        private List<Student> students;
        private List<Admin> admin;
        private List<Faculty> faculty;
        private List<Course> courses;
        private Student student;

        public Form2(List<Student> students, List<Admin> admin, List<Faculty> faculty, List<Course> courses, Student student)
        {
            this.students = students;
            this.admin = admin;
            this.faculty = faculty;
            this.courses = courses;
            this.student = student;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
        }


        public Student getStudent()
        {
            return student;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void ChangeLabelName(string s)
        {
            label2.Text = s;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(courses, student);
            form3.listBox3.DataSource = null;
            form3.listBox3.DataSource = student.getRegisteredCourses();
            form3.ShowDialog();
  
        }

        private void Form3_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Refresh();
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string[] coursesTaken = student.getCourseHistory().Split('\n');
            List<string> ct = new List<string>();
            foreach (string element in coursesTaken)
            {
                string opt = "";
                string[] tmp = Regex.Replace(element, @"\s+", " ").Split();
                foreach (string s in tmp)
                {
                    string t = s.Trim() + "\t";
                    opt += t;
                }
                ct.Add(opt);
                opt = "";
            }
            Form4 form4 = new Form4(ct, student.getGPA());
            form4.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }

}
