using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe_3
{
    public class Game
    {
        public string[] _board;
        public string _playerTurn;
        private readonly Random _random = new Random();
        private int iter;
        private string aiPlayer = "O";
        private string huPlayer = "X";
        
        public Game()
        {
            
        }
        public struct MoveS
        {
            public int index;
            public int score;
            
        }
        public string[] Avail(string[] reboard)
        {
            var availMoves = reboard.Where(s => s !="X" && s != "O").ToArray();
            return availMoves;
        }
         public MoveS Minimax(string[] reboard, string player)
         {
             iter++;
            // board looks like this [] {"0","1","2","3","4","5","6","7","8"}
            //player X == true and player O == false
            //X is human O is an AI
            //array of possible moves 

            var array = Avail(reboard);
            //check if current position of the board is winning
            if (WinningMinMax(reboard, huPlayer)) {
                var pokusObject = new MoveS();
                pokusObject.score = -10;
                return pokusObject;
            } else if (WinningMinMax(reboard, aiPlayer)) {
                var pokusObject = new MoveS();
                pokusObject.score = 10;
                return pokusObject;
            // or it is a draw
            } else if (array.Length == 0) {
                var pokusObject = new MoveS();
                pokusObject.score = 0;
                return pokusObject;
            }
            //MoveS is an object with two parameters: index and score
            List<MoveS> moves = new List<MoveS>();
           
            for (var i = 0; i < array.Length; i++)
            {
                var move = new MoveS();
                move.index = Convert.ToInt32(reboard[Convert.ToInt32(array[i])]);
                
                reboard[Convert.ToInt32(array[i])] = player;
               
                if (player == aiPlayer) {
                    //var result = new MoveS();
                   //recursive call for building the tree of possible moves
                     var result = Minimax(reboard, huPlayer);
                     move.score = result.score;
                } else {
                    //var result = new MoveS();
                    var result = Minimax(reboard, aiPlayer);
                    move.score = result.score;
                }
                //resets the board value
                reboard[Convert.ToInt32(array[i])] = move.index.ToString();
                // adding the final object move to a List of moves with score for every move
                
                moves.Add(move);
            }
         
            //finding the best move of possible moves
            int bestMove = 0;
            if (player == aiPlayer) {
                var bestScore = -10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (moves[i].score > bestScore) {
                        bestScore = moves[i].score;
                        bestMove = i;
                    }
                }
            } else {
                var bestScore = 10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (moves[i].score < bestScore) {
                        bestScore = moves[i].score;
                        bestMove = i;
                    }
                }
            }
            //returning the best move's index for an Ai to play
            return moves[bestMove];
        }
         public bool WinningMinMax(string[] board, string player)
         {
            
            
             if ((board[0] == player && board[1] == player && board[2] == player) ||
                 (board[3] == player && board[4] == player && board[5] == player) ||
                 (board[6] == player && board[7] == player && board[8] == player) ||
                 (board[0] == player && board[3] == player && board[6] == player) ||
                 (board[1] == player && board[4] == player && board[7] == player) ||
                 (board[2] == player && board[5] == player && board[8] == player) ||
                 (board[0] == player && board[4] == player && board[8] == player) ||
                 (board[2] == player && board[4] == player && board[6] == player))
             {
                
                 return true;
             }
             return false;
         }
         public bool DrawMinMax(string[] board)
         {
             if (board[0] != "0" && board[1] != "1" && board[2] != "2" && board[3]
                 != "3" && board[4] != "4" && board[5] != "5" && board[6] != "6"
                 && board[7] != "7" && board[8] != "8" && !WinningMinMax(_board, _playerTurn))
             {
                 return true;
             }
             return false;
         }
         private void Move(int element, bool player)
         {
                 if (_board[element] != "X" && _board[element] != "O")
                 {
                     if (player)
                     {
                         _board[element] = "X";
                        
                         _playerTurn = aiPlayer;
                     }
                     else
                     {
                         _board[element] = "O";
                         
                         _playerTurn = huPlayer;
                     }
                     Print(_board);
                 }
         }
         private void Reset()
         {
             _board = new [] {"0","1","2","3","4","5","6","7","8"};
            
             if (_random.Next(0,2) == 1)
             {
                 _playerTurn = huPlayer;
             }
             else
             {
                 _playerTurn = aiPlayer;
             }
         }
         private void Print(string[] board)
         {
             Console.Clear();
            
             Console.WriteLine("Vítejte v TicTacToe, kde hrajete proti počítači.");
             Console.WriteLine("Hrajete za křížek: X a potítač za kolečko: O");
            
             Console.WriteLine("     |     |      ");
             Console.WriteLine("  {0}  |  {1}  |  {2}", board[0] , board[1], board[2]);
             Console.WriteLine("_____|_____|_____ ");
             Console.WriteLine("     |     |      ");
             Console.WriteLine("  {0}  |  {1}  |  {2}", board[3], board[4], board[5]);
             Console.WriteLine("_____|_____|_____ ");
             Console.WriteLine("     |     |      ");
             Console.WriteLine("  {0}  |  {1}  |  {2}", board[6], board[7], board[8]);
             Console.WriteLine("     |     |      ");
         }
         private int HracTurn()
         {
             var choose = Convert.ToInt32(Console.ReadLine());
             return choose;
         }
         private int AiTurn()
         {
             /*var avail = Avail(_board);
             return  Convert.ToInt32(avail[_random.Next(0, avail.Length)]);*/
             return Minimax(_board, aiPlayer).index;
         }
         public void Start()
         {
             while (true)
             {
                 Reset();
                 Print(_board);
                
                 while (!WinningMinMax(_board, aiPlayer) && !DrawMinMax(_board) && !WinningMinMax(_board, huPlayer))
                 {
                     if (_playerTurn == huPlayer)
                     {
                         Move(HracTurn(), true);
                     }
                     else
                     {
                         Move(AiTurn(), false);
                     }
                 }

                 if (WinningMinMax(_board, aiPlayer) || WinningMinMax(_board, huPlayer))
                 {
                     if (_playerTurn == aiPlayer)
                     {
                         Console.WriteLine("Vyhrál: X");
                     }
                     else
                     {
                         Console.WriteLine("Vyhrál: O");
                     }
                 }
                 else if (DrawMinMax(_board))
                 {
                     Console.WriteLine("It's a draw!");
                 }


                 Console.WriteLine("Chcete hrát znovu? y/n");
                 if (Console.ReadLine() != "y")
                 {
                     break;
                 }
             }
         }
    }
}