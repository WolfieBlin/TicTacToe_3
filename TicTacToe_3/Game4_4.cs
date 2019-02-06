using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe_3
{
    class Game4_4
    {
        private string[] _board;
        private string _playerTurn;
        private string _playerFirst;
        private readonly Random _random = new Random();
        private long _iter;
        private int _round;
        private const string AiPlayer = "O";
        private const string HuPlayer = "X";
        private int _x;

        private struct MoveS
        {
            public int Index;
            public int Score;
            
        }

        private static string[] Avail(string[] reboard)
        {
            var availMoves = reboard.Where(s => s !="X" && s != "O").ToArray();
            return availMoves;
        }

        private MoveS Minimax(string[] reboard, string player, int depth, int random)
         {
             _iter++;
            // board looks like this [] {"0","1","2","3","4","5","6","7","8"}
            //player X == true and player O == false
            //X is human O is an AI
            //array of possible moves 

            var array = Avail(reboard);
            
            
            //check if current position of the board is winning
            if (WinningMinMax(reboard, HuPlayer)) {
                var pokusObject = new MoveS();
                pokusObject.Score = -10;
                return pokusObject;
            }

            if (WinningMinMax(reboard, AiPlayer)) {
                var pokusObject = new MoveS {Score = 10};
                return pokusObject;
                // or it is a draw
            }

            if (array.Length == 0) {
                var pokusObject = new MoveS {Score = 0};
                return pokusObject;
            }

            if (depth <= 0) {
                var pokusObject = new MoveS();
                pokusObject.Score = 0;
                return pokusObject;
            }
            //MoveS is an object with two parameters: index and score
            var moves = new List<MoveS>();
         
            for (var i = 0; i < array.Length; i++)
                {
                    var move = new MoveS();
                    move.Index = Convert.ToInt32(reboard[Convert.ToInt32(array[i])]);

                    reboard[Convert.ToInt32(array[i])] = player;

                    if (player == AiPlayer)
                    {
                        //var result = new MoveS();
                        //recursive call for building the tree of possible moves
                        var result = Minimax(reboard, HuPlayer, depth--, 1);
                        move.Score = result.Score;
                    }
                    else
                    {
                        //var result = new MoveS();
                        var result = Minimax(reboard, AiPlayer, depth--, 1);
                        move.Score = result.Score;
                    }

                    //resets the board value
                    reboard[Convert.ToInt32(array[i])] = move.Index.ToString();
                    // adding the final object move to a List of moves with score for every move
                    moves.Add(move);
                }
            


            //finding the best move of possible moves
            int bestMove = 0;
            int bestMoveRandom = 0;
            if (player == AiPlayer) {
                var bestScore = -10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (moves[i].Score > bestScore) {
                        bestScore = moves[i].Score;
                        bestMove = i;
                        bestMoveRandom = bestScore;
                    }
                }
            } else {
                var bestScore = 10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (moves[i].Score < bestScore) {
                        bestScore = moves[i].Score;
                        bestMove = i;
                        bestMoveRandom = bestScore;
                    }
                }
            }
            //returning the best move's index for an Ai to play
            if (random != 1)
            {
                return moves[bestMove];
                
            }
            
            moves = moves.Where(s => s.Score == bestMoveRandom).ToList();
            var tah = _random.Next(0, moves.Count);
            return moves[tah];
        }

        private bool WinningMinMax(string[] board, string player)
         {
             if (board[0] == player && board[1] == player && board[2] == player && board[3] == player ||
                 board[4] == player && board[5] == player && board[6] == player && board[7] == player ||
                 board[8] == player && board[9] == player && board[10] == player && board[11] == player ||
                 board[12] == player && board[13] == player && board[14] == player && board[15] == player)
             {
                 return true;
             }

             if (board[0] == player && board[4] == player && board[8] == player && board[12] == player ||
                 board[1] == player && board[5] == player && board[9] == player && board[13] == player ||
                 board[2] == player && board[6] == player && board[10] == player && board[14] == player ||
                 board[3] == player && board[7] == player && board[11] == player && board[15] == player)
             {
                 return true;
             }

             if (board[0] == player && board[5] == player && board[10] == player && board[15] == player ||
                 board[3] == player && board[6] == player && board[9] == player && board[12] == player)
             {
                 return true;
             }

             if (board[0] == player && board[1] == player && board[4] == player && board[5] == player ||
                 board[1] == player && board[2] == player && board[5] == player && board[6] == player ||
                 board[2] == player && board[3] == player && board[6] == player && board[7] == player ||
                 board[4] == player && board[5] == player && board[8] == player && board[9] == player ||
                 board[5] == player && board[6] == player && board[9] == player && board[10] == player ||
                 board[6] == player && board[7] == player && board[10] == player && board[11] == player ||
                 board[8] == player && board[9] == player && board[12] == player && board[13] == player ||
                 board[9] == player && board[10] == player && board[13] == player && board[14] == player ||
                 board[10] == player && board[11] == player && board[14] == player && board[15] == player)
             {
                 return true;
             }

             if ((board[0] == player && board[3] == player && board[12] == player && board[15] == player))
             {
                 return true;
             }
             return false;
         }

        private bool DrawMinMax(string[] board)
         {
             if (board[0] != "0" && board[1] != "1" && board[2] != "2" && board[3]
                 != "3" && board[4] != "4" && board[5] != "5" && board[6] != "6"
                 && board[7] != "7" && board[8]  != "8" && board[9]  != "9" && board[10]  != "10" 
                 && board[11]  != "11" && board[12]  != "12" && board[13]  != "13" && board[14]  != "14"
                 && board[15]  != "15"
                 && !WinningMinMax(_board, _playerTurn))
             {
                 return true;
             }
             return false;
         }
         
         private void Move(int element, string player)
         {
             if (_board[element] != "X" && _board[element] != "O")
             {
                 if (player == HuPlayer)
                 {
                     _board[element] = "X";
                        
                     _playerTurn = AiPlayer;
                 }
                 else
                 {
                     _board[element] = "O";
                         
                     _playerTurn = HuPlayer;
                 }
                 Print(_board);
             }
         }
         
          private void Reset()
         {
             _board = new [] {"0","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15"};
             _round = 0;
             
             if (_random.Next(0,2) == 1)
             {
                 _playerTurn = HuPlayer;
                 _playerFirst = HuPlayer;
             }
             else
             {
                 _playerTurn = AiPlayer;
                 _playerFirst = AiPlayer;
             }
         }
         private void Print(string[] board)
         {
             Console.Clear();
            
             Console.WriteLine("Vítejte v TicTacToe, kde hrajete proti počítači.");
             Console.WriteLine("Hrajete za křížek: X a potítač za kolečko: O");

             Console.WriteLine("round: " + _round + " Calculation: " + _iter);
            
             Console.WriteLine("     |     |     |      ");
             Console.WriteLine("  {0}  |  {1}  |  {2}  |  {3}  ", board[0] , board[1], board[2], board[3]);
             Console.WriteLine("_____|_____|_____|_____");
             Console.WriteLine("     |     |     |     ");
             Console.WriteLine("  {0}  |  {1}  |  {2}  |  {3}", board[4], board[5], board[6], board[7]);
             Console.WriteLine("_____|_____|_____|_____");
             Console.WriteLine("     |     |     |      ");
             Console.WriteLine("  {0}  |  {1}  |  {2} |  {3}", board[8], board[9], board[10], board[11]);
             Console.WriteLine("_____|_____|_____|_____");
             Console.WriteLine("     |     |     |      ");
             Console.WriteLine("  {0} |  {1} |  {2} |  {3}", board[12], board[13], board[14], board[15]);
             Console.WriteLine("     |     |     |      ");
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
             /*if (round == 1  || round == 2 || round == 3 || round == 4)
             {
                 var privniTah = _random.Next(0, Avail(_board).Length);

                 return Convert.ToInt32(Avail(_board)[privniTah]);
             }*/
             if (_playerFirst == AiPlayer)
             {
                 _x = 9;
             }
             else
             {
                 _x = 10;
             }
             if (_round == 1)
             {
                 return Minimax(_board, AiPlayer, _x, 1).Index;
             }

             if (_round == 2)
             {
                 return Minimax(_board, AiPlayer, _x + 3, 1).Index;
             }
             if (_round == 3)
             {
                 return Minimax(_board, AiPlayer, _x + 7, 1).Index;
             }
             return Minimax(_board, AiPlayer, 100, 1).Index;
         }
         public void Start()
         {
             while (true)
             {
                 Reset();
                 Print(_board);
                
                 while (!WinningMinMax(_board, AiPlayer) && !DrawMinMax(_board) && !WinningMinMax(_board, HuPlayer))
                 {
                     if (_playerTurn == HuPlayer)
                     {
                         if (_playerFirst == HuPlayer)
                         {
                             _round++;
                         }
                         Move(HracTurn(), HuPlayer);
                     }
                     else
                     {
                         if (_playerFirst == AiPlayer)
                         {
                             _round++;
                         }
                         _iter = 0;
                         Move(AiTurn(), AiPlayer);
                     }
                 }

                 if (WinningMinMax(_board, AiPlayer) || WinningMinMax(_board, HuPlayer))
                 {
                     if (_playerTurn == AiPlayer)
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