using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form20 : Form
    {
        private DataBase DDD;
        private string user;
        public Form20(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            InitializeComponent();
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.FacultyDB.Rows)
            {
                lst.Add(r["User"].ToString());
            }
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            listBox1.DataSource = lst;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                string fac = listBox1.SelectedItem.ToString();
                string tempfac = DDD.getStudentFieldString(user, "AdvisorUser");
                DDD.removeIteminFaculty(tempfac, "AdviseeUsers", user);

                DDD.setStudentField<string>(user, "AdvisorUser", fac);
                DDD.pushIteminFaculty(fac, "AdviseeUsers", user);

                MessageBox.Show("Advisor Changed to: " + fac);
            }
        }
        public void ChangeLabelName(string s)
        {
            label1.Text = s;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
