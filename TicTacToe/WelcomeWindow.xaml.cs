using System.Windows;
using System.Windows.Controls;

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
                PlayerVsComputerWindow window = new PlayerVsComputerWindow();
                window.Show();
                Close();
            }
            else
            {
                string mensaje = "¿Salir de la aplicación?";
                string titulo = "Atención";

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
