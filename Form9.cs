using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ClassRegistration
{
    public partial class Form9 : Form
    {
        private DataBase DDD;
        private string user;
        public Form9(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
            ChangeLabelName(user);
            if (!DDD.getAdminFieldBool(user, "Manager"))
            {
                button6.Visible = false;
                button7.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }

            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void ChangeLabelName(string s)
        {
            label1.Text = s;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this course?",
                "Confirm Class Deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string course = listBox1.SelectedItem.ToString();
                course = course.Substring(0, course.IndexOf(" "));
                List<string> stuenrolled = new List<string>(DDD.getCourseFieldList(course, "StudentsEnrolled"));
                foreach (string stu in stuenrolled)
                {
                    DDD.removeIteminStudent(stu, "RC", course);
                    decimal creds = DDD.getCourseFieldDecimal(course, "Credits");
                    DDD.IncrementDecrementinStudent(stu, "RegCred", -1 * creds);
                }
                string fac = DDD.getCourseFieldString(course, "Instructor");
                if (fac != "Staff")
                    DDD.removeIteminFaculty(fac, "Courses", course);
                DataRow DR = DDD.CourseDB.Select("CourseCode = '" + course + "'")[0];
                DDD.CourseDB.Rows.Remove(DR);

                List<string> lst = new List<string>();
                foreach (DataRow r in DDD.CourseDB.Rows)
                {
                    lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
                }
                listBox1.DataSource = null;
                listBox1.DataSource = lst;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList arrayList = new ArrayList();
            bool finished = false;
            while (!finished)
            {
                Form17 form17 = new Form17(ref DDD, user, ref arrayList, ref finished);
                form17.ShowDialog();
                finished = form17.finished;
            }

            string course = listBox1.SelectedItem.ToString();
            course = course.Substring(0, course.IndexOf(" "));
            DialogResult dialogResult = MessageBox.Show("New Schedule: " + DDD.getSchedule(arrayList),
                "Confirm New Change", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                DDD.setCourseField<ArrayList>(course, "TimeBlocks", arrayList);
                List<string> lst = new List<string>();
                foreach (DataRow r in DDD.CourseDB.Rows)
                {
                    lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
                }
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                listBox1.DataSource = lst;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string course = listBox1.SelectedItem.ToString();
            course = course.Substring(0, course.IndexOf(" "));
            Form19 form19 = new Form19(ref DDD, course);
            form19.ShowDialog();

            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(ref DDD, user);
            form10.ShowDialog();
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14(ref DDD,user);
            form14.ShowDialog();
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16(ref DDD, user);
            form16.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string code = Interaction.InputBox("Insert Course Code", "Add Course", "XXX-XXX-XX", 100, 100).ToUpper();
            if (code.Length < 9)
            {
                MessageBox.Show("Error: Not a Valid Course Code");
                return;
            }

            if (!code.Substring(code.Length-3).StartsWith("-"))
            {
                MessageBox.Show("Error: Not a Valid Course Code");
                return;
            }

            if (DDD.CourseDB.Select("CourseCode = '" + code + "'").Length != 0)
            {
                MessageBox.Show(code + " already exists");
                return;
            }
            string name = Interaction.InputBox("Insert Course Name", "Add Course", "Intro Lrn", 100, 100);
            decimal credits = 0M;
            int seats = 0;
            try
            {
                credits = decimal.Parse(Interaction.InputBox("Insert Credits", "Add Course", "1.00", 100, 100).ToLower());
                seats = Int32.Parse(Interaction.InputBox("Insert Seats", "Add Course", "20", 100, 100).ToLower());
            }
            catch
            {
                MessageBox.Show("Invlid Information");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("course: " + code + "\n" + "name: " + name + "\n" +
                "Instructor : Staff" + "\n" + "Credits: " + credits + "\n" + "Seats: " + seats + "\n" + 
                "Time: ARRANGED", "Confirm Course", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                ArrayList arrayList = new ArrayList();
                arrayList.Add(0);
                DDD.CourseDB.Rows.Add(code, name, "Staff", credits, seats, 0 ,arrayList, new List<string>());
                MessageBox.Show(code + " added to database");
            }

            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString(), false));
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;

        }
    }
}
