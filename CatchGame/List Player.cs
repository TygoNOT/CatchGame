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

namespace CatchGame
{
    /// <summary>
    /// This form shows all saved records after the game in which you can see which user scored how many points and perform a search filter by the name of the played users
    /// Button 2 closes this form
    /// The List_Player_Load method opens a text document that stores recorded data about games and displays them on the user's screen
    /// The full_txt function will reopen the text file and display all the data on the screen
    /// Button 1 searches for a specific user in a text document by his name and displays information about him on the screen
    /// Button 3 using the full_txt function returns displaying information about all users after searching for a specific one 
    /// </summary>
    public partial class List_Player : Form
    {
        public List_Player()
        {
            InitializeComponent();
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void List_Player_Load(object sender, EventArgs e)
        {
            FileStream fs;
            StreamReader sr;
            fs = new FileStream("scores.txt", FileMode.Open, FileAccess.Read);
            sr = new StreamReader(fs);
            richTextBox1.Text = sr.ReadToEnd();
            sr.Close();
            fs.Close();

        }
        private string full_txt()
        {
            string full_txt;
            FileStream fs;
            StreamReader sr;
            fs = new FileStream("scores.txt", FileMode.Open, FileAccess.Read);
            sr = new StreamReader(fs);
            full_txt = sr.ReadToEnd();
            return full_txt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Search Box is empty! Please Enter player name.", "Empty Search Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int start = 0;
                int end = richTextBox1.Text.IndexOf(';');

                while (start < end)
                {
                    richTextBox1.Find(textBox2.Text, start, richTextBox1.TextLength, RichTextBoxFinds.MatchCase);
                    start = richTextBox1.Text.IndexOf(textBox2.Text);
                    string word = richTextBox1.Text.Substring(start, end);
                    richTextBox1.Text = word;
                    start= end ;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string full_lst;
            full_lst = full_txt();

            if (full_lst.Length > richTextBox1.Text.Length)
            {
                richTextBox1.Text = full_lst;
            }

        }
    }
}
