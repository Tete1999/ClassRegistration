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
        public Form3(List<Course> courses, Student student)
        {
            this.courses = courses;
            this.student = student;
            Console.WriteLine("XXX");
            Console.WriteLine(student.ToString());
            InitializeComponent();
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = courses; 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Course> studentsCourses = student.getRegisteredCourses();
            decimal totalCred = 0;
            Course newCourse = new Course();
            bool flag = false;
            foreach (Course c in courses)
            {
                if (c.getCourseName().TrimEnd() == listBox1.SelectedItem.ToString().Substring(0,11).TrimEnd())
                {
                    newCourse = c;
                }
            }

            foreach (Course c in studentsCourses)
            {
                totalCred += c.getCredit();
                if (c.getCourseName().TrimEnd() == newCourse.getCourseName().TrimEnd())
                {
                    flag = true;
                }
            }

            if (newCourse.getSeats() > 0)
            {
                if (flag == false)
                {
                    if (totalCred < 5)
                    {
                        student.addCourse(newCourse);
                    }
                }
            }
            else { MessageBox.Show("Course could not be added"); }

            if (student.getCourseHistory().ToString().Contains(newCourse.getCourseName().ToString().TrimEnd()))
            {
                MessageBox.Show("Warning, Course taken before");
            }

            foreach (Course c in studentsCourses)
            {
                
                if (c.Overlap(newCourse))
                {
                    MessageBox.Show("Warning, time overlap");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
