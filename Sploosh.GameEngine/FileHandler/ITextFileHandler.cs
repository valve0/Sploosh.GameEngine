namespace Sploosh.GameEngine.FileHandler
{
    public interface ITextFileHandler
    {
        string ReadFromFile(string fileName);

        void WriteToFile(string fileName, string text);
    }
}
