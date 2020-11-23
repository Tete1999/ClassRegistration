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
    public partial class Form17 : Form
    {
        private DataBase DDD;
        private string user;
        private ArrayList timeblocks;
        public bool finished = true;
        public bool forcedclose = true;
        private string[] days = new[] { "M", "T", "W", "R", "F" };
        private List<string> start_time = new List<string> { "5:00 AM", "5:30 AM", "6:00 AM", "6:30 AM",
                "7:00 AM", "7:30 AM", "8:00 AM", "8:30 AM", "9:00 AM", "9:30 AM", "10:00 AM", "10:30 AM", 
                "11:00 AM","11:30 AM", "12:00 PM", "12:30 PM", "1:00 PM", "1:30 PM", "2:00 PM", "2:30 PM",
                "3:00 PM","3:30 PM", "4:00 PM", "4:30 PM", "5:00 PM", "5:30 PM", "6:00 PM", "6:30 PM", "7:00 PM",
                "7:30 PM", "8:00 PM", "8:30 PM", "9:00 PM"};
        private List<int> ddttltime = new List<int>();
        private List<string> class_length = new List<string> { "30 Minutes", "60 Minutes", "90 Minutes",
            "120 Minutes", "150 Minutes", "180 Minutes", "210 Minutes", "240 Minutes" };
        
        public Form17(ref DataBase master, string user, ref ArrayList tb, ref bool finished)
        {
            this.DDD = master;
            this.user = user;
            this.timeblocks = tb;
            //this.finished = finished;
            InitializeComponent();
            for (int i = 10; i < 43; i++)
                ddttltime.Add(i);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DataSource = null;
            comboBox1.DataSource = start_time;

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DataSource = null;
            comboBox2.DataSource = class_length;

            checkedListBox1.Items.AddRange(days);

            if (timeblocks.Count != 0)
                Arranged.Visible = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int getdaycode(CheckedListBox.CheckedItemCollection checkedItems)
        {
            int daycode = 0;
            foreach (string s in checkedItems)
            {
                switch (s)
                {
                    case "M":
                        daycode += 1;
                        break;
                    case "T":
                        daycode += 2;
                        break;
                    case "W":
                        daycode += 4;
                        break;
                    case "R":
                        daycode += 8;
                        break;
                    case "F":
                        daycode += 16;
                        break;
                    default:
                        break;
                }
            }
            return daycode;
        }

        private void Add_More_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                int daycode = getdaycode(checkedListBox1.CheckedItems);
                int numblocks = comboBox2.SelectedIndex + 1;
                int timecode = ddttltime[comboBox1.SelectedIndex];
                if (timecode + numblocks < 48)
                {
                    int tbcode = daycode * 1000 + timecode * 10 + numblocks;
                    if (!DDD.Overlap(timeblocks, tbcode))
                    {
                        timeblocks.Add(tbcode);
                        finished = false;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Time Conflict");
                }
                else
                {
                    MessageBox.Show("Cannot Schedule Past 11:30PM");
                }
            }
            else
            {
                MessageBox.Show("Please Select Days");
            }
        }

        private void Finish_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                int daycode = getdaycode(checkedListBox1.CheckedItems);
                int numblocks = comboBox2.SelectedIndex + 1;
                int timecode = ddttltime[comboBox1.SelectedIndex];
                if (timecode + numblocks < 48)
                {
                    int tbcode = daycode * 1000 + timecode * 10 + numblocks;
                    if (!DDD.Overlap(timeblocks, tbcode))
                    {
                        timeblocks.Add(tbcode);
                        finished = true;
                        forcedclose = false;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Time Conflict");
                }
                else
                {
                    MessageBox.Show("Cannot Schedule Past 11:30PM");
                }
            }
            else
            {
                MessageBox.Show("Please Select Days");
            }
        }

        private void Arranged_Click(object sender, EventArgs e)
        {
            timeblocks.Clear();
            timeblocks.Add(0);
            finished = true;
            forcedclose = false;
            this.Close();
        }

        private void Form17_Load(object sender, EventArgs e)
        {
        }

        private void Form17_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
