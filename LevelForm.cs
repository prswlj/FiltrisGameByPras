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
using System.Windows.Forms;

namespace FILTRIS_FINAL
{
    public partial class LevelForm : Form
    {
        // level selection music
        System.Media.SoundPlayer levelMusic;

        public string SelectedDifficulty { get; private set; }

        // constructor
        public LevelForm()
        {
            InitializeComponent();
        }

        // runs when the form loads
        void LevelFormLoad(object sender, EventArgs e)
        {
            LoadBackground();
            LoadMusic();
            SetupUI();
        }

        // loads the background image
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

        // loads the background music
        void LoadMusic()
        {
            try
            {
                string musicPath = System.IO.Path.Combine(
                    Application.StartupPath,
                    "solarflex-roblox-minecraft-fortnite-video-game-music-491492.wav"
                );

                if (System.IO.File.Exists(musicPath))
                {
                    levelMusic = new System.Media.SoundPlayer(musicPath);
                    levelMusic.PlayLooping();
                }
            }
            catch { }
        }

        // ui setup
        void SetupUI()
        {
            levelLabel.Left = (this.ClientSize.Width - levelLabel.Width) / 2;

            easyButton.Left = (this.ClientSize.Width - easyButton.Width) / 2;
            mediumButton.Left = (this.ClientSize.Width - mediumButton.Width) / 2;
            hardButton.Left = (this.ClientSize.Width - hardButton.Width) / 2;

            chkClue.Left = (this.ClientSize.Width - chkClue.Width) / 2;

            levelLabel.Top = 30;
            easyButton.Top = 120;
            mediumButton.Top = 180;
            hardButton.Top = 240;
            chkClue.Top = 300;

            this.BackColor = Color.FromArgb(18, 18, 30);

            levelLabel.ForeColor = Color.Cyan;

            btnBack.BackColor = Color.FromArgb(200, 150, 40);
            easyButton.BackColor = Color.FromArgb(50, 150, 50);
            mediumButton.BackColor = Color.FromArgb(200, 150, 40);
            hardButton.BackColor = Color.FromArgb(180, 50, 50);

            btnBack.ForeColor = Color.White;
            easyButton.ForeColor = Color.White;
            mediumButton.ForeColor = Color.White;
            hardButton.ForeColor = Color.White;

            chkClue.ForeColor = Color.White;
        }

        // easy difficulty
        void EasyButtonClick(object sender, EventArgs e)
        {
            OpenGame("Easy");
        }

        // medium difficulty
        void MediumButtonClick(object sender, EventArgs e)
        {
            OpenGame("Medium");
        }

        // hard difficulty
        void HardButtonClick(object sender, EventArgs e)
        {
            OpenGame("Hard");
        }

        // opens the game form
        private void OpenGame(string difficulty)
        {
            if (levelMusic != null)
                levelMusic.Stop();

            GameForm game = new GameForm(difficulty, chkClue.Checked);
            game.Show();

            this.Hide();
        }

        // returns to main menu
        void BtnBackClick(object sender, EventArgs e)
        {
            if (levelMusic != null)
                levelMusic.Stop();

            MainForm menu = new MainForm();
            menu.Show();

            this.Close();
        }
    }
}