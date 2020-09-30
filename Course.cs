﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
 


        public Course(string courseName, string title, string instructor, decimal credit, int seats, ArrayList timeBlocks)
        {
            this.courseName = courseName;
            this.title = title;
            this.instructor = instructor;
            this.credit = credit;
            this.seats = seats;
            this.timeBlocks = timeBlocks;
        }

        private string getCourseName() { return this.courseName; }
        private string getTitle() { return this.title; }
        private string getInstructor() { return this.instructor; }
        private decimal getCredit() { return this.credit; }
        public int getSeats() { return this.seats; }
        private ArrayList getTimeBlocks() { return this.timeBlocks; }

        private void addTimeBlocks(int timeBlock) { timeBlocks.Add(timeBlock);}
        private void setSeats(int s) { this.seats = s; }

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
            string str = (s.Hours).ToString() + ":" + min + AMPM;
            return str;
        }

        static private string getSchedule(ArrayList timeBlocks)
        {
            string output = "";
            string tab = "\n\t\t\t\t\t\t\t\t\t\t\t";
           
            foreach (int ddttl in timeBlocks)
            {
                output += sum_up(ddttl / 1000) + "\t" + getTime((ddttl / 10) % 100) + "-" + getTime(((ddttl / 10) % 100) + ((ddttl % 10))) + tab;
            }
            return output;
        }

        public override string ToString()
        {
            return getCourseName() + getTitle() + getInstructor() +  getCredit() + "\t" + getSchedule(timeBlocks);
        }
    }
}
