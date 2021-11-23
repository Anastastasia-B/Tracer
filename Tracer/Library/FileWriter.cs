using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class FileWriter : IWriter
    {

        private string filePath;

        public FileWriter(string filePath = "file.txt")
        {
            this.filePath = filePath;
        }

        public void Write(string text)
        {
            System.IO.File.WriteAllText(filePath, text);
        }

        public void changeFileName(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
