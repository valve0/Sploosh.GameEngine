using Sploosh.GameEngine.FileHandler;
using Sploosh.GameEngine.Model;

namespace Sploosh.GameEngine
{

    public interface IGameState
    {
        public void SetupGame();
        public AttackResultCode MakeShot(int[] index);  
        public int GetHighScore();
        public void UpdateHighScore();

    }

    public class GameState : IGameState
    {
        private static readonly int _boardSize = 8;
        private ITextFileHandler _textFileHandler;

        public GameState()
        {
            _textFileHandler = new TextFileHandler();
        }

        public int SquidKillCount { get; private set; }
        public int MaxSquidCount { get; private set; }
        public int BoardSize { get; private set; }
        public int ShotCount { get; private set; }
        public int MaxShotCount { get; private set; }
        public int HighScore { get; private set; }
        public List<List<Square>>? Board { get; set; }

        public void SetupGame()
        {
            ShotCount = 0;
            MaxShotCount = 24;
            SquidKillCount = 0;
            MaxSquidCount = 3;
            BoardSize = 8;
            Board = GenerateNewBoard();
            HighScore = GetHighScore();
        }

        public List<List<Square>> GenerateNewBoard()
        {
            //Fill array of twoDSquares with new twoDSquares, setting them to default start value
            List<List<Square>> tempBoard = new List<List<Square>>(_boardSize);

            for (int row = 0; row < _boardSize; row++)
            {
                List<Square> newRow = new List<Square>(_boardSize);
                for (int col = 0; col < _boardSize; col++)
                {

                    //Fill list with default value of sea state
                    //int[] squarePosition = { row, col };

                    newRow.Add(new Square());
                }

                tempBoard.Add(newRow);
            }

            PlaceSquid(2, tempBoard);
            PlaceSquid(3, tempBoard);
            PlaceSquid(4, tempBoard);

            return tempBoard;
        }

        /// <summary>
        /// This method works out where to put each squid. It loops through
        /// the length of each squid putting the selected location for each part
        private static void PlaceSquid(int length, List<List<Square>> tempBoard)
        {
            int orientation;
            int row;
            int col;

            while (true)
            {
                //generate new orientation (0 = up down, 1 = left right)
                Random Random = new Random();
                orientation = Random.Next(2);

                row = Random.Next(_boardSize); //Starting row
                col = Random.Next(_boardSize); //Starting col

                if (Fits(tempBoard, length, orientation, row, col))
                    break;
            }

            //Create the squid
            Squid squid = new Squid(length);

            //Create refernce to squid object in relevant squares
            if (orientation == 0)
            {
                for (int i = 0; i < length; i++)
                    tempBoard[row][col + i].Squid = squid;
            }
            else
            {
                for (int i = 0; i < length; i++)
                    tempBoard[row + i][col].Squid = squid;
            }

        }

        /// <summary>
        /// Checks to see if the squid will fit on the board
        /// </summary>
        private static bool Fits(List<List<Square>> tempBoard, int length, int orientation, int row, int col)
        {


            for (int i = 0; i < length; ++i) //Loop through the length of the squid
            {
                if (orientation == 0)
                {
                    if (col + i >= _boardSize) //Is is out of bounds? 
                        return false;


                    if (tempBoard[row][col + i].Squid != null) //Are there any squid present at this location?             
                        return false;

                }
                else
                {

                    if (row + i >= _boardSize)
                        return false;

                    if (tempBoard[row + i][col].Squid != null)
                        return false;

                }
            }
            return true; // All squid checked 


        }

        /// <summary>
        /// Accpets an index of the square selected as a two integer array [Row,Col]
        /// </summary>
        /// <returns> Attack Result Code</returns>
        public AttackResultCode MakeShot(int[] index)
        {
            var attackResult = Board[index[0]][index[1]].AttackSquid();
            ShotCount++;

            if (attackResult == AttackResultCode.SquidDead)
            {
                SquidKillCount++;
                if (SquidKillCount == MaxSquidCount)
                {
                    UpdateHighScore();
                    return AttackResultCode.GameWin;
                }
                return attackResult;
            }
            else // Hit or miss
            {
                if (ShotCount == MaxShotCount)
                {
                    UpdateHighScore();
                    return AttackResultCode.GameLose;
                }
                else
                    return attackResult;
            }
        }

        /// <summary>
        /// Returns Highscore value stored in text file
        /// </summary>
        public int GetHighScore()
        {
            return int.Parse(_textFileHandler.ReadFromFile("HighScore.txt"));
        }

        /// <summary>
        /// Checks whether the final score of the user beats that of the stored Highscore
        /// If so, it updates the Highscore property and writes the new result to the Highscore file.
        /// </summary>
        public void UpdateHighScore()
        {

            if (ShotCount < HighScore)
            {
                _textFileHandler.WriteToFile("HighScore.txt", (ShotCount).ToString());
            }
        }
    }
}
