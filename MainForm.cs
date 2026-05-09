/*
 * Created by SharpDevelop.
 * User: Praswel
 * Date: 28/04/2026
 * Time: 6:27 pm
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace FILTRIS_FINAL
{
    public partial class MainForm : Form
    {
        // main menu music
        System.Media.SoundPlayer mainMusic;

        // constructor
        public MainForm()
        {
            InitializeComponent();
        }

        // runs when the form loads
        void MainFormLoad(object sender, EventArgs e)
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

        // loads the menu music
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
                    mainMusic = new System.Media.SoundPlayer(musicPath);
                    mainMusic.PlayLooping();
                }
            }
            catch { }
        }

        // ui setup
        void SetupUI()
        {
            titleLabel.Left = (this.ClientSize.Width - titleLabel.Width) / 2;
            startButton.Left = (this.ClientSize.Width - startButton.Width) / 2;
            exitButton.Left = (this.ClientSize.Width - exitButton.Width) / 2;

            titleLabel.Top = 80;
            startButton.Top = 180;
            exitButton.Top = 240;

            this.BackColor = Color.FromArgb(18, 18, 30);

            titleLabel.ForeColor = Color.Cyan;

            startButton.BackColor = Color.FromArgb(50, 150, 50);
            startButton.ForeColor = Color.White;

            exitButton.BackColor = Color.FromArgb(180, 50, 50);
            exitButton.ForeColor = Color.White;
        }

        // opens the level form
        void StartButtonClick(object sender, EventArgs e)
        {
            if (mainMusic != null)
                mainMusic.Stop();

            LevelForm level = new LevelForm();
            level.Show();

            this.Hide();
        }

        // closes the game
        void ExitButtonClick(object sender, EventArgs e)
        {
            if (mainMusic != null)
                mainMusic.Stop();

            Environment.Exit(0);
        }

        // makes sure everything closes properly
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (mainMusic != null)
                mainMusic.Stop();

            Application.Exit();

            base.OnFormClosed(e);
        }
    }
}