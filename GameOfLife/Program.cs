using System;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        private bool[,] board;

        public Program()
        {
            board = new bool[24, 36];

            var rand = new Random();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (rand.Next(100) > 75)
                        board[i, j] = true;
                }
            }

        }

        public void NextState()
        {
            bool[,] newBoard = new bool[board.GetLength(0), board.GetLength(1)];
            Array.Copy(board, newBoard, board.Length);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    newBoard[i, j] = GetNewState(i, j, board[i, j]);
                }
            }

            board = newBoard;
        }

        private bool GetNewState(int x, int y, bool currentState)
        {
            var count = GetNumNeighbors(x, y);

            if (currentState)
            {
                if (2 > count || count >= 4)
                    return false;
                else
                    // else condition implicitily satisfies a live cell count == 2 || count == 3
                    return true;
            }
            else
            {
                if (count == 3)
                    return true;
                else
                    return false;
            }
        }

        private int GetNumNeighbors(int x, int y)
        {
            int count = 0;

            if (x > 0)
            {
                if (board[x - 1, y])
                    count++;
            }
            if (x > 0 && y > 0)
            {
                if (board[x - 1, y - 1])
                    count++;
            }
            if (y > 0)
            {
                if (board[x, y - 1])
                    count++;
            }
            if (x < board.GetLength(0) - 1 && y > 0)
            {
                if (board[x + 1, y - 1])
                    count++;
            }
            if (x < board.GetLength(0) - 1)
            {
                if (board[x + 1, y])
                    count++;
            }
            if (x < board.GetLength(0) - 1 && y < board.GetLength(1) - 1)
            {
                if (board[x + 1, y + 1])
                    count++;
            }
            if (y < board.GetLength(1) - 1)
            {
                if (board[x, y + 1])
                    count++;
            }
            if (x > 0 && y < board.GetLength(1) - 1)
            {
                if (board[x - 1, y + 1])
                    count++;
            }

            return count;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j])
                        sb.Append(". ");
                    else
                        sb.Append("  ");
                }
                sb.Append("\n\r");
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            var game = new Program();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(game.ToString());
                game.NextState();
                Thread.Sleep(50);
            }
        }
    }
}
