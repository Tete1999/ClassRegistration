using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassRegistration
{
    public partial class Form3 : Form
    {
        private List<Course> courses;
        private List<string> allCourse = new List<string>();
        public Form3(List<Course> courses)
        {
            this.courses = courses;
            
            
            InitializeComponent();
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = allCourse;
            foreach (string element in allCourse)
            {
                Console.WriteLine(element);
                allCourse.Add(element.ToString());
            }

        }
    }
}
