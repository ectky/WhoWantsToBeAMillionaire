﻿using Game.App.Entities;
using Game.Data.Services;
using Game.Data.Services.Contracts;
using System;
using System.Collections.Generic;
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

namespace Game.App
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        private static string Source = DirectoryProvider.CurrentDirectory();

        private string mainthemeSound = Source + "main theme.wav";
        private readonly SoundPlayer sound;
        private readonly IPlayersService playersService;
        private readonly IGamesService gamesService;

        public StartUpWindow()
        {
            InitializeComponent();
            sound = new SoundPlayer
                (mainthemeSound);
            sound.Play();

            this.playersService = new PlayersService();
            this.gamesService = new GamesService(playersService);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(playersService, gamesService);
            main.Show();
            this.Close();
        }
    }
}
