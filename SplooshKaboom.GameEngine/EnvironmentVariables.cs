using System.Reflection;

namespace SplooshGameEngine
{
    public class EnvironmentVariables
    {
        public readonly string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string? HighScorePath { get; set; }
        public Uri? SquareStartImagePath { get; set; }
        public Uri? SquareHitPath { get; set; }
        public Uri? SquareMissPath { get; set; }
        public Uri? BombAvailablePath { get; set; }
        public Uri? BombUnavailablePath { get; set; }
        public Uri? SquidAlivePath { get; set; }
        public Uri? SquidDeadPath { get; set; }
    }
}
