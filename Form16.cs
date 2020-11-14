using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ClassRegistration
{
    public partial class Form16 : Form
    {
        private DataBase DDD;
        private string user;
        public Form16(ref DataBase master, string user)
        {
            this.DDD = master;
            this.user = user;
            InitializeComponent();


            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.AdminDB.Select())
            {
                if (r["User"].ToString() != user)
                    lst.Add(r["User"].ToString());
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;

        }

        private void Add_Click(object sender, EventArgs e)
        {
            string username = Interaction.InputBox("Insert Admin Username", "Add Admin", "JDoe", 100, 100).ToLower();

            if (DDD.AdminDB.Select("User = '" + username + "'").Length != 0)
            {
                MessageBox.Show(username + " has already been taken");
                return;
            }
            string pass = Interaction.InputBox("Insert Admin Password", "Add Admin", "1234", 100, 100).ToLower();
            string first = Interaction.InputBox("Insert First Name", "Add Admin", "first", 100, 100).ToLower();
            string middle = Interaction.InputBox("Insert Middle Name", "Add Admin", "middle", 100, 100).ToLower();
            string last = Interaction.InputBox("Insert Last Name", "Add Admin", "last", 100, 100).ToLower();
            DialogResult manager_result = MessageBox.Show("Is this Administrator a Manager?","Add Admin", MessageBoxButtons.YesNo);
            bool manager = manager_result == DialogResult.Yes;

            DialogResult dialogResult = MessageBox.Show("Username: " + username + "\n" + "Password: " + pass + "\n" +
                "Full Name: " + first + " " + middle + " " + last + "\n" + "Manager:" + " " + manager, 
                "Confirm Admin Credentials", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                DDD.AdminDB.Rows.Add(username, pass, first, middle, last, manager);
                MessageBox.Show(username + " added to database");
            }
            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.AdminDB.Select())
            {
                if (r["User"].ToString() != user)
                    lst.Add(r["User"].ToString());
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this admin?",
                    "Confirm Admin Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string admin = listBox1.SelectedItem.ToString();
                    DataRow DR = DDD.AdminDB.Select("User = '" + admin + "'")[0];
                    DDD.AdminDB.Rows.Remove(DR);

                    List<string> lst = new List<string>();
                    foreach (DataRow r in DDD.AdminDB.Select())
                    {
                        if (r["User"].ToString() != user)
                            lst.Add(r["User"].ToString());
                    }
                    listBox1.DataSource = null;
                    listBox1.DataSource = lst;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
