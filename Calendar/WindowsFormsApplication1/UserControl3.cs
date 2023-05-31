using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UserControl3 : UserControl
    {
        public static string static_day;
        public Form1 nf;

        public UserControl3(Form1 nf = null)
        { 
            InitializeComponent();
            this.nf = nf;
        }
        public void days(int numDay)
        {
            lbdays.Text = numDay + "";
        }

        public void setLabel(string str)
        {
            label1.Text = str;
        }

        public void DisplayDays()
        {
            if(nf != null)
                nf.displayDays();
        }

        private void Note_Click(object sender, EventArgs e)
        {
            
            static_day = lbdays.Text;
            DateTime dt = new DateTime(Form1.static_year, Form1.static_month, int.Parse(static_day));
            Note n1 = new Note(dt, nf);
            n1.Visible = true;
        }

       
    }
}
