using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form4 : Form
    {
        private List<Course> courses;
        private Student student;

        public Form4(Student student, List<Course> courses)
        {
            this.courses = courses;
            this.student = student;
            InitializeComponent();
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = courses;
        }

        public void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.DataSource = student.getRegisteredCourses();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

    }
}
