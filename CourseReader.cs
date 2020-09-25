using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Remoting.Messaging;

namespace ClassRegistration
{
    public class CourseReader
    {
        private List<Course> courses;
        private string courseName;
        private string title;
        private string instructor;
        private decimal credit;
        private int seats;
        private int numBlocks;
        private ArrayList timeBlocks = new ArrayList();
        
        public void readFile(string fname)
        {
            using (StreamReader file = new StreamReader(fname))
            {

                string ln;
                courses = new List<Course>();
                while ((ln = file.ReadLine()) != null)
                {
                    courseName = ln.Substring(0, 11);
                    title = ln.Substring(11, 15);
                    instructor = ln.Substring(27, 10);
                    credit = decimal.Parse(ln.Substring(38, 4));
                    seats = int.Parse(ln.Substring(43, 3));
                    numBlocks = int.Parse(ln.Substring(47, 1));
                    append(ln.Substring(49).TrimEnd());
        
                    Course course = new Course(courseName, title, instructor, credit, seats, timeBlocks);
                    courses.Add(course);
                }
                file.Close();
            }
        }
        private void append(string tb)
        {
            string[] tmp = tb.Split(' ');
            timeBlocks.Clear();
            foreach (string element in tmp) { timeBlocks.Add(int.Parse(element));}
        }
        
        public List<Course> getCourses() { return courses;}

    }



}
