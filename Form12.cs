using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form12 : Form
    {
        private DataBase DDD;
        private string user;
        public Form12(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
            List<string> lst = DDD.CourseHistoryToList(user);
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
            decimal[] tmp = DDD.GetStudentGPA(user);
            richTextBox1.Text = "GPA:                  " + tmp[0]
                + "\nQuality Points:       " + tmp[1] + "\nTotal Points:         " + tmp[2];

            List<string> lst2 = DDD.CurrentCourseToList(user);
            listBox2.Items.Clear();
            listBox2.DataSource = lst2;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public void ChangeLabelName(string s)
        {
            label4.Text = s;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
