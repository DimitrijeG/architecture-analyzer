using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Core;
using Readers;

namespace Collectors
{
    public class FileUrlCollector
    {
        private string filePath;

        public FileUrlCollector() {
            this.filePath = "";
        }

        public FileUrlCollector(string filePath) {
            this.filePath = filePath;
        }

        public string[] collectUrls() {
            FileReader fileReader = new FileReader();

            string content = fileReader.LoadFile(filePath);
            return content.Split("\n").ToArray();;
        }
    }
}