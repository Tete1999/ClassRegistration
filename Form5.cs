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
        private List<Student> students;
        private List<Admin> admin;
        private List<Faculty> faculty;
        private List<Course> courses;
        private Faculty facultyMember;
        public Form5(List<Student> students, List<Admin> admin, List<Faculty> faculty, List<Course> courses, Faculty fac)
        {
            this.students = students;
            this.admin = admin;
            this.faculty = faculty;
            this.courses = courses;
            this.facultyMember = fac;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
        }





    }
}
