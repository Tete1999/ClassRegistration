using System;
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
    public partial class Form5 : Form
    {
        private Form3 f3;
        public Form5(Form3 f3)
        {
            this.f3 = f3;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Course> rg = f3.getRegisteredCourses();
            Course toRemove = new Course(null, null, null, 1, 0,  null);
            foreach(Course c in rg)
            {
                if (c.ToString().Substring(0,11).TrimEnd() == listBox1.SelectedItem.ToString().Substring(0, 11).TrimEnd())
                {
                    toRemove = c;
                }
            }
            rg.Remove(toRemove);
            listBox1.DataSource = rg;
            MessageBox.Show("Course Dropped Successfully");
            this.Close();
        }
    }
}
