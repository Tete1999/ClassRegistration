﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form4 : Form
    {
        private List<string> coursesTaken;
        private string gpaInfo;
        public Form4(List<string>coursesTaken, string gpaInfo )
        {
            this.coursesTaken = coursesTaken;
            this.gpaInfo = gpaInfo;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
            listBox1.DataSource = coursesTaken;
            richTextBox1.Text = gpaInfo;
        }

     

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
