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
               
                game.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}