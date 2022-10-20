using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows;

namespace Fifteen
{
    class Game
    {
        public Random random = new Random();
        public int[] array = new int[16];
        public int[,] field = new int[4, 4];
        Point zero;
        private int[] FillArr()
        {
            array = Enumerable.Range(0, 16).ToArray().OrderBy(i => random.Next()).ToArray();
            return array;
        }
        public int[,] ArrToField()
        {
            array = FillArr();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    field[i, j] = array[count];
                    if (field[i, j] == 0)
                    {
                        zero = new Point(i, j);
                    }
                    count++;
                }
            }
            return field;
        }
        public void ShowField()
        {
            Console.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(field[i, j] + " \t");
                }
                Console.WriteLine();
            }
        }
        public void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public void Right()
        {
            if (zero.Y < field.GetLength(1) - 1)
            {
                Swap(ref field[(int)zero.X, (int)zero.Y], ref field[(int)zero.X, (int)zero.Y + 1]);
                zero.Y++;
            }
        }
        public void Left()
        {
            if (zero.Y > 0)
            {
                Swap(ref field[(int)zero.X, (int)zero.Y], ref field[(int)zero.X, (int)zero.Y - 1]);
                zero.Y--;
            }
        }
        public void Up()
        {
            if (zero.X > 0)
            {
                Swap(ref field[(int)zero.X - 1, (int)zero.Y], ref field[(int)zero.X, (int)zero.Y]);
                zero.X--;
            }
        }
        public void Down()
        {
            if (zero.X < field.GetLength(0) - 1)
            {
                Swap(ref field[(int)zero.X + 1, (int)zero.Y], ref field[(int)zero.X, (int)zero.Y]);
                zero.X++;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.ArrToField();
            game.ShowField();
            while (true)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        game.Up();
                        break;
                    case ConsoleKey.DownArrow:
                        game.Down();
                        break;
                    case ConsoleKey.LeftArrow:
                        game.Left();
                        break;
                    case ConsoleKey.RightArrow:
                        game.Right();
                        break;
                }
                game.ShowField();
            }
        }
    }
}