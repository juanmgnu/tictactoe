using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicTacToe
{
    public partial class PlayerVsPlayerWindow : Window
    {
        public PlayerVsPlayerWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            if (boton.Name == "botonAceptar")
            {
                MainWindow mainWindow = new MainWindow(textBoxJugador1.Text, textBoxJugador2.Text);
                mainWindow.Show();
                Close();
            }
            else
            {
                WelcomeWindow welcomeWindow = new WelcomeWindow();
                welcomeWindow.Show();
                Close();
            }
        }
    }
}
