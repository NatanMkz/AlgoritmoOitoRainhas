using AlgoritmoOitoRainhas.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlgoritmoOitoRainhas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
                   
        public PlaceQueen placeQueen;
        public MainWindow()
        {
            InitializeComponent();
            //placeQueen = new PlaceQueen();

            GeneticAlgorithm gen = new GeneticAlgorithm(7, 10);
            int index = gen.ResolveRainhas();


        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtBoardSize.Text, out placeQueen.boardSize))
            {
                if (placeQueen.boardSize >= 4)
                {
                    placeQueen.SolveNQueens();
                    DrawBoard();
                }
                else
                {
                    MessageBox.Show("O tamanho do tabuleiro deve ser maior ou igual a 4.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Digite um número válido para o tamanho do tabuleiro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void DrawBoard()
        {
            gridBoard.Children.Clear();
            gridBoard.RowDefinitions.Clear();
            gridBoard.ColumnDefinitions.Clear();

            for (int i = 0; i < placeQueen.boardSize; i++)
            {
                gridBoard.RowDefinitions.Add(new RowDefinition());
                gridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < placeQueen.boardSize; row++)
            {
                for (int col = 0; col < placeQueen.boardSize; col++)
                {
                    var border = new Border();
                    border.SetValue(Grid.RowProperty, row);
                    border.SetValue(Grid.ColumnProperty, col);

                    var textBlock = new TextBlock();
                    textBlock.Text = (placeQueen.board[row, col] == 1) ? "♛" : string.Empty;
                    textBlock.FontSize = 24;

                    border.Child = textBlock;
                    gridBoard.Children.Add(border);
                }
            }

            gridBoard.Visibility = Visibility.Visible;
        }
    }
}
