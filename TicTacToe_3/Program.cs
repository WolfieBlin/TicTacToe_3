using System;

namespace TicTacToe_3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
               
                var game = new Game();
                var game4_4 = new Game4_4();
                Console.WriteLine("Vyberte si, jestli chcete hrát na " +
                                  "poli 3x3 (možnost 1), nebo na poli 4x4 s " +
                                  "jinými pravidly (možnost2)");
                var volba = Convert.ToInt32(Console.ReadLine());

                switch (volba)
                {
                    case 1: 
                        game.Start();
                        break;
                    case 2:
                        game4_4.Start();
                        break;
                    default:
                        Console.WriteLine("Špatná volba");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}