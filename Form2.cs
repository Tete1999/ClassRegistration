using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
            form3.listBox1.DataSource = courses;
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
            Form4 form4 = new Form4();
            string text = student.getCourseHistory().Replace("\n", "\r\n");
            // form4.listBox1.DataSource = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            form4.textBox1.Text = text;
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
