using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatchGame
{
    /// <summary>
    /// This form is the starting form when starting the program
    /// Button 2 is responsible for closing the application
    /// Buttons 1 saves the username, collapses this form and opens a new form
    /// the get_player method is responsible for copying the username
    /// </summary>
    public partial class StartMenu : Form
    {
        public static StartMenu Instance;
        public string player_name;
        public StartMenu()
        {
            InitializeComponent();
            Instance = this;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player_name = get_player();

            this.Hide();
            Form1 newForm = new Form1();
            newForm.ShowDialog();
        }
        public string get_player()
        {
            string player;
            player = textBox1.Text;
            return player;
        }
    }
}
