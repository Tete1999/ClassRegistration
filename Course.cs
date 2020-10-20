using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassRegistration
{
    public class Course
    {
        private string courseName;
        private string title;
        private string instructor;
        private decimal credit;
        private int seats;
        private ArrayList timeBlocks;




        public Course()
        {}
        public Course(string courseName, string title, string instructor, decimal credit, int seats, ArrayList timeBlocks)
        {
            this.courseName = courseName;
            this.title = title;
            this.instructor = instructor;
            this.credit = credit;
            this.seats = seats;
            this.timeBlocks = timeBlocks;
        }

        public string getCourseName()
        {

            return courseName + "\t";
        }
        public string getTitle()
        {
            return title + "\t";
        }
        private string getInstructor() { return instructor + "\t"; }
        public decimal getCredit() { return credit; }
        public int getSeats() { return seats; }
        public ArrayList getTimeBlocks() { return timeBlocks; }

        public void setSeats(int s) { this.seats = s; }

        private static string sum_up(int target)
        {
            string code = "";
            List<int> numbers = new List<int>() { 1, 2, 4, 8, 16 };
            sum_up_recursive(numbers, target, new List<int>(), ref code);
            return code;
        }

        private static void sum_up_recursive(List<int> numbers, int target, List<int> partial, ref string code)
        {
            int s = 0;
            foreach (int x in partial) s += x;

            if (s == target)
                code = getDays(partial);

            if (s >= target)
                return;

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new List<int>();
                int n = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

                List<int> partial_rec = new List<int>(partial);
                partial_rec.Add(n);
                sum_up_recursive(remaining, target, partial_rec, ref code);
            }
        }

        private static string getDays(List<int> lst)
        {
            string str = "";
            if (lst.Contains(1))
                str += "M";
            if (lst.Contains(2))
                str += "T";
            if (lst.Contains(4))
                str += "W";
            if (lst.Contains(8))
                str += "R";
            if (lst.Contains(16))
                str += "F";
            while (str.Length != 7)
            {
                str += " ";
            }
            return str;
        }

        public static string getTime(int t)
        {
            string AMPM = " AM";
            float time = t;
            time = time / 2;
            System.TimeSpan s = System.TimeSpan.FromSeconds(time * 3600);
            if (s.Hours >= 12)
            {
                AMPM = " PM";
                if (s.Hours > 12)
                    s = s.Subtract(System.TimeSpan.FromSeconds(12 * 3600));
            }
            string min = (s.Minutes).ToString();
            if (min.Length != 2)
                min += "0";
            string hours = (s.Hours).ToString();
            if (hours.Length != 2)
                hours = "0" + hours;
            return hours + ":" + min + AMPM;
        }

        private string getSchedule(ArrayList timeBlocks)
        {
            string output = "";
            string tab = " \t";

            foreach (int ddttl in timeBlocks)
            {
                //Console.WriteLine(ddttl);
                output += sum_up(ddttl / 1000) + tab + getTime((ddttl / 10) % 100) + "-" + getTime(((ddttl / 10) % 100) + ((ddttl % 10))) + tab;
            }
            return output;
        }

        public override string ToString()
        {
            return getCourseName() + getTitle() + getInstructor() + getCredit() + "\t" + getSchedule(timeBlocks);
        }



        private bool TimeSpan_Overlap(TimeSpan start1, TimeSpan finish1, TimeSpan start2, TimeSpan finish2)
        {
            if (start1 == start2 || finish1 == finish2)
                return true;

            if ((start1 < start2 && start2 < finish1) || (start1 < finish2 && finish2 < finish1))
                return true;

            if ((start2 < start1 && start1 < finish2) || (start2 < finish1 && finish1 < finish2))
                return true;
            else
                return false;

        }

        public bool Overlap(Course c2)
        {
            ArrayList tb1 = this.getTimeBlocks();
            ArrayList tb2 = c2.getTimeBlocks();
            foreach (int ddttl1 in tb1)
            {
                char[] dayarray1 = sum_up(ddttl1 / 1000).Trim().ToCharArray();
                foreach (int ddttl2 in tb2)
                {
                    char[] dayarray2 = sum_up(ddttl2 / 1000).Trim().ToCharArray();
                    foreach(char day1 in dayarray1)
                    {
                        TimeSpan start1 = DateTime.Parse(getTime((ddttl1 / 10) % 100)).TimeOfDay;
                        TimeSpan finish1 = DateTime.Parse(getTime(((ddttl1 / 10) % 100) + ((ddttl1 % 10)))).TimeOfDay;
                        foreach (char day2 in dayarray2)
                        {
                            if (day1 == day2)
                            {
                                TimeSpan start2 = DateTime.Parse(getTime((ddttl2 / 10) % 100)).TimeOfDay;
                                TimeSpan finish2 = DateTime.Parse(getTime(((ddttl2 / 10) % 100) + ((ddttl2 % 10)))).TimeOfDay;
                                if (TimeSpan_Overlap(start1, finish1, start2, finish2))
                                    return true;
                            }

                        }
                    }
                }
            }
            return false;
        }
   
    }
}

