using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows;

namespace Fifteen
{
    public delegate void DeligateGame();
    public class myClass 
    {
        public event DeligateGame myEvent;
        public void InvokeEvent()
        {
            myEvent.Invoke();
        }
    }

    class Game
    {
        private Random random = new Random();
        private int[] array = new int[16];
        private int[,] field = new int[4, 4];
        private Point zero;
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
                Console.WriteLine("\n");
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
    class Write
    {
        private StreamWriter sw = new StreamWriter("game.txt", true);
        public void WriteToFile(int[,] arr)
        {
            sw.AutoFlush = true;
            Console.SetOut(sw);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Out.Write(arr[i, j] + " \t");
                }
                Console.Out.WriteLine("\n");
            }
            Console.WriteLine();

        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
           
            Game game = new Game();
            myClass mc = new myClass();
            game.ArrToField();
            mc.myEvent += new DeligateGame(game.ShowField);
            mc.InvokeEvent();
            do 
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
                mc.InvokeEvent();
            } while (true);
        }
    }
}