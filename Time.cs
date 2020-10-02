using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration
{
    class Time
    {
        private int start;
        private int finish;





        public Time(int ddttl)
        {
            this.start = ((ddttl / 10) % 100) / 2;
            this.finish = start + (ddttl % 10) / 2;
        }

        public bool compareTimes(Time class1, Time class2)
        {
            if (class2.start < class1.finish && class1.finish < class2.finish) { return false; }
            else if (class1.start < class2.finish && class2.finish < class1.finish) { return false; }
            else if (class1.start == class2.start && class1.finish == class2.finish) { return false; }
            else { return true; }
        } 



    }
}
