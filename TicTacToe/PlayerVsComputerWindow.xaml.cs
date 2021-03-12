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
    public partial class PlayerVsComputerWindow : Window
    {
        public PlayerVsComputerWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            if (boton.Name == "botonAceptar")
            {
                string nombreJugador1 = textBoxJugador1.Text;
                string nombreJugador2 = "Computadora (fácil)";

                if (!String.IsNullOrEmpty(nombreJugador1))
                {
                    MainWindow mainWindow = new MainWindow(new JugadorHumano(nombreJugador1), new JugadorComputadora(nombreJugador2));
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
