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
        public YouWin()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            SoundPlayer sound1 = new SoundPlayer
                (DirectoryProvider.CurrentDirectory() + "win.wav");
            sound1.Play();
        }
    }
}
