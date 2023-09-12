using SplooshGameEngine;
using SplooshGameEngine.Model;
using System.Reflection;
using System.Text;

namespace SplooshKaboom.GameEngine
{
    public class GameState
    {
        private static readonly int _boardSize = 8;

        public GameState()
        {
        }

        public int SquidsLeft { get; private set; }
        public int ShotCount { get; private set; }
        public int MaxShotCount { get; private set; }
        public int HighScore { get; private set; }
        public List<List<Square>>? Board { get; set; }

        public void SetupGame()
        {
            ShotCount = 0;
            MaxShotCount = 24;
            SquidsLeft = 3;
            HighScore = ReturnHighScore();
            Board = GenerateNewBoard();
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
                SquidsLeft--;
                if (SquidsLeft == 0)
                {
                    CheckForHighScore();
                    return AttackResultCode.GameWin;
                }
                return attackResult;
            }
            else // Hit or miss
            {
                if (ShotCount == MaxShotCount)
                {
                    CheckForHighScore();
                    return AttackResultCode.GameLose;
                }
                else
                    return attackResult;
            }
        }

        /// <summary>
        /// Checks whether the final score of the user beats that of the stored Highscore
        /// If so, it updates the Highscore property and writes the new result to the Highscore file.
        /// </summary>
        private void CheckForHighScore()
        {

            if (ShotCount < HighScore)
            {
                //update highscore property and textfile

                string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string path = $@"{assemblyDirectory}/HighScore.txt";
                HighScore = ShotCount;

                //Overwrites all text in file
                File.WriteAllText(path, HighScore.ToString());
            }
        }

            /// <summary>
            /// Reads the HighScore textfile and returns the stored value
            /// </summary>
            public int ReturnHighScore()
        {

            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = $@"{assemblyDirectory}/HighScore.txt";
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                if (File.Exists(path))
                {
                    foreach (string line in File.ReadAllLines(path))
                    {
                        if (line != null)
                            stringBuilder.AppendLine(line);
                    }
                }
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file couldn't be found!");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return int.Parse(stringBuilder.ToString().Replace("\r\n", string.Empty));
        }

    }
}
