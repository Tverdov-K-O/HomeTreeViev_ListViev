using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeTreeViev_ListViev
{
    public partial class Form2 : Form
    {
        public string   _Name       
        {
            get { return this.textBox1.Text; } 
            set { textBox1.Text = value; } 
        }
        public double   _Price      
        { 
            get { return Convert.ToDouble(this.textBox2.Text); }
            set { textBox2.Text = value.ToString(); }
         }
        public DateTime _CreateDate { 
            get { return this.dateTimePicker1.Value; }
            set { this.dateTimePicker1.Value = value; }
        }
        public DateTime _ExpDate    { 
            get { return this.dateTimePicker2.Value; }
            set { this.dateTimePicker2.Value = value; }
        }

        public Form2(string tex)
        {
            InitializeComponent();
            label5.Text =  tex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
                this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
