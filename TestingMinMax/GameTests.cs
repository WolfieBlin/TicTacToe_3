using System;
using NUnit.Framework;
using TicTacToe_3;

namespace TestingMinMax
{
    [TestFixture]
    public class GameTests
    {
        Game game = new Game();
        
        [Test]
        public void WinningMinMax_GetBoardPlayer_TrueFalse()
        {
            
            string[] boardTest1 = new[] {"O", "X", "2", "3", "O", "5", "X", "7", "O"};
            string[] boardTest2 = new[] {"0", "O", "X", "O", "X", "X", "O", "7", "X"};
            string[] boardTest3 = new[] {"O", "X", "O", "O", "X", "X", "X", "O", "O"};

            bool result1_1 = game.WinningMinMax(boardTest1, true); // false
            bool result1_2 = game.WinningMinMax(boardTest1, false); // true
            
            bool result2_1 = game.WinningMinMax(boardTest2, true); // true
            bool result2_2 = game.WinningMinMax(boardTest2, false); // false
            
            bool result3_1 = game.WinningMinMax(boardTest3, true); // false
            bool result3_2 = game.WinningMinMax(boardTest3, false); // false
            Assert.True(!result1_1 && result1_2 && result2_1 && !result2_2 && !result3_1 && !result3_2);
        }

        [Test]
        public void MinMax_board_bestMove()
        {
            string[] boardTest1 = new[] {"O", "X", "2", "3", "O", "5", "X", "7", "8"}; // 8
            string[] boardTest2 = new[] {"0", "O", "2", "O", "X", "X", "O", "7", "X"}; // 0
            string[] boardTest3 = new[] {"O", "1", "X", "X", "4", "X", "6", "O", "O"}; // 0

      //      int result1 = game.Minimax(boardTest1, false);
            int result2 = game.Minimax(boardTest2, false);
            int result3 = game.Minimax(boardTest3, false);

            Assert.True(/*result1 == 8 &&*/ result2 == 0 && result3 == 0);
        }
    }
}