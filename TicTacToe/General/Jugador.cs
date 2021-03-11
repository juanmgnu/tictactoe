using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    public class Jugador : INotifyPropertyChanged
    {
        private string nombre;

        private int puntaje;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Puntaje
        {
            get { return puntaje; }
            set { puntaje = value; NotifyPropertyChanged("Puntaje"); }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
