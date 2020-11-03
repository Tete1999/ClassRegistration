using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace ClassRegistration
{


    public partial class Form2 : Form
    {
        private DataBase DDD;
        private string user;

        public Form2(ref DataBase master, string username)
        {
            this.DDD = master;
            this.user = username;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
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
            Form3 form3 = new Form3(ref DDD, user);
            //form3.listBox3.DataSource = null;
            //form3.listBox3.DataSource = DDD.getStudentFieldList(user, "RC");
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
            //Console.WriteLine(student.getCourseHistory());
            //string[] coursesTaken = student.getCourseHistory().Split('\n');
            //List<string> ct = new List<string>();
            //foreach (string element in coursesTaken)
            //{
            //    Console.WriteLine(element);
            //    string opt = "";
            //    string[] tmp = Regex.Replace(element, @"\s+", " ").Split();
            //    foreach (string s in tmp)
            //    {
            //        string t = s.Trim() + "\t";
            //        opt += t;
            //    }
            //    ct.Add(opt);
            //}
            Form4 form4 = new Form4(ref DDD, user);
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
