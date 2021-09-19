using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Exam
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if (File.Exists(@"Folder1\History.txt"))
            {
                StreamReader reader = new StreamReader(@"Folder1\History.txt");
                string str = reader.ReadToEnd();
                reader.Close();
                richTextBox1.Text = str;
            }
            else
                MessageBox.Show("No History!");
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image image = Properties.Resources.books2;
            Rectangle rectangle = new Rectangle(new Point(170, 16), new Size(164, 90));
            g.DrawImage(image, rectangle);
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            File.Delete(@"Folder1\History.txt");
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
