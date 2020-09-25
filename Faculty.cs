using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration
{
    public class Faculty
    {
        private string user;
        private string pass;
        private string firstName;
        private string middleName;
        private string lastName;
        public Faculty(string user, string pass, string firstName, string middleName, string lastName)
        {
            this.user = user.ToLower();
            this.pass = pass;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
        }

        public string getUser() { return this.user; }
        public string getPass() { return this.pass; }
        public string getFirst() { return this.firstName; }
        public string getMiddle() { return this.middleName; }
        public string getLast() { return this.lastName; }


        public void setUser(string user) { this.user = user; }
        public void setPass(string pass) { this.pass = pass; }
        public void setFirst(string firstName) { this.firstName = firstName; }
        public void setMiddle(string middleName) { this.middleName = middleName; }
        public void setLast(string lastName) { this.lastName = lastName; }

        public string getFullName() { return (this.firstName + " " + this.middleName + " " + this.lastName); }

    }
}
