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
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
