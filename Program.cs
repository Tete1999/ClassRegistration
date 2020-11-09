using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace ClassRegistration
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            DataBase DDD = new DataBase("UserDB.in", "courseDB.in", "historyDB.in", "prereq.in");

            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(ref DDD));
        }
    }
}
