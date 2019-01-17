using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe_3
{
    class Game4_4
    {
        public string[] _board;
        public string _playerTurn;
        private string _playerFirst;
        private readonly Random _random = new Random();
        private long iter;
        private int round = 0;
        private string aiPlayer = "O";
        private string huPlayer = "X";
        private int x;
        
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
        
         public MoveS Minimax(string[] reboard, string player, int depth, int random)
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
            
            if (depth <= 0) {
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

                    if (player == aiPlayer)
                    {
                        //var result = new MoveS();
                        //recursive call for building the tree of possible moves
                        var result = Minimax(reboard, huPlayer, depth--, 1);
                        move.score = result.score;
                    }
                    else
                    {
                        //var result = new MoveS();
                        var result = Minimax(reboard, aiPlayer, depth--, 1);
                        move.score = result.score;
                    }

                    //resets the board value
                    reboard[Convert.ToInt32(array[i])] = move.index.ToString();
                    // adding the final object move to a List of moves with score for every move
                    moves.Add(move);
                }
            


            //finding the best move of possible moves
            int bestMove = 0;
            int bestMoveRandom = 0;
            if (player == aiPlayer) {
                var bestScore = -10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (moves[i].score > bestScore) {
                        bestScore = moves[i].score;
                        bestMove = i;
                        bestMoveRandom = bestScore;
                    }
                }
            } else {
                var bestScore = 10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (moves[i].score < bestScore) {
                        bestScore = moves[i].score;
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
            
            moves = moves.Where(s => s.score == bestMoveRandom).ToList();
            var tah = _random.Next(0, moves.Count);
            return moves[tah];
        }
         
         public bool WinningMinMax(string[] board, string player)
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
         
         public bool DrawMinMax(string[] board)
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
                 if (player == huPlayer)
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
             _board = new [] {"0","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15"};
             round = 0;
             
             if (_random.Next(0,2) == 1)
             {
                 _playerTurn = huPlayer;
                 _playerFirst = huPlayer;
             }
             else
             {
                 _playerTurn = aiPlayer;
                 _playerFirst = aiPlayer;
             }
         }
         private void Print(string[] board)
         {
             Console.Clear();
            
             Console.WriteLine("Vítejte v TicTacToe, kde hrajete proti počítači.");
             Console.WriteLine("Hrajete za křížek: X a potítač za kolečko: O");

             Console.WriteLine("round: " + round + " Calculation: " + iter);
            
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
             if (_playerFirst == aiPlayer)
             {
                 x = 9;
             }
             else
             {
                 x = 10;
             }
             if (round == 1)
             {
                 return Minimax(_board, aiPlayer, x, 1).index;
             }

             if (round == 2)
             {
                 return Minimax(_board, aiPlayer, x + 3, 1).index;
             }
             if (round == 3)
             {
                 return Minimax(_board, aiPlayer, x + 7, 1).index;
             }
             return Minimax(_board, aiPlayer, 100, 1).index;
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
                         if (_playerFirst == huPlayer)
                         {
                             round++;
                         }
                         Move(HracTurn(), huPlayer);
                     }
                     else
                     {
                         if (_playerFirst == aiPlayer)
                         {
                             round++;
                         }
                         iter = 0;
                         Move(AiTurn(), aiPlayer);
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