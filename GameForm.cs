/*
 * Created by SharpDevelop.
 * User: Praswel
 * Date: 28/04/2026
 * Time: 6:31 pm
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FILTRIS_FINAL
{
    public partial class GameForm : Form
    {
        // game variables
        string currentWord;
        string difficulty;
        bool clueEnabled;

        int score = 0;
        int health = 100;
        int speed = 5;

        Random rnd = new Random();

        // game music
        System.Media.SoundPlayer bgMusic;

        // word lists
        string[] easyWords = { "wika", "batas", "gamot" };
        string[] mediumWords = { "balita", "sanaysay", "panitikan" };
        string[] hardWords = { "kultura", "kasaysayan", "karunungan" };

        // constructor
        public GameForm(string selectedDifficulty, bool clueOn)
        {
            InitializeComponent();

            difficulty = selectedDifficulty;
            clueEnabled = clueOn;

            LoadAssets();

            ApplyDifficulty();
            StartGame();
        }

        // loads background image and music
        void LoadAssets()
        {
            try
            {
                string bgPath = Application.StartupPath +
                    "\\26176787_abstract_pixel_design_background_2104.jpg";

                if (System.IO.File.Exists(bgPath))
                {
                    this.BackgroundImage = Image.FromFile(bgPath);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

                string musicPath = Application.StartupPath +
                    "\\viacheslavstarostin-game-gaming-video-game-music-471936.wav";

                if (System.IO.File.Exists(musicPath))
                {
                    bgMusic = new System.Media.SoundPlayer(musicPath);
                    bgMusic.PlayLooping();
                }
            }
            catch
            {
                // prevents crash if file is missing
            }
        }

        // ui setup
        void GameFormLoad(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(18, 18, 30);

            topPanel.BackColor = Color.FromArgb(30, 30, 50);

            difficultyLabel.ForeColor = Color.White;
            scoreLabel.ForeColor = Color.White;
            clueLabel.ForeColor = Color.LightGray;
            wordLabel.ForeColor = Color.Cyan;

            answerBox.BackColor = Color.FromArgb(40, 40, 60);
            answerBox.ForeColor = Color.White;
        }

        // starts the game
        void StartGame()
        {
            score = 0;

            scoreLabel.Text = "Score: 0";

            progressBar1.Maximum = health;
            progressBar1.Value = health;

            difficultyLabel.Text = "Difficulty: " + difficulty;

            gameTimer.Interval = 100;
            gameTimer.Start();

            GenerateWord();
        }

        // changes settings depending on difficulty
        void ApplyDifficulty()
        {
            if (difficulty == "Easy")
            {
                speed = 3;
                health = 100;
            }
            else if (difficulty == "Medium")
            {
                speed = 5;
                health = 100;
            }
            else
            {
                speed = 13;
                health = 80;
            }
        }

        // main game loop
        void GameTimerTick(object sender, EventArgs e)
        {
            wordLabel.Top += speed;

            if (wordLabel.Top > this.Height)
            {
                LoseHealth();
                GenerateWord();
            }

            if (health <= 0)
            {
                gameTimer.Stop();

                if (bgMusic != null)
                    bgMusic.Stop();

                GameOverForm go = new GameOverForm(score, difficulty);
                go.Show();

                this.Close();
            }
        }

        // checks player input
        void AnswerBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (answerBox.Text.Trim().ToLower() == currentWord)
                {
                    score++;
                    scoreLabel.Text = "Score: " + score;

                    if (speed < 15)
                        speed++;

                    GenerateWord();
                }

                answerBox.Clear();
            }
        }

        // generates random scrambled words
        void GenerateWord()
        {
            if (difficulty == "Easy")
                currentWord = easyWords[rnd.Next(easyWords.Length)];
            else if (difficulty == "Medium")
                currentWord = mediumWords[rnd.Next(mediumWords.Length)];
            else
                currentWord = hardWords[rnd.Next(hardWords.Length)];

            wordLabel.Text = Scramble(currentWord);

            clueLabel.Text = clueEnabled ? "Word: " + currentWord : "";

            wordLabel.Top = 0;
            wordLabel.Left = (this.ClientSize.Width - wordLabel.Width) / 2;
        }

        // lowers player health
        void LoseHealth()
        {
            health -= 10;

            if (health < 0)
                health = 0;

            progressBar1.Value = health;
        }

        // scrambles the word letters
        string Scramble(string word)
        {
            return new string(word.OrderBy(x => rnd.Next()).ToArray());
        }

        // returns to level form
        void BtnExitClick(object sender, EventArgs e)
        {
            gameTimer.Stop();

            if (bgMusic != null)
                bgMusic.Stop();

            LevelForm level = new LevelForm();
            level.Show();

            this.Close();
        }
    }
}