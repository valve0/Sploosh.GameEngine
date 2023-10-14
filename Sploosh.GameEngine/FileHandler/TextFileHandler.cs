using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Sploosh.GameEngine.FileHandler
{
    public class TextFileHandler : ITextFileHandler
    {
        private static readonly string _assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


        public void WriteToFile(string fileName, string text)
        {
            string path = $@"{_assemblyDirectory}/{fileName}";

            try
            {
                if (File.Exists(path))
                {
                    //Overwrites all text in file
                    File.WriteAllText(path, text);
                }
            }
            catch (FileNotFoundException fnfex)
            {
                Debug.WriteLine("The file couldn't be found!");
                Debug.WriteLine(fnfex.Message);
                Debug.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something went wrong while loading the file!");
                Debug.WriteLine(ex.Message);
            }

        }

        public string ReadFromFile(string fileName)
        {
            string path = $@"{_assemblyDirectory}/{fileName}";

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
                Debug.WriteLine("The file couldn't be found!");
                Debug.WriteLine(fnfex.Message);
                Debug.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something went wrong while loading the file!");
                Debug.WriteLine(ex.Message);
            }

            return stringBuilder.ToString().Replace("\r\n", string.Empty);
        }
    }
}
