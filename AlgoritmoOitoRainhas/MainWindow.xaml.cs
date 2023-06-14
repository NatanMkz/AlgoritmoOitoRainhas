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
        public GeneticAlgorithm Genetic;
        public PlaceQueen placeQueen;
        int TamTabuleiro;
        int TamPopulacao;
        int index = 0;
        public MainWindow()
        {
            InitializeComponent();
            //placeQueen = new PlaceQueen();
            //GeneticAlgorithm gen = new GeneticAlgorithm(7, 10);
            //int index = gen.ResolveRainhas();


        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtTamTabuleiro.Text, out TamTabuleiro) && int.TryParse(txtTamPopulacao.Text, out TamPopulacao))
            {
                Genetic = new GeneticAlgorithm(TamTabuleiro, TamPopulacao);
                if (Genetic.TamTabuleiro >= 4)
                {
                    //placeQueen.SolveNQueens();
                    index = Genetic.ResolveRainhas();
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

            for (int i = 0; i < Genetic.TamTabuleiro; i++)
            {
                gridBoard.RowDefinitions.Add(new RowDefinition());
                gridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }
            List<Border> lista = new List<Border>();
            List<TextBlock> txtBlocks = new List<TextBlock>();
            int count = 0;
            for (int row = 0; row < Genetic.TamTabuleiro; row++)
            {
                for (int i = 0; i < TamTabuleiro; i++)
                {
                    count++;
                    var border = new Border();
                    border.SetValue(Grid.RowProperty, row);
                    border.SetValue(Grid.ColumnProperty, i);
                    var textBlock = new TextBlock();
                    textBlock.Text = (Genetic.populacao[index, i] == 1) ? "♛" : string.Empty;
                    textBlock.FontSize = 24;
                    string id = $"Id{count.ToString()}";
                    textBlock.Name = id;
                    txtBlocks.Add(textBlock);
                    border.Name = id;
                    //border.Child = textBlock;
                    lista.Add(border);
                    gridBoard.Children.Add(border);
                }
            }

            for (int i = 1; i < gridBoard.Children.Count; i++)
            {
                string id = $"Id{i.ToString()}";
                var borda = lista.Where(x => x.Name == id).FirstOrDefault();
                TextBlock textblock = txtBlocks.Where(x => x.Name == id).FirstOrDefault();
                if (borda != null && textblock != null)
                {
                    borda.Child = textblock;

                }

            }

            gridBoard.Visibility = Visibility.Visible;
        }
    }
}
