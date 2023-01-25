using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Core
{
    public class RawComponentListParser
    {
        private string[] rawJsonList;

        public RawComponentListParser(string[] rawJsonList) {
            this.rawJsonList = rawJsonList;
        }

        public List<ComponentRawInfo> parseRawList() {
            List<ComponentRawInfo> rawComponentList = new List<ComponentRawInfo>();
            foreach (string json in this.rawJsonList) {
                rawComponentList.Add((new RawJsonParser(json)).Parse());
            }

            return rawComponentList;
        }
    }
}