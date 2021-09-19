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
using System.Xml.Serialization;

namespace WF_Exam
{
    public partial class Form1 : Form
    {
        MyDictionary dictionary = new MyDictionary();
        List<string> historyList = new List<string>();
        List<string> printList = new List<string>();
        int index = 0;
        public Form1()
        {
            InitializeComponent();         
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image image = Properties.Resources.books;
            Rectangle rectangle = new Rectangle(new Point(27, 50), new Size(212, 145));
            g.DrawImage(image, rectangle);
        }

        private void Open_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                string path = @"Folder\";
                string chosen_path = comboBox1.SelectedItem.ToString() + "-" + comboBox2.SelectedItem.ToString();
                XmlSerializer formatter = new XmlSerializer(typeof(MyDictionary));
                using (FileStream fs = new FileStream(path + chosen_path + ".xml", FileMode.Open))
                {
                    dictionary = (MyDictionary)formatter.Deserialize(fs);
                }
                foreach (var item in dictionary.Words)
                {
                    listBox1.Items.Add(item);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Sort_A_Z_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var item in dictionary.Words.OrderBy(d => d.Word))
            {
                listBox1.Items.Add(item);
            }
        }
        private void Sort_Z_A_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var item in dictionary.Words.OrderByDescending(d => d.Word))
            {
                listBox1.Items.Add(item);
            }
        }
        private void Find_Click(object sender, EventArgs e)
        {
            string path1 = @"Folder1\";
            listBox2.Items.Clear();
            try
            {
                var result = dictionary.Words.Where(w => w.Word == toolStripTextBox1.Text).Select(t => t).DefaultIfEmpty().First();
                if (result != null)
                {
                    listBox2.Items.Add(result);
                    historyList.Add(result.ToString());
                    dictionary.SaveHistory(path1, result);
                    printList.Add(result.ToString());
                    dictionary.SaveResult(path1, result);
                }
                else { MessageBox.Show($" Word: \"{toolStripTextBox1.Text}\" not found!"); }          
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void History_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            DialogResult result = form2.ShowDialog();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            string path1 = @"Folder1\";
            listBox1.Items.Clear();
            foreach (var item in printList)
            {
                listBox1.Items.Add(item);
            } 
            printDialog1.ShowDialog();
            File.Delete(path1 + "Result.txt");
        }

        private void Create_Click(object sender, EventArgs e)
        {
            string path = @"Folder\";
            Form3 form3 = new Form3();
            DialogResult result = form3.ShowDialog();
            if (result == DialogResult.OK)
            {              
                string[] str = form3.form3_Dictionary.DictionaryType.Split('-');            
                    if(!comboBox1.Items.Contains(str[0]))
                    comboBox1.Items.Add(str[0]);
                    if(!comboBox2.Items.Contains(str[1]))
                    comboBox2.Items.Add(str[1]);
                XmlSerializer formatter = new XmlSerializer(typeof(MyDictionary));
                using (FileStream fs = new FileStream(path + form3.form3_Dictionary.DictionaryType + ".xml", FileMode.Create))
                {
                    formatter.Serialize(fs, form3.form3_Dictionary);
                }
                MessageBox.Show("Dictionary Created!");
            }
        }
        private void DeleteWord_Click(object sender, EventArgs e)
        {
            string path = @"Folder\";
            string chosen_path = comboBox1.SelectedItem.ToString() + "-" + comboBox2.SelectedItem.ToString();
            index = listBox1.SelectedIndex;
            listBox1.Items.Remove(listBox1.SelectedItem);
            dictionary.Words.RemoveAt(index);
            XmlSerializer formatter = new XmlSerializer(typeof(MyDictionary));
            using (FileStream fs = new FileStream(path + chosen_path + ".xml", FileMode.Truncate))
            {
                formatter.Serialize(fs, dictionary);
            }
        }
        private void Add_Click(object sender, EventArgs e)
        {          
            if (this.comboBox1.SelectedItem == null || this.comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Choose Dictionary!");
            }
            else
            {
                string path = @"Folder\";
                string chosen_path = comboBox1.SelectedItem.ToString() + "-" + comboBox2.SelectedItem.ToString(); 
                DictionaryWord word = new DictionaryWord(); ;
                Form4 form4add = new Form4(word, true);
                DialogResult result = form4add.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        if (dictionary.Words.Exists(w => w.Word == word.Word))
                            throw new Exception("Word already exists!");
                        else
                        {
                            listBox1.Items.Add(word);
                            dictionary.Words.Add(word);
                            XmlSerializer formatter = new XmlSerializer(typeof(MyDictionary));
                            using (FileStream fs = new FileStream(path + chosen_path + ".xml", FileMode.Truncate))
                            {
                                formatter.Serialize(fs, dictionary);
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message);}                   
                }
            }
        }

        private void Correct_Click(object sender, EventArgs e)
        {          
            if (this.comboBox1.SelectedItem == null || this.comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Choose Dictionary!");
            }
            else
            {
                string path = @"Folder\";
                string chosen_path = comboBox1.SelectedItem.ToString() + "-" + comboBox2.SelectedItem.ToString();
                if (this.listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Choose Word!");
                }
                else
                {
                    index = listBox1.SelectedIndex;
                    DictionaryWord p = null;
                    p = (DictionaryWord)listBox1.Items[index];
                    Form4 form4correct = new Form4(p, false);
                    form4correct.Text = "Correct Word";
                    DialogResult result = form4correct.ShowDialog();
                    listBox1.Items.RemoveAt(index);
                    listBox1.Items.Insert(index, p);
                    listBox1.SelectedIndex = index;
                    if (result == DialogResult.OK)
                    {
                        dictionary.Words.RemoveAt(index);
                        dictionary.Words.Insert(index, p);
                        XmlSerializer formatter = new XmlSerializer(typeof(MyDictionary));
                        using (FileStream fs = new FileStream(path + chosen_path + ".xml", FileMode.Truncate))
                        {
                            formatter.Serialize(fs, dictionary);
                        }
                    }
                }
            }
        }
    }
}
