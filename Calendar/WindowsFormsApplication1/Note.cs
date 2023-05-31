using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Note : Form
    {
        Filebase fb = new Filebase();
        public Form1 nf;
        DateTime targetDate;

        public Note(DateTime targetDate, Form1 nf)
        {
            InitializeComponent();
            this.targetDate = targetDate;
            this.nf = nf;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (fb.GetNoteClassByDate(targetDate.ToShortDateString()) == null)
                fb.Add(new NoteClass(targetDate.ToShortDateString(), textBox1.Text));
            else
                fb.Update(targetDate.ToShortDateString(), textBox1.Text);


            if (nf != null) nf.displayDays();

            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
                Form1 f1 = new Form1();
                f1.Visible = false;
                this.Close();
        }
        private void Note_Load(object sender, EventArgs e)
        {
            string[] lines = fb.ReadLinesByDate(targetDate.ToShortDateString());
            label1.Text = targetDate.ToLongDateString();
            
            if(lines != null)
                foreach (string line in lines)
                    textBox1.Text += line + "\r\n";
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightSkyBlue;
        }
        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.LightSkyBlue;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.White;
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
