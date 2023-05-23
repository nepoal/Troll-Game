using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {

        bool goLeft, 
            goRight, 
            goUp, 
            goDown;
        int foeSpeed = 10;
        int playerSpeed = 12;
        int score = 0;
        int numbFoeImage;
        Random randX = new Random();
        Random randY = new Random();
        Random randFoeImage = new Random();

        public Form1()
        {
            InitializeComponent();
            Restart();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            textScore.Text = "Score:" + score;

            Move();

            MoveFoe();

            ChangeSpeed();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                goLeft = true;
            if (e.KeyCode == Keys.D) 
                goRight = true;
            if (e.KeyCode == Keys.W)
                goUp = true;
            if (e.KeyCode == Keys.S)
                goDown = true;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                goLeft = false;
            if (e.KeyCode == Keys.D)
                goRight = false;
            if (e.KeyCode == Keys.W)
                goUp = false;
            if (e.KeyCode == Keys.S)
                goDown = false;
        }

        private void Move()
        {
            if (goLeft == true && player.Left > 0)
                player.Left -= playerSpeed;
            if (goRight == true && player.Left + player.Width + 7 < this.ClientSize.Width)
                player.Left += playerSpeed;
            if (goUp == true && player.Top + 10 > 0)
                player.Top -= playerSpeed;
            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
                player.Top += playerSpeed;
        }

        private void ChangeSpeed()
        {
            if (score > 50)
            {
                foeSpeed = 12;
            }
            if (score > 100)
            {
                foeSpeed = 15;
                playerSpeed = 15;
            }
            if (score > 200)
            {
                foeSpeed = 20;
                playerSpeed = 20;
            }
            if (score > 250)
            {
                foeSpeed = 25;
            }
        }

        private void MoveFoe()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "foe")
                {
                    control.Top += foeSpeed;

                    if (control.Top + control.Height > this.ClientSize.Height)
                    {
                        ChangeFoeImage((PictureBox)control);
                        score += 1;
                    }

                    if (player.Bounds.IntersectsWith(control.Bounds))
                    {
                        mainTimer.Stop();
                        MessageBox.Show("Вы проиграли 😥" +
                                        Environment.NewLine +
                                        "Счёт: " + score +
                                        Environment.NewLine +
                                        "Нажмите OK для повтора");
                        Restart();
                    }
                }
            }
        }

        private void ChangeFoeImage(PictureBox tempFoe)
        {
            numbFoeImage = randFoeImage.Next(1, 8);

            switch (numbFoeImage)
            {
                case 1:
                    tempFoe.Image = Properties.Resources.biblethump23;
                    break;
                case 2:
                    tempFoe.Image = Properties.Resources.bloodtrail23;
                    break;
                case 3:
                    tempFoe.Image = Properties.Resources.coolstorybob23;
                    break;
                case 4:
                    tempFoe.Image = Properties.Resources.frankerz23;
                    break;
                case 5:
                    tempFoe.Image = Properties.Resources.notlikethis23;
                    break;
                case 6:
                    tempFoe.Image = Properties.Resources.omegalul23;
                    break;
                case 7:
                    tempFoe.Image = Properties.Resources.pngegg;
                    break;
            }

            tempFoe.Top = randY.Next(20, 600) * -1;

            if ((string)tempFoe.Tag == "foe")
            {
                tempFoe.Left = randX.Next(5, this.ClientSize.Width - tempFoe.Width);
            }
        }

        private void Restart()
        {
            //PlayMusic();
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "foe")
                {
                    control.Top = randY.Next(80, 400) * -1;
                    control.Left = randX.Next(5, this.ClientSize.Width - control.Width);
                }

                player.Left = this.ClientSize.Width / 2;
                player.Top = this.ClientSize.Height / 2;
                player.Image = Properties.Resources.trollface50;

                score = 0;
                foeSpeed = 10;

                goLeft = false;
                goRight = false;
                goUp = false;
                goDown = false;

                mainTimer.Start();
            }
        }

        private void PlayMusic()
        {
            System.Media.SoundPlayer music = new System.Media.SoundPlayer(Properties.Resources.musicForGame);
            music.Play();
        }
    }
}
