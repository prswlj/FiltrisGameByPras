/*
 * Created by SharpDevelop.
 * User: Praswel
 * Date: 29/04/2026
 * Time: 6:43 pm
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace FILTRIS_FINAL
{
    public partial class GameOverForm : Form
    {
        string difficulty;
        int score;

        // game over sound
        System.Media.SoundPlayer gameOverSound;

        // constructor
        public GameOverForm(int finalScore, string selectedDifficulty)
        {
            InitializeComponent();

            score = finalScore;
            difficulty = selectedDifficulty;

            SetupUI();
        }

        // restart button
        void BtnRestartClick(object sender, EventArgs e)
        {
            if (gameOverSound != null)
                gameOverSound.Stop();

            GameForm game = new GameForm(difficulty, true);
            game.Show();

            this.Close();
        }

        // return to main menu
        void BtnMenuClick(object sender, EventArgs e)
        {
            if (gameOverSound != null)
                gameOverSound.Stop();

            MainForm menu = new MainForm();
            menu.Show();

            this.Close();
        }

        // updates score text
        private void SetupUI()
        {
            lblScore.Text = "Score: " + score;
        }

        // runs when form loads
        void GameOverFormLoad(object sender, EventArgs e)
        {
            LoadBackground();
            LoadSound();
            SetupLayout();
        }

        // loads background image
        void LoadBackground()
        {
            try
            {
                string bgPath = System.IO.Path.Combine(
                    Application.StartupPath,
                    "26176787_abstract_pixel_design_background_2104.jpg"
                );

                if (System.IO.File.Exists(bgPath))
                {
                    this.BackgroundImage = Image.FromFile(bgPath);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch { }
        }

        // plays game over sound
        void LoadSound()
        {
            try
            {
                string soundPath = System.IO.Path.Combine(
                    Application.StartupPath,
                    "alphix-game-over-417465.wav"
                );

                if (System.IO.File.Exists(soundPath))
                {
                    gameOverSound = new System.Media.SoundPlayer(soundPath);
                    gameOverSound.Play();
                }
            }
            catch { }
        }

        // ui setup
        void SetupLayout()
        {
            lblGameOver.AutoSize = true;
            lblScore.AutoSize = true;

            lblGameOver.Left =
                (this.ClientSize.Width - lblGameOver.Width) / 2;

            lblScore.Left =
                (this.ClientSize.Width - lblScore.Width) / 2;

            lblGameOver.Top = 50;
            lblScore.Top = 170;

            int buttonSpacing = 10;

            int totalWidth =
                btnRestart.Width +
                buttonSpacing +
                btnMenu.Width;

            int startX =
                (this.ClientSize.Width - totalWidth) / 2;

            btnRestart.Left = startX;

            btnMenu.Left =
                startX + btnRestart.Width + buttonSpacing;

            btnRestart.Top = 280;
            btnMenu.Top = 280;

            this.BackColor = Color.FromArgb(18, 18, 30);
        }
    }
}