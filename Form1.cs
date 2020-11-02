using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ClassRegistration
{


    public partial class Form1 : Form
    {
        private DataBase DDD;

        public Form1(ref DataBase master)
        {
            this.DDD = master;
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
            //Form2 form2 = new Form2(students, admin, faculty, courses, null);
            bool flag = false;
            

            foreach (DataRow row in DDD.FacultyDB.Rows)
            {
                if (row.Field<string>(0) == textBox1.Text.ToLower() && row.Field<string>(1) == textBox2.Text)
                {
                    Faculty2 fac = DDD.getFacultyObject(textBox1.Text.ToLower());
                    Form5 form5 = new Form5(ref DDD, fac.user);
                    //List<Course> studentsCourses = element.getRegisteredCourses();
                    //courseBox = new List<string>();
                    //if (studentsCourses == null)
                    //{
                    //    courseBox.Add("You are not registered for any courses yet!");
                    //}
                    //else
                    //{
                    //    courseBox.Clear();
                    //    foreach (Course c in studentsCourses)
                    //    {
                    //        courseBox.Add(c.ToString());
                    //    }
                    //}


                    string s = "Welcome " + fac.firstName + " " + fac.middleName + " " + fac.lastName;
                    form5.ChangeLabelName(s);
                    form5.ShowDialog();
                    flag = true;
                    form5.Refresh();
                    break;
                    //Faculty2 faculty = DDD.getFacultyObject(textBox1.Text.ToLower());
                    //Form5 form5 = new Form5(ref DDD, faculty.user);
                    //string s = "Welcome " + faculty.firstName + " " +  faculty.middleName + " " +  faculty.lastName;
                    //form5.ChangeLabelName(s);
                    //form5.ShowDialog();
                    //flag = true;
                    //form5.Refresh();
                    //break;
                }
            }
            foreach (DataRow row in DDD.AdminDB.Rows)
            {
                if (row.Field<string>(0) == textBox1.Text.ToLower() && row.Field<string>(1) == textBox2.Text)
                {
                    //Admin2 admin = DDD.getAdminObject(textBox1.Text.ToLower());
                    //Form2 form2 = new Form2(DDD, admin);
                    //string s = "Welcome " + admin.firstName + " " +  admin.middleName + " " +  admin.lastName;
                    //form2.ChangeLabelName(s);
                    //form2.ShowDialog();
                    //flag = true;
                    //break;
                }
            }
            foreach (DataRow row in DDD.StudentDB.Rows)
            {
                if (row.Field<string>(0) == textBox1.Text.ToLower() && row.Field<string>(1) == textBox2.Text)
                {
                    Student2 student = DDD.getStudentObject(textBox1.Text.ToLower());
                    Form2 form2 = new Form2(ref DDD, student.user);
                    //List<Course> studentsCourses = element.getRegisteredCourses();
                    //courseBox = new List<string>();
                    //if (studentsCourses == null)
                    //{
                    //    courseBox.Add("You are not registered for any courses yet!");
                    //}
                    //else
                    //{
                    //    courseBox.Clear();
                    //    foreach (Course c in studentsCourses)
                    //    {
                    //        courseBox.Add(c.ToString());
                    //    }
                    //}

                  
                    string s = "Welcome " + student.firstName + " " + student.middleName + " " + student.lastName;
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
            else
            {
                textBox1.Text = "";
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
