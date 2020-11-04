using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();

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
                Form11 form11 = new Form11(ref DDD, student);
                form11.ShowDialog();
            }
        }
    }
}
