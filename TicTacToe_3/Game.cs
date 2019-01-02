using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe_3
{
    class Game
    {
        public string[] _board;
        public bool _playerTurn;
        private readonly Random _random = new Random();
        
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
         public int Minimax(string[] reboard, bool player) 
         {
           
            // board looks like this [] {"0","1","2","3","4","5","6","7","8"}
            //player X == true and player O == false
            //X is human O is an AI
            //array of possible moves 

            var array = Avail(reboard);
            //check if current position of the board is winning
            if (WinningMinMax(reboard, true)) {
                return -10;
            } else if (WinningMinMax(reboard, false)) {
              return  10;
            // or it is a draw
            } else if (DrawMinMax(reboard)) {
               return 0;
            }
            //MoveS is an object with two parameters: index and score
            List<MoveS> moves = new List<MoveS>();
            
            for (var i = 0; i < array.Length; i++)
            {
                var move = new MoveS();
                move.index = Convert.ToInt32(reboard[Convert.ToInt32(array[i])]);
                
                if (player)
                {
                    reboard[Convert.ToInt32(array[i])] = "X";
                }
                else
                {
                    reboard[Convert.ToInt32(array[i])] = "O";
                }

                if (!player) {
                    var g = new MoveS();
                   //recursive call for building the tree of possible moves
                     g.score = Minimax(reboard, true);
                    
                     move.score = g.score;
                } else {
                    var g = new MoveS();
                   
                    g.score = Minimax(reboard, false);
                   
                    move.score = g.score;
                }
                //resets the board value
                reboard[Convert.ToInt32(array[i])] = move.index.ToString();
                // adding the final object move to a List of moves with score for every move
                
                moves.Add(move);
            }
         
            //finding the best move of possible moves
            int bestMove = 0;
            if (player == false) {
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
            return moves[bestMove].index;
        }
         public bool WinningMinMax(string[] board, bool player)
         {
             string playerXO;
           
             if (player)
             {
                 playerXO = "X";
             }
             else
             {
                 playerXO = "O";
             }
            
             if ((board[0] == playerXO && board[1] == playerXO && board[2] == playerXO) ||
                 (board[3] == playerXO && board[4] == playerXO && board[5] == playerXO) ||
                 (board[6] == playerXO && board[7] == playerXO && board[8] == playerXO) ||
                 (board[0] == playerXO && board[3] == playerXO && board[6] == playerXO) ||
                 (board[1] == playerXO && board[4] == playerXO && board[7] == playerXO) ||
                 (board[2] == playerXO && board[5] == playerXO && board[8] == playerXO) ||
                 (board[0] == playerXO && board[4] == playerXO && board[8] == playerXO) ||
                 (board[2] == playerXO && board[4] == playerXO && board[6] == playerXO))
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
                        
                         _playerTurn = false;
                     }
                     else
                     {
                         _board[element] = "O";
                         
                         _playerTurn = true;
                     }
                     Print(_board);
                 }
         }
         private void Reset()
         {
             _board = new [] {"0","1","2","3","4","5","6","7","8"};
            
             if (_random.Next(0,2) == 1)
             {
                 _playerTurn = true;
             }
             else
             {
                 _playerTurn = false;
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
             var avail = Avail(_board);
             return  Convert.ToInt32(avail[_random.Next(0, avail.Length)]);
         }
         public void Start()
         {
             while (true)
             {
                 Reset();
                 Print(_board);
                
                 while (!WinningMinMax(_board, !_playerTurn) && !DrawMinMax(_board))
                 {
                     if (_playerTurn)
                     {
                         Move(HracTurn(), true);
                     }
                     else
                     {
                         Move(AiTurn(), false);
                     }
                 }

                 if (WinningMinMax(_board, !_playerTurn))
                 {
                     if (!_playerTurn)
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