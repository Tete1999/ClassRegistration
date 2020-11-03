using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form19 : Form
    {
        private DataBase DDD;
        private string course;
        public Form19(ref DataBase master, string coursecode)
        {
            this.DDD = master;
            this.course = coursecode;
            InitializeComponent();
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.FacultyDB.Rows)
            {
                lst.Add(r["User"].ToString());
            }
            lst.Add("Staff");
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            listBox1.DataSource = lst;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fac = listBox1.SelectedItem.ToString();
            string tempfac = DDD.getCourseFieldString(course, "Instructor");
            DDD.setCourseField<string>(course, "Instructor", fac);
            if (tempfac != "Staff")
                DDD.removeIteminFaculty(tempfac, "Courses", course);
            if (fac != "Staff")
                DDD.pushIteminFaculty(fac, "Courses", course);
            MessageBox.Show("Instructor of " + course + " updated to " + fac);
        }
    }
}
