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
        private Form2 f;
   
        public Form3(List<Course> courses, Student student, Form2 frm)
        {
            this.courses = courses;
            this.student = student;
            this.f = frm;
            InitializeComponent();
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = courses; 
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            List<Course> studentsCourses = student.getRegisteredCourses();
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
                        MessageBox.Show("Warning, time overlap");
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

                //if (newCourse.getSeats() > 0 && flag == false && totalCred < 5)
                //{

                //    student.addCourse(newCourse);
                //}

                if (student.getCourseHistory().ToString().Contains(newCourse.getCourseName().ToString().TrimEnd()))
                {
                    MessageBox.Show("Warning, Course taken before");

                }
                if (flag == false && newCourse.getSeats() >= 1 && ((student.getTotalCredit() + newCourse.getCredit()) < 5))
                {
                    student.addCourse(newCourse);
                    studentsCourses = student.getRegisteredCourses();
                }
                else
                {
                    MessageBox.Show("Course could not be added");
                }
            }
            else
            {
                MessageBox.Show("Course could not be added");
            }
            f.listBox1.DataSource = studentsCourses;
            this.Refresh();
            //f.Hide();
            //f.ResetText();
            //f.Refresh();
 

            
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            List<Course> studentsCourses = student.getRegisteredCourses();
            f.listBox1.DataSource = studentsCourses;
            Course newCourse = new Course();
            foreach (Course c in courses)
            {
                if (c.getCourseName().TrimEnd() == listBox1.SelectedItem.ToString().Substring(0, 11).TrimEnd())
                {
                    newCourse = c;
                }
            }
            student.dropCourse(newCourse);
            f.listBox1.DataSource = studentsCourses;
            this.Refresh();
            
        }

        
    }
}
