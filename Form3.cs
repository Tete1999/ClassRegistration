using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form3 : Form
    {
        private List<Course> courses;
        private Student student;
        private List<Course> studentsCourses = new List<Course>();


        public Form3(List<Course> courses, Student student)
        {
            this.courses = courses;
            this.student = student;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
            listBox1.DataSource = courses;
            listBox3.DataSource = studentsCourses;
            List<string> seats = seatsData(courses);
            listBox2.DataSource = seats;

        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private List<string> seatsData(List<Course> courses)
        {
            List<string> seats = new List<string>();
            foreach (Course c in courses)
            {
                if (c.getCourseName().TrimEnd() == listBox1.SelectedItem.ToString().Substring(0, 11).TrimEnd())
                {
                    c.setSeats(c.getSeats() - 1);
                }
                seats.Add(c.getSeats().ToString() + "\n");
            }
            return seats;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            studentsCourses = student.getRegisteredCourses();
            Course newCourse = new Course();
            bool flag = false;
            //Selection of Course 
            if (student.getTotalCredit() < 5)
            {
                foreach (Course c in courses)
                {
                    if (c.getCourseName().TrimEnd() == listBox1.SelectedItem.ToString().Substring(0, 11).TrimEnd())
                    {
                        newCourse = c;
                    }
                }
                foreach (Course c in studentsCourses)
                {
                    if (c.Overlap(newCourse))
                    {
                        MessageBox.Show("Warning: Time Overlap");
                    }
                }

                // Have I already registered
                foreach (Course c in studentsCourses)
                {
                    if (c.getTitle().TrimEnd() == newCourse.getTitle().TrimEnd())
                    {
                        flag = true;
                    }
                }

                if (student.getCourseHistory().ToString().Contains(newCourse.getCourseName().ToString().TrimEnd()))
                {
                    MessageBox.Show("Warning: Course Taken Before");

                }
                if (flag == false && newCourse.getSeats() >= 1)
                {
                    if ((student.getTotalCredit() + newCourse.getCredit()) < 5)
                    {
                        student.addCourse(newCourse);
                        studentsCourses = student.getRegisteredCourses();
                        MessageBox.Show("Course Added");
                        //newCourse.setSeats(newCourse.getSeats() - 1);
                    }
                    else
                    {
                        MessageBox.Show("Error: Credit Limit Exceeded");
                    }
                }
                else
                {
                    MessageBox.Show("Error: Course is Full or Course Already Registered");
                }
            }
            List<string> seats = seatsData(courses);
            listBox2.DataSource = seats;
            listBox1.DataSource = null;
            listBox1.DataSource = courses;
            listBox3.DataSource = null;
            listBox3.DataSource = studentsCourses;
           
        }
       
        

        private void button2_Click(object sender, EventArgs e)
        {
            List<Course> studentsCourses = student.getRegisteredCourses();
            Course newCourse = new Course();
            foreach (Course c in courses)
            {
                if (c.getCourseName().TrimEnd() == listBox1.SelectedItem.ToString().Substring(0, 11).TrimEnd())
                {
                    newCourse = c;
                }
            }
            student.dropCourse(newCourse);
            newCourse.setSeats(newCourse.getSeats() - 1);
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
            List<Course> rg = student.getRegisteredCourses();
            Course toRemove = new Course(null, null, null, 1, 0, null);
            foreach (Course c in rg)
            {
                if (c.ToString().Substring(0, 11).TrimEnd() == listBox3.SelectedItem.ToString().Substring(0, 11).TrimEnd())
                {
                    toRemove = c;
                };
            }
            rg.Remove(toRemove);
            listBox3.DataSource = null;
            listBox3.DataSource = rg;
            MessageBox.Show("Course Dropped Successfully");
        }

        public List<Course> getRegisteredCourses()
        {
            return student.getRegisteredCourses();
        }

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
