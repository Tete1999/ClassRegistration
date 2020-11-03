using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            ChangeLabelName(user)
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
    }
}
