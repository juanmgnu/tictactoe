using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public abstract class Jugador : INotifyPropertyChanged
    {
        private string nombre;
        private int puntaje;

        public event PropertyChangedEventHandler PropertyChanged;

        public Jugador(string nombre)
        {
            this.nombre = nombre;
            puntaje = 0;
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Puntaje
        {
            get { return puntaje; }
            set
            {
                puntaje = value;
                NotifyPropertyChanged("Puntaje");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
