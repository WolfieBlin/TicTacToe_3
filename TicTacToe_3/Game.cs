using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe_3
{
    public class Game
    {
        private string[] _board;
        private string _playerTurn;
        private string _playerFirst;
        private readonly Random _random = new Random();
        private int _iter;
        private int _round;
        private const string AiPlayer = "O";
        private const string HuPlayer = "X";

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

        private MoveS Minimax(string[] reboard, string player, int random)
         {
             _iter++;
            // board looks like this [] {"0","1","2","3","4","5","6","7","8"}
            //player X == true and player O == false
            //X is human O is an AI
            //array of possible moves 

            var array = Avail(reboard);
            //check if current position of the board is winning
            if (WinningMinMax(reboard, HuPlayer)) {
                var pokusObject = new MoveS {Score = -10};
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
            //MoveS is an object with two parameters: index and score
            var moves = new List<MoveS>();
           
            for (var i = 0; i < array.Length; i++)
            {
                var move = new MoveS();
                move.Index = Convert.ToInt32(reboard[Convert.ToInt32(array[i])]);
                
                reboard[Convert.ToInt32(array[i])] = player;
               
                if (player == AiPlayer) {
                    //var result = new MoveS();
                   //recursive call for building the tree of possible moves
                     var result = Minimax(reboard, HuPlayer, 2);
                     move.Score = result.Score;
                } else {
                    //var result = new MoveS();
                    var result = Minimax(reboard, AiPlayer, 2);
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
            return (board[0] == player && board[1] == player && board[2] == player) ||
                   (board[3] == player && board[4] == player && board[5] == player) ||
                   (board[6] == player && board[7] == player && board[8] == player) ||
                   (board[0] == player && board[3] == player && board[6] == player) ||
                   (board[1] == player && board[4] == player && board[7] == player) ||
                   (board[2] == player && board[5] == player && board[8] == player) ||
                   (board[0] == player && board[4] == player && board[8] == player) ||
                   (board[2] == player && board[4] == player && board[6] == player);
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
             _board = new [] {"0","1","2","3","4","5","6","7","8"};
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
             return Minimax(_board, AiPlayer, 1).Index;
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