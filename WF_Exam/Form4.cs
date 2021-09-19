using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Exam
{
    public partial class Form4 : Form
    {
        DictionaryWord form4_newWord;  
        bool fill;

        public Form4()
        {
            InitializeComponent();
        }
        public Form4(DictionaryWord word, bool fill)
        {
            InitializeComponent();
            this.form4_newWord = word;
            this.fill = fill;
            if (fill == false)
            {
                textBox1.Text = form4_newWord.Word;     
                foreach (var item in form4_newWord.Translation)
                {
                    richTextBox1.Text += item + ",";
                }              
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (form4_newWord == null) form4_newWord = new DictionaryWord();
            form4_newWord.Word = textBox1.Text;
            string[] str = richTextBox1.Text.Split(',');
            form4_newWord.Translation.Clear();
            foreach (var item in str)
            {
                form4_newWord.Translation.Add(item);
            }
            this.DialogResult = DialogResult.OK;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image image = Properties.Resources.Dictionary2;
            Rectangle rectangle = new Rectangle(new Point(20, 124), new Size(132, 160));
            g.DrawImage(image, rectangle);
        }
    }
}
