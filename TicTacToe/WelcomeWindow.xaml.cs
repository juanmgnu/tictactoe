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
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            if (boton.Name == "botonJvJ")
            {
                PlayerVsPlayerWindow window = new PlayerVsPlayerWindow();
                window.Show();
                Close();
            }
            else if (boton.Name == "botonJvC")
            {
                
            }
            else
            {
                string mensaje = "¿Seguro que quiere salir?";
                string titulo = "Salir de la aplicación";

                MessageBoxButton botones = MessageBoxButton.YesNo;
                MessageBoxImage icono = MessageBoxImage.Warning;

                MessageBoxResult resultado = MessageBox.Show(mensaje, titulo, botones, icono);

                if (resultado == MessageBoxResult.Yes)
                {
                    Close();
                }
            }


        }
    }
}
