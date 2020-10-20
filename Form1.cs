using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ClassRegistration
{


    public partial class Form1 : Form
    {
        private List<Student> students;
        private List<Admin> admin;
        private List<Faculty> faculty;
        private List<Course> courses;
        private List<string> courseBox;

        public Form1(List<Student> students, List<Admin> admin, List<Faculty> faculty, List<Course> courses)
        {
            this.students = students;
            this.admin = admin;
            this.faculty = faculty;
            this.courses = courses;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }





private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(students, admin, faculty, courses, null);
            bool flag = false;
            

            foreach (Faculty element in faculty)
            {
                if (element.getUser() == textBox1.Text.ToLower() && element.getPass() == textBox2.Text)
                {
                    Form5 form5 = new Form5(students, admin, faculty, courses, element);
                    string s = "Welcome " + element.getFullName();
                    form5.ChangeLabelName(s);
                    form5.ShowDialog();
                    flag = true;
                    form5.Refresh();
                    break;
                }
            }
            foreach (Admin element in admin)
            {
                if (element.getUser() == textBox1.Text.ToLower() && element.getPass() == textBox2.Text)
                {
                    form2 = new Form2(students, admin, faculty, courses, null);
                    string s = "Welcome " + element.getFullName();
                    form2.ChangeLabelName(s);
                    form2.ShowDialog();
                    flag = true;
                    break;
                }
            }
            foreach (Student element in students)
            {
                if (element.getUser() == textBox1.Text.ToLower() && element.getPass() == textBox2.Text)
                {
                    form2 = new Form2(students, admin, faculty, courses, element);
                    List<Course> studentsCourses = element.getRegisteredCourses();
                    courseBox = new List<string>();
                    if (studentsCourses == null)
                    {
                        courseBox.Add("You are not registered for any courses yet!");
                    }
                    else
                    {
                        courseBox.Clear();
                        foreach (Course c in studentsCourses)
                        {
                            courseBox.Add(c.ToString());
                        }
                    }

                  
                    string s = "Welcome " + element.getFullName();
                    form2.ChangeLabelName(s);
                    form2.ShowDialog();
                    flag = true;
                    form2.Refresh();
                    break;
                }
            }
           
            if (flag == false)
            {
                MessageBox.Show("The Username or Password is Incorrect", "Error");
                textBox2.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
