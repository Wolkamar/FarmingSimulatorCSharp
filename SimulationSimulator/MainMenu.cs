using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;
using System.Threading;

namespace SimulationSimulator
{
    public partial class MainMenu : Form
    {

        const int WM_PARENTNOTIFY = 0x210;
        const int WM_LBUTTONDOWN = 0x201;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN || (m.Msg == WM_PARENTNOTIFY && (int)m.WParam == WM_LBUTTONDOWN))
            {
                System.Media.SoundPlayer click_sound = new System.Media.SoundPlayer("click_sound.wav");
                click_sound.Play();
            }
            base.WndProc(ref m);
        }

        System.Windows.Media.MediaPlayer sp = new System.Windows.Media.MediaPlayer();

    public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, 0);
            if (File.Exists("save.txt")) pictureBox3.Image = (SimulationSimulator.Properties.Resources.cont);
            sp.Open(new System.Uri("SS_theme.wav", System.UriKind.Relative));
            sp.MediaEnded += delegate { sp.Position = ts; };
            sp.Play();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Gamemode.sav))
            {
                File.Delete(Gamemode.sav);
            }
            System.Threading.Thread.Sleep(500);
            Gameplay openForm = new Gameplay();
            openForm.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (File.Exists(Gamemode.sav))
            {
                Gameplay openForm = new Gameplay();
                openForm.Show();
                this.Hide();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What do you want to change!?\nTHE GAME IS PERFECT!!!");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer click_sound = new System.Media.SoundPlayer("click_sound.wav");
            click_sound.PlaySync();
            Close();
        }


        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = (SimulationSimulator.Properties.Resources.ng_click);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = (SimulationSimulator.Properties.Resources.ng);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = (SimulationSimulator.Properties.Resources.ng_hov);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = (SimulationSimulator.Properties.Resources.ng_click);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Image = (SimulationSimulator.Properties.Resources.opt_click);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = (SimulationSimulator.Properties.Resources.opt);
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = (SimulationSimulator.Properties.Resources.opt_hov);
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = (SimulationSimulator.Properties.Resources.opt_click);
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.Image = (SimulationSimulator.Properties.Resources.ex_click);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = (SimulationSimulator.Properties.Resources.ex);
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox5.Image = (SimulationSimulator.Properties.Resources.ex_hov);
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox5.Image = (SimulationSimulator.Properties.Resources.ex_click);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            if (File.Exists("save.txt")) pictureBox3.Image = (SimulationSimulator.Properties.Resources.cont_click);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (File.Exists("save.txt"))  pictureBox3.Image = (SimulationSimulator.Properties.Resources.cont);
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (File.Exists("save.txt"))  pictureBox3.Image = (SimulationSimulator.Properties.Resources.cont_hov);
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (File.Exists("save.txt"))  pictureBox3.Image = (SimulationSimulator.Properties.Resources.cont_click);
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {

        }

        private void MainMenu_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("romzes, lol");
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = (SimulationSimulator.Properties.Resources.cred_click);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = (SimulationSimulator.Properties.Resources.cred);
        }

    }
}
