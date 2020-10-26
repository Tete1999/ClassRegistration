using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassRegistration
{

    public partial class Form8 : Form
    {
        //private bool msg;
        public Form8()
        {
            //this.msg = flag;
            InitializeComponent();
        }

        public void ChangeLabelName(string s)
        {
            label2.Text = s;
        }

        public void ChangeLabelName2(string s)
        {
            label4.Text = s;
            label4.Visible = true;
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        public void error()
        {
            MessageBox.Show("Time conflict detected");
        }
    }
}
