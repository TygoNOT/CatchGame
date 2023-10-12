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
    /// This form is the main form in which the main game logic is located
    /// Button 1 launches the method responsible for launching the game
    /// Method StartGame Resets the score counter, starts a timer for the duration of the game and turns on the display of additional fields for the user such as: score counter, remaining time and goals (which are circles of different colors)
    /// The GenerateRandomImageIndex method makes the appearance of the target type random
    /// The SetRandomLocation method creates randomness of target appearances in a certain area
    /// The save method writes data about the game to a text document
    /// The EndGame method is launched at the end of the game (at the end of the timer), notifying the user about the end of the game, asking him to write down the result and hides the score counter and timer fields
    /// The GameTimer_Tick method starts a timer and determines the selected difficulty level (each selected difficulty level changes the speed at which the target appears and closes)
    /// The Level method changes the size of the goal upon reaching a certain number of points
    /// The gamePictureBox_Click_1 method gives different bonuses for clicking on different targets
    /// Button 2 closes applications
    /// Button 3 opens a form with a table of players
    /// </summary>
    public partial class Form1 : Form
    {
        private Image[] gameImages;
        private int currentImageIndex;
        private int score;
        private int remainingTime;
        private int timeSinceImageDisplayed;
        private Random random;
        private Timer gameTimer;
        bool level1 = false;
        bool level2 = false;
        bool level3 = false;
        bool level4 = false;
        bool level5 = false;
        public Form1()
        {
            InitializeComponent();
            gameImages = new Image[] {
                Properties.Resources.G_circle,
                Properties.Resources.R_circle,
                Properties.Resources.Y_circle
            };
            scoreLabel.Visible = false;
            remainingTimeLabel.Visible = false;
            gamePictureBox.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
        }
        private void StartGame()
        {
            score = 0;
            remainingTime = 60;
            scoreLabel.Visible = true;
            remainingTimeLabel.Visible = true;
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            scoreLabel.Text = score.ToString();
            remainingTimeLabel.Text = remainingTime.ToString();
            gamePictureBox.Visible = true;
            GenerateRandomImageIndex();
            SetRandomLocation();
            gamePictureBox.Image = gameImages[currentImageIndex];
        }
        private void GenerateRandomImageIndex()
        {
            currentImageIndex = new Random().Next(gameImages.Length);
        }
        private void SetRandomLocation()
        {
            var maxWidth = panel4.Width - gamePictureBox.Width;
            var maxHeight = panel4.Height - gamePictureBox.Height;
            var rnd = new Random();
            var x = rnd.Next(0, maxWidth);
            var y = rnd.Next(0, maxHeight);
            gamePictureBox.Location = new Point(x, y);
        }
        private void save(string winner, string points)
        {
            FileStream fs = new FileStream("scores.txt",
                FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine($"{winner} has scored {points};");
            sw.Close();
            fs.Close();
        }
        private void EndGame()
        {
            DialogResult end = new DialogResult();
            string name;
            name = StartMenu.Instance.player_name;
            gameTimer.Stop();
            gamePictureBox.Visible = false;
            end = MessageBox.Show($"Game over! Your score is {score}. Do you wanna save result?", "Information",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (end == DialogResult.Yes)
            {
                save(name, score.ToString());
            }
            else {
                Application.Exit();
            }
            scoreLabel.Visible = false;
            remainingTimeLabel.Visible = false;
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            remainingTime--;
            remainingTimeLabel.Text = remainingTime.ToString();
            if (remainingTime <= 0)
            {
                EndGame();
            }
            timeSinceImageDisplayed++;

            if (radioButton1.Checked & timeSinceImageDisplayed >= 3)
            {
                GenerateRandomImageIndex();
                SetRandomLocation();
                gamePictureBox.Image = gameImages[currentImageIndex];
                timeSinceImageDisplayed = 0;
            }
            else if (radioButton2.Checked & timeSinceImageDisplayed >= 2)
            {
                GenerateRandomImageIndex();
                SetRandomLocation();
                gamePictureBox.Image = gameImages[currentImageIndex];
                timeSinceImageDisplayed = 0;
            }
            else if (radioButton3.Checked & timeSinceImageDisplayed >= 1)
            {
                GenerateRandomImageIndex();
                SetRandomLocation();
                gamePictureBox.Image = gameImages[currentImageIndex];
                timeSinceImageDisplayed = 0;
            }
        }
        public void Level()
        {
            if (score >= 50 && !level5)
            {
                level5 = true;
                gamePictureBox.Size = new Size(gamePictureBox.Width / 2, gamePictureBox.Height / 2);
            }
            else if (score >= 40 && !level4)
            {
                level4 = true;
                gamePictureBox.Size = new Size(120, 110);
            }
            else if (score >= 30 && !level3)
            {
                level3 = true;
                gamePictureBox.Size = new Size(180, 170);
            }
            else if (score >= 20 && !level2)
            {
                level2 = true;
                gamePictureBox.Size = new Size(240, 230);
            }
            else if (score >= 10 && !level1)
            {
                level1 = true;
                gamePictureBox.Size = new Size(300, 290);
            }
        }
        private void gamePictureBox_Click_1(object sender, EventArgs e)
        {
            switch (currentImageIndex)
            {
                case 0:
                    score += 1;
                    break;
                case 1:
                    score += 2;
                    break;
                case 2:
                    remainingTime += 3;
                    break;
            }
            scoreLabel.Text = score.ToString();
            GenerateRandomImageIndex();
            Level();
            SetRandomLocation();
            gamePictureBox.Image = gameImages[currentImageIndex];
            timeSinceImageDisplayed = 0;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List_Player newForm = new List_Player();
            newForm.ShowDialog();
        }
    }
}
