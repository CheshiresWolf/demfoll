using System.IO;
using System;
using UnityEngine;

namespace Utils {

    public class FileWorker {

        public string readFile(string fileName) {
            Debug.Log(fileName);
            return File.ReadAllText(fileName);
        }

        public void writeFile(string filename, string data) {
            System.IO.File.WriteAllText(filename, data);
        }

        public void deleteFile(string filename) {
        	System.IO.File.Delete(filename);
        }

        public void createDirectory(string path) {
            Debug.Log(path);
            System.IO.Directory.CreateDirectory(path);
        }

        public void deleteDirectory(string path) {
            try {
                System.IO.Directory.Delete(path, true);
            }
            catch (Exception e) {
                Debug.Log("Something went wrong");
            }
        }

        public string getCurrentDirectory() {
            return System.IO.Directory.GetCurrentDirectory();
        }

    }
    
}