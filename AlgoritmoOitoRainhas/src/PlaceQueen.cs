using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoOitoRainhas.src
{
    public class PlaceQueen
    {
        public int[,] board;
        public int boardSize;
        public void SolveNQueens()
        {
            board = new int[boardSize, boardSize];
            PlaceQueens(0);
        }

        private bool PlaceQueens(int col)
        {
            if (col >= boardSize)
            {
                return true; // Todas as rainhas foram colocadas com segurança
            }

            for (int row = 0; row < boardSize; row++)
            {
                if (IsSafe(row, col))
                {
                    board[row, col] = 1; // Coloca a rainha no tabuleiro

                    if (PlaceQueens(col + 1))
                    {
                        return true;
                    }

                    board[row, col] = 0; // Remove a rainha do tabuleiro (backtracking)
                }
            }

            return false; // Nenhuma posição segura encontrada nesta coluna
        }

        private bool IsSafe(int row, int col)
        {
            // Verifica se não há rainhas na mesma linha
            for (int i = 0; i < col; i++)
            {
                if (board[row, i] == 1)
                {
                    return false;
                }
            }

            // Verifica a diagonal superior esquerda
            for (int i = row, j = col; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == 1)
                {
                    return false;
                }
            }

            // Verifica a diagonal inferior esquerda
            for (int i = row, j = col; i < boardSize && j >= 0; i++, j--)
            {
                if (board[i, j] == 1)
                {
                    return false;
                }
            }

            return true; // Posição segura para colocar a rainha
        }
    }
}
