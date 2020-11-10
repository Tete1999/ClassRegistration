using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ClassRegistration
{
    public partial class Form10 : Form
    {
        private DataBase DDD;
        private string user;
        public Form10(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();

            if (!DDD.getAdminFieldBool(user, "Manager"))
            {
                button2.Visible = false;
                button3.Visible = false;
            }

            List<string> lst = new List<string>();
            foreach(DataRow r in DDD.StudentDB.Select())
            {
                lst.Add(r["User"].ToString());
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this student?",
                    "Confirm Student Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string student = listBox1.SelectedItem.ToString();

                    List<string> RC = new List<string>(DDD.getStudentFieldList(student, "RC"));
                    foreach (string crs in RC)
                    {
                        DDD.removeIteminCourse(crs, "StudentsEnrolled", student);
                    }

                    string facadvisor = DDD.getStudentFieldString(student, "AdvisorUser");
                    if (facadvisor != "Staff")
                        DDD.removeIteminFaculty(facadvisor, "AdviseeUsers", student);

                    DataRow DR = DDD.StudentDB.Select("User = '" + student + "'")[0];
                    DDD.StudentDB.Rows.Remove(DR);

                    List<string> lst = new List<string>();
                    foreach (DataRow r in DDD.StudentDB.Select())
                    {
                        lst.Add(r["User"].ToString());
                    }
                    listBox1.DataSource = null;
                    listBox1.DataSource = lst;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string student = listBox1.SelectedItem.ToString();
                Student2 stu = DDD.getStudentObject(student);
                Form11 form11 = new Form11(ref DDD, student);
                string s = stu.firstName + " " + stu.middleName + " " + stu.lastName;
                form11.ChangeLabelName(s);
                form11.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string user = Interaction.InputBox("Insert Student Username", "Add Student", "JDoe", 100, 100).ToLower();

            if (DDD.StudentDB.Select("User = '" + user + "'").Length != 0)
            {
                MessageBox.Show(user + " has already been taken");
                return;
            }
            string pass = Interaction.InputBox("Insert Student Password", "Add Student", "1234", 100, 100).ToLower();
            string first = Interaction.InputBox("Insert First Name", "Add Student", "first", 100, 100).ToLower();
            string middle = Interaction.InputBox("Insert Middle Name", "Add Student", "middle", 100, 100).ToLower();
            string last = Interaction.InputBox("Insert Last Name", "Add Student", "last", 100, 100).ToLower();

            DialogResult dialogResult = MessageBox.Show("username: " + user + "\n" + "password: " + pass + "\n" +
                "full name : " + first + " " + middle + " " + last, "Confirm Student Credentials",
                MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                DDD.StudentDB.Rows.Add(user, pass, first, middle, last, "Staff", 0.0, new List<string>(), new List<string>());
                MessageBox.Show(user + " added to database");
            }
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.StudentDB.Select())
            {
                lst.Add(r["User"].ToString());
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;
        }
    }
}
