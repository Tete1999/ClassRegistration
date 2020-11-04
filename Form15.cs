using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form15 : Form
    {
        private DataBase DDD;
        private string user;
        public Form15(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            InitializeComponent();

            List<string> lst = new List<string>();
            foreach (string r in DDD.getFacultyFieldList(user,"Courses"))
            {
                lst.Add(DDD.CourseToString(r));
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
