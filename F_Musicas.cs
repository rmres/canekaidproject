using Canekaid.Properties;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canekaid
{
    public partial class F_Musicas : Form
    {
        public static F_Musicas musicform;
        public F_Musicas()
        {
            InitializeComponent();
            musicform = this;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linklabel_soundcloud_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://soundcloud.com/canekaid-official/a-jacobina-state-of-mind");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1.form1.label_musica.Text = label_jacob.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1.form1.label_musica.Text = label_cloreto.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=F-tCMS98-ac");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://soundcloud.com/canekaid-official/canekaid-cloreto-de-saudade");
        }
    }
}
