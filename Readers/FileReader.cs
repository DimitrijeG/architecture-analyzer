using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Core;

namespace Readers
{
    public class FileReader : Reader
    {
        private string[] filePaths;

        public FileReader() {
            this.filePaths = new string[]{};
        }

        public FileReader(params string[] filePaths) {
            this.filePaths = filePaths;
        }

        public string LoadFile(string filePath)
        {
            string content = "";

            using (StreamReader r = new StreamReader(filePath))
            {
                content = r.ReadToEnd();
            }

            return content;
        }

        public string[] readJson()
        {
            List<string> jsons = new List<string>();

            foreach (string filePath in this.filePaths) {
                string fileContent = this.LoadFile(filePath);
                JObject json = JObject.Parse(fileContent);

                jsons.Add(json.ToString());
            }

            return jsons.ToArray();
        }
    }  
}