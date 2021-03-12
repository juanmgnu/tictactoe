using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public class JugadorComputadora : Jugador
    {
        public JugadorComputadora(string nombre) : base(nombre) { }

        //public void RealizarMovimiento()
        //{
        //    MainWindow mw = (MainWindow)Application.Current.Windows[0];
        //    mw.ButtonTopLeft.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //}

        //public void GetBotonApretadoPorElHumano()
        //{
        //    // puedo llenar mi propia matriz con ese boton apretado y ver mis jugadas a partir de ahi.
        //}

    }
}
