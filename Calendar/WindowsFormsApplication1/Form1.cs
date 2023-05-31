using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        static DateTime dt = DateTime.Now;
        static int currentYear = dt.Year;
        static int currentMonth = dt.Month;

        public static int static_month;
        public static int static_year;

        Filebase fb = new Filebase();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            displayDays();
        }

        public void displayDays()
        {
            dayContainer.Controls.Clear();

            //gets first day of month
            DateTime firstOfMonth = new DateTime(currentYear, currentMonth, 1);

            //return number of days in specifc month.
            int count = DateTime.DaysInMonth(currentYear, currentMonth);

            int daysOfWeek = Convert.ToInt32(firstOfMonth.DayOfWeek.ToString("D")) + 1;

            //create blank usercontrol
            if (daysOfWeek == 7) daysOfWeek = 0;
            for (int i = 1; i <= daysOfWeek; i++)
            {
                UserControl1 usBlank = new UserControl1();
                dayContainer.Controls.Add(usBlank);
            }

            //create usercontrol days
            for (int i = 1; i <= count; i++)
            {
                UserControl3 usc = new UserControl3(this);
                usc.days(i);

                // day is today
                if (dt.Year == currentYear && dt.Month == currentMonth && dt.Day == i)
                    usc.BackColor = Color.FromName("Teal");


                if (fb.GetNoteClassByDate(new DateTime(currentYear, currentMonth, i).ToShortDateString()) != null && fb.GetNoteClassByDate(new DateTime(currentYear, currentMonth, i).ToShortDateString()).text != null)
                {
                    NoteClass note = fb.GetNoteClassByDate(new DateTime(currentYear, currentMonth, i).ToShortDateString());
                    usc.setLabel(note.text.Substring(0, note.text.Length > 20 ? 20 : note.text.Length));
                }

                dayContainer.Controls.Add(usc);
            }

            //show month and year
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(currentMonth);
            showlable.Text = monthName + " " + currentYear;

            static_month = currentMonth;
            static_year = currentYear;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure wish you quit", "Exit Application", MessageBoxButtons.YesNo);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }

        }
        
        //Get prev month
        private void prev()
        {
            dayContainer.Controls.Clear();
            if (currentMonth == 1)
            {
                currentYear -= 1;
                currentMonth = 12;
            }
            else currentMonth -= 1;
            displayDays();
        }

        //Get next month
        private void next()
        {
            dayContainer.Controls.Clear();
            if (currentMonth == 12)
            {
                currentYear += 1;
                currentMonth = 1;
            }
            else currentMonth += 1;
            displayDays();
        }

        // goto prev month
        private void prevBtn_Click(object sender, EventArgs e)
        {
            prev();
        }

        //goto next month
        private void nextbtn_Click(object sender, EventArgs e)
        {
            next();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res1 = MessageBox.Show("Are you sure wish you quit", "Cancel", MessageBoxButtons.YesNo);
            if (res1 == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        
        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightSkyBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        private void Todaybtn_Click(object sender, EventArgs e)
        {
            dayContainer.Controls.Clear();
            currentYear = dt.Year;
            currentMonth = dt.Month;
            displayDays();
        }
    }
}
