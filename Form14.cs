using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
    }
}
