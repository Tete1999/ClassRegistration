using System;
using System.Collections;
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
   
    public partial class Form6 : Form
    {
        private DataBase DDD;
        private string user;
        public Form6(ref DataBase master, string username)
        {

            this.DDD = master;
            this.user = username;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            InitializeComponent();

            List<string> lst = new List<string>();
            foreach (DataRow r in DDD.CourseDB.Rows)
            {
                lst.Add(DDD.CourseToString(r["CourseCode"].ToString()));
            }
            listBox1.DataSource = null;
            listBox1.DataSource = lst;

            List<string> advisees = new List<string>(DDD.getFacultyFieldList(user, "AdviseeUsers"));
            listBox2.DataSource = null;
            listBox2.DataSource = advisees;
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox2.SelectedItem != null)
            {
                string student = listBox2.SelectedItem.ToString();
                List<string> regcourses = new List<string>(DDD.getStudentFieldList(student, "RC"));
                Form8 form8 = new Form8();
                form8.ChangeLabelName(DDD.getStudentFieldString(student,"First") + " " +
                    DDD.getStudentFieldString(student, "Middle") + " " 
                    + DDD.getStudentFieldString(student, "Last"));
                List<string> flags = new List<string>();
                for(int i= 0; i < regcourses.Count; i++ )
                {
                    for(int j =0; j < i; j++)
                    {
                        if(DDD.Overlap(regcourses[i],regcourses[j]))
                        {
                            if (!flags.Contains(regcourses[i]))
                                flags.Add(regcourses[i]);
                            if (!flags.Contains(regcourses[j]))
                                flags.Add(regcourses[j]);
                        }
                    }
                }
                if (flags.Count != 0)
                {
                    string flaggedcourses = "Time Conflict Detected in Following Courses: ";
                    foreach (string c in flags)
                        flaggedcourses += c + ", ";
                    flaggedcourses = flaggedcourses.Substring(0, flaggedcourses.Length - 2);
                    form8.ChangeLabelName2(flaggedcourses);
                }
                List<string> lst = new List<string>();
                foreach (string s in regcourses)
                {
                    lst.Add(DDD.CourseToString(s, false));
                }
                form8.listBox1.DataSource = null;
                form8.listBox1.DataSource = lst;

                List<string> lst2 = DDD.CurrentCourseToList(student);
                form8.listBox2.Items.Clear();
                form8.listBox2.DataSource = lst2;
                form8.ShowDialog();
            }
        //    Student adv = new Student();
        //    string s = listBox2.SelectedItem.ToString();
        //    Form8 form8 = new Form8();
        //    form8.ChangeLabelName(s);
        //    foreach(Student stud in students)
        //    {
        //        if (stud.getFullName().TrimEnd().ToLower() == s.TrimEnd().ToLower())
        //        {
        //            adv = stud;
        //            ArrayList ch = new ArrayList();
        //            form8.listBox1.DataSource = adv.getRegisteredCourses();
        //            foreach(string crse in stud.getCourseHistory().Split('\n'))
        //            {
                        
        //                if (crse.Contains("S08"))
        //                {
        //                    ch.Add(crse);
        //                }
        //            }
        //            form8.listBox2.DataSource = ch;
        //        }
        //    }
            
        //    foreach (Course c in adv.getRegisteredCourses())
        //    {
        //        foreach (Course c2 in adv.getRegisteredCourses())
        //        {
        //            if (c.Overlap(c2))
        //            {
        //                form8.ChangeLabelName2("Time Conflict Detected!");
        //                break;
        //            }
        //        }
        //    }
        //    form8.ShowDialog();
            //form8.listBox1.DataSource = adv.getRegisteredCourses();

        }
    }
}
