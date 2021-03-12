using System;
using System.Windows;
using System.Windows.Controls;

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
                string nombreJugador1 = textBoxJugador1.Text;
                string nombreJugador2 = textBoxJugador2.Text;

                if (!String.IsNullOrEmpty(nombreJugador1) && !String.IsNullOrEmpty(nombreJugador2))
                {
                    MainWindow mainWindow = new MainWindow(nombreJugador1, nombreJugador2);
                    mainWindow.Show();
                    Close();
                }
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
