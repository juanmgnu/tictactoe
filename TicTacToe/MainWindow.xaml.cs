using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private readonly Jugador jugador1;
        private readonly Jugador jugador2;

        private Simbolo[,] matriz;

        private bool juegoTerminado;

        private bool turnoJugador1;

        public MainWindow(string nombreJugador1, string nombreJugador2)
        {
            InitializeComponent();

            jugador1 = new Jugador(nombreJugador1);
            jugador2 = new Jugador(nombreJugador2);

            DataContext = new
            {
                jugador1,
                jugador2
            };

            NuevaPartida();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Casteo el objeto que provocó el evento a Button.
            Button boton = (Button)sender;

            // Si apreté el boton Reiniciar...
            if (boton.Name == "botonReiniciar")
            {
                // Reseteo los puntajes.
                jugador1.Puntaje = 0;
                jugador2.Puntaje = 0;

                // Inicio nueva partida.
                NuevaPartida();
            }
            // Si apreté el botón Volver...
            else if (boton.Name == "botonVolver")
            {
                string mensaje = "¿Volver al menu principal?";
                string titulo = "Atención";

                MessageBoxButton botones = MessageBoxButton.YesNo;
                MessageBoxImage icono = MessageBoxImage.Warning;

                MessageBoxResult resultado = MessageBox.Show(mensaje, titulo, botones, icono);

                if (resultado == MessageBoxResult.Yes)
                {
                    WelcomeWindow welcomeWindow = new WelcomeWindow();
                    welcomeWindow.Show();
                    Close();
                }
            }
            // Si apreté otro botón, entonces estoy jugando.
            else
            {
                // Primero me fijo si ya había terminado el juego. Si es así, inicio una
                // nueva partida y retorno. 
                if (juegoTerminado)
                {
                    NuevaPartida();
                    return;
                }

                // Obtengo las coordenadas del botón que apreté.
                int fila = Grid.GetRow(boton);
                int columna = Grid.GetColumn(boton);

                // Me fijo si el casillero que quiero ocupar está libre.
                if (CasilleroLibre(fila, columna))
                {
                    // Ocupo el lugar correspondiente.
                    OcuparCasillero(fila, columna);

                    // Actualizo tablero y turno.
                    ActualizarEstadoDelJuego(boton);
                }

                // Por último, chequeo si hay ganador.
                ChequearResultado(fila, columna);
            }
        }

        private void NuevaPartida()
        {
            // Creo la matriz de 3x3.
            matriz = new Simbolo[3, 3];

            // Inicializo la matriz con el valor Vacio en todas sus posiciones.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matriz[i, j] = Simbolo.Vacio;
                }
            }

            // El jugador 1 comienza primero.
            turnoJugador1 = true;

            // Recién comienza el juego. 
            juegoTerminado = false;

            // Paso por todos los hijos del Grid. Como sé que son botones, puedo
            // castearlos y acceder a sus propiedades para resetearlas.
            foreach (Button hijo in Board.Children)
            {
                hijo.Content = String.Empty;
                hijo.Background = Brushes.WhiteSmoke;
            }
        }

        private bool CasilleroLibre(int fila, int columna)
        {
            return matriz[fila, columna] == Simbolo.Vacio;
        }

        private void OcuparCasillero(int fila, int columna)
        {
            // Ocupo el casillero con el símbolo que corresponda.
            matriz[fila, columna] = turnoJugador1 ? Simbolo.X : Simbolo.O;
        }

        private void ActualizarEstadoDelJuego(Button boton)
        {
            // Si es turno del jugador 1, el color es azul; si es turno del jugador 2, rojo.
            boton.Foreground = turnoJugador1 ? Brushes.CornflowerBlue : Brushes.PaleVioletRed;

            // Muestro el símbolo en el casillero marcado.
            boton.Content = turnoJugador1 ? "X" : "O";

            // Actualizo el turno.
            turnoJugador1 = !turnoJugador1;
        }

        private void ChequearResultado(int fila, int columna)
        {
            // Chequeo ganador (se formó alguna línea - vertical, horizontal, diagonal - con el mismo valor).
            bool hayGanador = ChequearGanador(fila, columna, out string linea);

            // Chequeo empate (no hay ningún lugar vacío).
            bool esEmpate = ChequearEmpate();

            // Coloreo al ganador y le sumo un punto. Si es empate, coloreo todo el tablero y no sumo nada. 
            if (hayGanador)
            {
                juegoTerminado = true;
                Simbolo ganador = matriz[fila, columna];
                
                if (linea == "horizontal")
                {
                    ColorearFila(fila);
                    SumarPuntoAlGanador(ganador);
                }
                else if (linea == "vertical")
                {
                    ColorearColumna(columna);
                    SumarPuntoAlGanador(ganador);
                }
                else if (linea == "diagonal1")
                {
                    ColorearDiagonal(1);
                    SumarPuntoAlGanador(ganador);
                }
                else if (linea == "diagonal2")
                {
                    ColorearDiagonal(2);
                    SumarPuntoAlGanador(ganador);
                }
            }
            else if (esEmpate)
            {
                juegoTerminado = true;
                ColorearEmpate();
            }
        }

        private bool ChequearEmpate()
        {
            for (int f = 0; f < 3; f++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (matriz[f, c] == Simbolo.Vacio)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ChequearGanador(int fila, int columna, out string linea)
        {
            // Chequeo que las filas sean iguales (pero que no sean iguales a Vacio, sino a X o a O).
            bool filaCompleta = matriz[fila, 0] != Simbolo.Vacio && (matriz[fila, 0] == matriz[fila, 1] && matriz[fila, 0] == matriz[fila, 2]);

            // Chequeo que las columnas sean iguales (pero que no sean iguales a Vacio, sino a X o a O).
            bool columnaCompleta = matriz[0, columna] != Simbolo.Vacio && (matriz[0, columna] == matriz[1, columna] && matriz[0, columna] == matriz[2, columna]);

            // Chequeo que las diagonales sean iguales (pero que no sean iguales a Vacio, sino a X o a O).
            bool diagonal1Completa = matriz[0, 0] != Simbolo.Vacio && (matriz[0, 0] == matriz[1, 1] && matriz[0, 0] == matriz[2, 2]);
            bool diagonal2Completa = matriz[0, 2] != Simbolo.Vacio && (matriz[0, 2] == matriz[1, 1] && matriz[0, 2] == matriz[2, 0]);

            if (filaCompleta)
            {
                linea = "horizontal";
                return true;
            }
            else if (columnaCompleta)
            {
                linea = "vertical";
                return true;
            }
            else if (diagonal1Completa)
            {
                linea = "diagonal1";
                return true;
            }
            else if (diagonal2Completa)
            {
                linea = "diagonal2";
                return true;
            }

            linea = string.Empty;
            return false;
        }

        private void SumarPuntoAlGanador(Simbolo simbolo)
        {
            if (simbolo == Simbolo.X)
            {
                jugador1.Puntaje++;
            }
            else if (simbolo == Simbolo.O)
            {
                jugador2.Puntaje++;
            }
        }

        private void ColorearFila(int fila)
        {
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

        private void ColorearColumna(int columna)
        {
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

        private void ColorearDiagonal(int diagonal)
        {
            if (diagonal == 1)
            {
                ButtonTopLeft.Background = ButtonMiddleCenter.Background = ButtonBottomRight.Background = Brushes.PaleGreen;
            }
            else
            {
                ButtonTopRight.Background = ButtonMiddleCenter.Background = ButtonBottomLeft.Background = Brushes.PaleGreen;
            }
        }

        private void ColorearEmpate()
        {
            foreach (Button hijo in Board.Children)
            {
                hijo.Background = Brushes.PaleTurquoise;
            }
        }


    }
}
