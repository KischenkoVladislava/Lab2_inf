using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2_inf
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button1.BackColor = Class1.graphicFilesColor;
            button2.BackColor = Class1.oficeFilesColor;
            button3.BackColor = Class1.archiveFilesColor;
            button4.BackColor = Class1.exeDllFilesColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                Class1.graphicFilesColor = cd.Color;
                button1.BackColor = Class1.graphicFilesColor;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                Class1.oficeFilesColor = cd.Color;
                button2.BackColor = Class1.oficeFilesColor;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                Class1.archiveFilesColor = cd.Color;
                button3.BackColor = Class1.archiveFilesColor;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                Class1.exeDllFilesColor = cd.Color;
                button4.BackColor = Class1.exeDllFilesColor;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Class1.update = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
