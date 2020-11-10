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
    public partial class Form14 : Form
    {
        private DataBase DDD;
        private string user;
        public Form14(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;

            InitializeComponent();

            if (!DDD.getAdminFieldBool(user, "Manager"))
            {
                button2.Visible = false;
                button3.Visible = false;
            }

            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.FacultyDB.Select())
            {
                lst.Add(r["User"].ToString());
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this faculty?",
                    "Confirm Student Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string fac = listBox1.SelectedItem.ToString();

                    List<string> RC = new List<string>(DDD.getFacultyFieldList(fac, "Courses"));
                    foreach (string crs in RC)
                    {
                        DDD.setCourseField<string>(crs, "Instructor", "Staff");
                    }
                    List<string> advisees = new List<string>(DDD.getFacultyFieldList(fac, "AdviseeUsers"));
                    foreach (string stu in advisees)
                        DDD.setStudentField<string>(stu, "AdvisorUser", "Staff");

                    DataRow DelFac = DDD.FacultyDB.Select("User = '" + fac + "'")[0];
                    DDD.FacultyDB.Rows.Remove(DelFac);

                    List<string> lst = new List<string>();
                    foreach (DataRow r in DDD.FacultyDB.Select())
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
                string fac = listBox1.SelectedItem.ToString();
                Form15 form15 = new Form15(ref DDD, fac);
                Faculty2 fac2 = DDD.getFacultyObject(fac);
                string s = fac2.firstName + " " + fac2.middleName + " " + fac2.lastName;
                form15.ChangeLabelName(s);
                form15.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string user = Interaction.InputBox("Insert Faculty Username", "Add Faculty", "JDoe", 100, 100).ToLower();

            if (DDD.FacultyDB.Select("User = '" + user + "'").Length != 0)
            {
                MessageBox.Show(user + " has already been taken");
                return;
            }
            string pass = Interaction.InputBox("Insert Faculty Password", "Add Faculty", "1234", 100, 100).ToLower();
            string first = Interaction.InputBox("Insert First Name", "Add Faculty", "first", 100, 100).ToLower();
            string middle = Interaction.InputBox("Insert Middle Name", "Add Faculty", "middle", 100, 100).ToLower();
            string last = Interaction.InputBox("Insert Last Name", "Add Faculty", "last", 100, 100).ToLower();

            DialogResult dialogResult = MessageBox.Show("username: " + user + "\n" + "password: " + pass + "\n" +
                "full name : " + first + " " + middle + " " + last, "Confirm Faculty Credentials",
                MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                DDD.FacultyDB.Rows.Add(user, pass, first, middle, last, new List<string>(), new List<string>());
                MessageBox.Show(user + " added to database");
            }
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.FacultyDB.Select())
            {
                lst.Add(r["User"].ToString());
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;
        }
    }
}
