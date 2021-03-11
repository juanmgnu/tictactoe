using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Simbolo[,] tablero;

        private bool juegoTerminado;

        private bool esTurnoDelHumano;

        public MainWindow(string jugador1, string jugador2)
        {
            InitializeComponent();
            
            DataContext = new
            {
                jugador1 = new Jugador() { Nombre = jugador1 },
                jugador2 = new Jugador() { Nombre = jugador2 }
            };

            NuevaPartida();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Si terminó el juego, inicio una nueva partida y retorno.
            if (juegoTerminado)
            {
                NuevaPartida();
                return;
            }

            // Casteo el objeto que provocó el evento a Button.
            Button boton = (Button)sender;

            // Obtengo las coordenadas del botón.
            int fila = Grid.GetRow(boton);
            int columna = Grid.GetColumn(boton);

            // Si la posición que quiero llenar está ocupada, no hago nada.
            if (tablero[fila, columna] != Simbolo.Vacio)
            {
                return;
            }

            // Relleno la posición libre con el símbolo que corresponda.
            tablero[fila, columna] = esTurnoDelHumano ? Simbolo.X : Simbolo.O;

            // Si es turno del humano, el color es azul; si es turno de la máquina, rojo.
            boton.Foreground = esTurnoDelHumano ? Brushes.CornflowerBlue : Brushes.PaleVioletRed;

            // Actualizo la propiedad Content del botón.
            boton.Content = esTurnoDelHumano ? "X" : "O";

            // Actualizo el turno.
            esTurnoDelHumano = !esTurnoDelHumano;

            // Chequeo si hay ganador.
            ChequearGanador(fila, columna);

        }

        private void NuevaPartida()
        {
            // Creo la matriz de 3x3.
            tablero = new Simbolo[3, 3];

            // Inicializo la matriz con el valor Vacio en todas sus posiciones.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tablero[i, j] = Simbolo.Vacio;
                }
            }

            // El jugador humano comienza primero.
            esTurnoDelHumano = true;

            // Recién comienza el juego. 
            juegoTerminado = false;

            // Paso por todos los hijos del Grid. Como sé que son botones, puedo
            // castearlos y acceder a sus propiedades para resetearlas.
            foreach (Button hijo in Board.Children)
            {
                hijo.Content = String.Empty;
                hijo.Background = Brushes.GhostWhite;
            }
        }

        private void ChequearGanador(int fila, int columna)
        {
            // Chequeo que las filas sean iguales (pero que no sean iguales a Vacio, sino a X o a O).
            bool filaCompleta = tablero[fila, 0] != Simbolo.Vacio && (tablero[fila, 0] == tablero[fila, 1] && tablero[fila, 0] == tablero[fila, 2]);

            // Chequeo que las columnas sean iguales (pero que no sean iguales a Vacio, sino a X o a O).
            bool columnaCompleta = tablero[0, columna] != Simbolo.Vacio && (tablero[0, columna] == tablero[1, columna] && tablero[0, columna] == tablero[2, columna]);

            // Chequeo que las diagonales sean iguales (pero que no sean iguales a Vacio, sino a X o a O).
            bool diagonal1Completa = tablero[0, 0] != Simbolo.Vacio && (tablero[0, 0] == tablero[1, 1] && tablero[0, 0] == tablero[2, 2]);
            bool diagonal2Completa = tablero[0, 2] != Simbolo.Vacio && (tablero[0, 2] == tablero[1, 1] && tablero[0, 2] == tablero[2, 0]);

            // Chequeo empate.
            bool esEmpate = true;

            for (int f = 0; f < 3; f++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (tablero[f, c] == Simbolo.Vacio)
                    {
                        esEmpate = false;
                    }
                }
            }

            // Si hay un ganador o es empate, retorno el Simbolo correspondiente
            if (filaCompleta)
            {
                juegoTerminado = true;

                if (fila == 0)
                {
                    ButtonTopLeft.Background = ButtonTopCenter.Background = ButtonTopRight.Background = Brushes.PaleGreen;
                }
                else if (fila == 1)
                {
                    ButtonMiddleLeft.Background = ButtonMiddleCenter.Background = ButtonMiddleRight.Background = Brushes.PaleGreen;
                }
                else
                {
                    ButtonBottomLeft.Background = ButtonBottomCenter.Background = ButtonBottomRight.Background = Brushes.PaleGreen;
                }
            }
            else if (columnaCompleta)
            {
                juegoTerminado = true;

                if (columna == 0)
                {
                    ButtonTopLeft.Background = ButtonMiddleLeft.Background = ButtonBottomLeft.Background = Brushes.PaleGreen;
                }
                else if (columna == 1)
                {
                    ButtonTopCenter.Background = ButtonMiddleCenter.Background = ButtonBottomCenter.Background = Brushes.PaleGreen;
                }
                else
                {
                    ButtonTopRight.Background = ButtonMiddleRight.Background = ButtonBottomRight.Background = Brushes.PaleGreen;
                }
            }
            else if (diagonal1Completa)
            {
                juegoTerminado = true;

                ButtonTopLeft.Background = ButtonMiddleCenter.Background = ButtonBottomRight.Background = Brushes.PaleGreen;
            }
            else if (diagonal2Completa)
            {
                juegoTerminado = true;

                ButtonTopRight.Background = ButtonMiddleCenter.Background = ButtonBottomLeft.Background = Brushes.PaleGreen;
            }
            else if (esEmpate)
            {
                juegoTerminado = true;

                foreach (Button hijo in Board.Children)
                {
                    hijo.Background = Brushes.PaleTurquoise;
                }
            }
        }

    }
}
