using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Game.App.Entities;

namespace Game.App
{
    /// <summary>
    /// Interaction logic for YouWin.xaml
    /// </summary>
    public partial class YouWin : Window
    {
        private static string Source = DirectoryProvider.CurrentDirectory();

        private string YouWinSound = Source + "win.wav";

        private bool MusicOn;
        private SoundPlayer currentSound;

        public YouWin(bool MusicOn)
        {
            InitializeComponent();

            PlaySound(YouWinSound);

            this.MusicOn = MusicOn;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            PlaySound(YouWinSound);
        }

        private void PlaySound(string soundDir, bool looping = false)
        {
            this.currentSound = new SoundPlayer(soundDir);

            if (MusicOn)
            {
                if (looping)
                {
                    this.currentSound.PlayLooping();
                }
                else
                {
                    this.currentSound.Play();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.MusicOn)
            {
                this.currentSound.Stop();
            }
            else
            {
                this.currentSound.Play();
            }
            this.MusicOn = !this.MusicOn;
        }
    }
}
