using System.IO;

namespace Utils {

    public class FileWorker {

        public string readFile(string fileName) {
            return File.ReadAllText(fileName);
        }

        public void writeFile(string filename, string data) {
            System.IO.File.WriteAllText(filename, data);
        }

    }
    
}