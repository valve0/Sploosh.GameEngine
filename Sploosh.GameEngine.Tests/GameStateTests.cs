﻿using Moq;
using Sploosh.GameEngine.FileHandler;
using Sploosh.GameEngine.Model;
using Xunit;

namespace Sploosh.GameEngine
{
    public class GameStateTests
    {
        private List<List<Square>> _board;
        private GameState _gameState;
        private int _boardSize = 8;

        public GameStateTests()
        {
            var textFileHandlerMock = new Mock<ITextFileHandler>();

            textFileHandlerMock.Setup(tf => tf.ReadFromFile("HighScore.txt"))
                .Returns("20");

            // Arrange
            _gameState = new GameState();
            _gameState.TextFileHandler = textFileHandlerMock.Object;

            //Create mock board of grid of 8 x 8 Squares
            _board = new List<List<Square>>(_boardSize);

            for (int row = 0; row < _boardSize; row++)
            {
                List<Square> newRow = new List<Square>(_boardSize);

                for (int col = 0; col < _boardSize; col++)
                    newRow.Add(new Square());

                _board.Add(newRow);
            }

            //Add board to mock gametstate
            _gameState.Board = _board;

        }

        [Fact]
        public void GenerateBoardWithThreeSquidsOfLengths2And3And4()
        {
            // Act
            var board = _gameState.GenerateNewBoard();

            //Assert
            //Make sure there are 9 squid parts on the board
            Assert.Equal(9, board.SelectMany(row => row).Count(square => square.Squid != null));

            //Make sure squids are placed horizontally and vertically

        }

        // Attack square - based on whats in the square "hit", or "squid killed" or "miss"



        [Fact]
        public void MakeShotShouldReturnHitifSquidPresentAndNotDead()
        {
            int[] targetSquare = new int[] { 0, 0 };
            Squid squid = new(2);

            //Place 2 long squid on board
            _board[0][0].Squid = squid;
            _board[0][1].Squid = squid;

            var attackResultCode = _gameState.MakeShot(targetSquare);

            Assert.Equal(AttackResultCode.Hit, attackResultCode);
        }

        [Fact]
        public void MakeShotShouldReturnSquidKilledifSquidPresentAndDead()
        {
            int[] targetSquare = new int[] { 0, 0 };
            Squid squid = new(2);
            squid.HitCounter = 1;

            //Place 2 long squid on board
            _board[0][0].Squid = squid;
            _board[0][1].Squid = squid;

            var attackResultCode = _gameState.MakeShot(targetSquare);

            Assert.Equal(AttackResultCode.SquidDead, attackResultCode);
        }

        [Fact]
        public void MakeShotShouldReturnMissifNoSquidPresent()
        {
            int[] targetSquare = new int[] { 0, 0 };

            var attackResultCode = _gameState.MakeShot(targetSquare);

            Assert.Equal(AttackResultCode.Miss, attackResultCode);
        }

        [Fact]
        public void ShouldReturnHighScore()
        {
            var highScore = _gameState.GetHighScore();

            Assert.Equal(20, highScore);
        }

        [Fact]
        public void ShouldSaveNewHighScoreToFile()
        {

        }
    }
}
