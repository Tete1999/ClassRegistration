﻿using System;
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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
