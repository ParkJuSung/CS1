using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    enum KakaoFriends
    {
        R,M,A,F,N,T,J,C
    }
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Console.WriteLine("Test Board");
            Console.WriteLine("Before update Board");
            board.ShowBoard();
            board.UpdateBoard();
            Console.WriteLine("After update Board");
            board.ShowBoard();
            Console.WriteLine();
            Console.WriteLine("Random data board");
            board.InData();
            Console.WriteLine("Before update Board");
            board.ShowBoard();
            board.UpdateBoard();
            Console.WriteLine("After update Board");
            board.ShowBoard();
        }

    }

    class Board
    {
        private const int r=6;
        private const int c=6;
        private bool brokenFlag = false;
        private bool switchingFlag = false;
        private string[,] board = new string[r,c]
    {
        {"T","T","T","A","N","T"},
        {"R","R","F","A","J","J"},
        {"R","R","R","F","J","J"},
        {"T","R","R","R","A","A"},
        {"T","T","M","M","M","F"},
        {"T","M","M","T","T","C"},
    };
        KakaoFriends kakao = new KakaoFriends();
        Random rnd = new Random();

        public void InData()
        {
            for (int i=0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    kakao = (KakaoFriends)rnd.Next(0, 7) ;
                    board[i, j] = kakao.ToString();
                }
            }
        }
        public void ShowBoard()
        {
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void UpdateBoard()
        {
            do
            {
                brokenFlag = false;
                switchingFlag = false;
                for (int i = 0; i < r - 1; i++)
                {
                    for (int j = 0; j < c - 1; j++)
                    {
                        if (IsSame(board[i, j], board[i + 1, j]) && IsSame(board[i, j], board[i, j + 1]) && IsSame(board[i, j], board[i + 1, j + 1]))
                        {
                            board[i, j] = board[i, j].ToLower();
                            board[i + 1, j] = board[i + 1, j].ToLower();
                            board[i, j + 1] = board[i, j + 1].ToLower();
                            board[i + 1, j + 1] = board[i + 1, j + 1].ToLower();
                            brokenFlag = true;
                        }
                    }
                }

                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        int temp = Convert.ToChar(board[i, j]);
                        if (temp >= 97 && temp <= 122)
                            board[i, j] = " ";
                        
                    }
                }

                for (int i = 0; i < r - 1; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        if (board[i + 1, j] == " " && board[i, j] != " ")
                        {
                            board[i + 1, j] = board[i, j];
                            board[i, j] = " ";
                            switchingFlag = true;
                        }
                    }
                }
            } while (brokenFlag || switchingFlag);
        }

        public bool IsSame(string board, string bodard2)
        {
            if (board.ToUpper() == bodard2.ToUpper() && (board != " ")) return true;
            return false;
        }
    }
}
