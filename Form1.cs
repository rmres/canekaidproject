using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
using NAudio.Wave;
using Canekaid.Properties;
using NAudio.CoreAudioApi;
using System.IO;
using System.Threading;

namespace Canekaid
{
    public partial class Form1 : Form
    {


        public static Form1 form1;
        static string path = Application.StartupPath;

        WaveOut waveout = new WaveOut(WaveCallbackInfo.FunctionCallback());
        public static FileStream jacobina = File.OpenRead($@"{path}\Musicas\A JACOBINA STATE OF MIND.mp3");
        static Mp3FileReader jacobinamp3wave = new Mp3FileReader(jacobina);
        static WaveStream jwavStream = WaveFormatConversionStream.CreatePcmStream(jacobinamp3wave);
        static BlockAlignReductionStream jacobbaStream = new BlockAlignReductionStream(jwavStream);


        public static FileStream cloreto = File.OpenRead($@"{path}\Musicas\cloreto-de-saudade.mp3");
        static Mp3FileReader cloretomp3wave = new Mp3FileReader(cloreto);
        static WaveStream cwavStream = WaveFormatConversionStream.CreatePcmStream(cloretomp3wave);
        static BlockAlignReductionStream cloretobaStream = new BlockAlignReductionStream(cwavStream);

        public Form1()
        {
            InitializeComponent();
            subMenuPanel.Visible = false;
            form1 = this;
            this.TopMost = false;
        }

        // Makes the upper pannel work as a border where you can drag the window through the screen
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Size = this.MinimumSize;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Size = this.MinimumSize;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // End of drag window


        // Controls from the upper pannel (close button and minimize button)
        private void button1_Click(object sender, EventArgs e)
        {
            waveout.Stop();
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        // End of upper panel controls


        // Toggle the visible property of submenu from the button "CANEKAID"
        public void ToggleSubMenu()
        {
            if (subMenuPanel.Visible == true)
            {
                subMenuPanel.Visible = false;
            }
            else
            {
                subMenuPanel.Visible = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ToggleSubMenu();
        }
        // End of toggle submenu


        // Method to call a child form within the "panelChildF" panel and its callings
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildF.Controls.Add(childForm);
            panelChildF.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new F_Membros());
        }
        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new F_Musicas());
        }
        // End of method to call child forms

        private void btn_play_Click(object sender, EventArgs e)
        {
            if (waveout.PlaybackState == PlaybackState.Stopped)
            {
                if (label_musica.Text == "CLORETO DE SAUDADE")
                {
                    waveout.Init(cloretobaStream);
                    waveout.Play();
                    btn_play.BackgroundImage = Resources.pausebutttt;
                }
                else if (label_musica.Text == "A JACOBINA STATE OF MIND")
                {
                    waveout.Init(jacobbaStream);
                    waveout.Play();
                    btn_play.BackgroundImage = Resources.pausebutttt;
                }
            }
            else if (waveout.PlaybackState == PlaybackState.Paused)
            {
                waveout.Resume();
                btn_play.BackgroundImage = Resources.pausebutttt;
            }
            else if (waveout.PlaybackState == PlaybackState.Playing)
            {
                waveout.Pause();
                btn_play.BackgroundImage = Resources.naoaguentomais;
            }
        }

        private void label_musica_TextChanged(object sender, EventArgs e)
        {
            waveout.Stop();
            jwavStream.Position = 0;
            cwavStream.Position = 0;
            btn_play.BackgroundImage = Resources.naoaguentomais;
        }

        // Buttons to oppen soundcloud/youtube link and change images when mouse is over
        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://soundcloud.com/canekaid-official");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCZs3pnudGFIvJp5o5NEmmIQ");
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            button7.BackgroundImage = Resources.ckscbtover;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.BackgroundImage = Resources.ckscbt;
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.BackgroundImage = Resources.ckytbtover;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackgroundImage = Resources.ckytbt;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (this.Bounds != Screen.FromHandle(this.Handle).WorkingArea) {
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;  
                this.Bounds = Screen.FromHandle(this.Handle).WorkingArea;
            }
            else
            {
                this.Size = this.MinimumSize;
                this.CenterToScreen();
            }
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Bounds != Screen.FromHandle(this.Handle).WorkingArea)
            {
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this.Bounds = Screen.FromHandle(this.Handle).WorkingArea;
            }
            else
            {
                this.Size = this.MinimumSize;
                this.CenterToScreen();
            }
        }
        // End of soundcloud/youtube buttons
    }
}
