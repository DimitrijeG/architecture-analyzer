using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Core;
using Collectors;

namespace Readers
{
    public class UrlReader
    {
        private string[] urls;
        private string accessToken;

        public UrlReader() {
            urls = new string[]{};
            accessToken = "";
        }

        public UrlReader(string[] urls, string accessToken) {
            this.urls = urls;
            this.accessToken = accessToken;
        }

        public UrlReader(FileUrlCollector collector) {
            this.urls = collector.collectUrls();
            this.accessToken = "";
        }

        public UrlReader(RepositoryUrlCollector collector) {
            this.urls = collector.collectUrls();
            this.accessToken = collector.accessToken;
        }

        public string loadFile(string url)
        {
            if (url == "") {
                return "";
            }

            string content = "";

            try {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Headers.Add("PRIVATE-TOKEN", this.accessToken);


                string response = "";
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                JObject jsonResponse = JObject.Parse(response);

                foreach (JProperty item in jsonResponse.Children<JProperty>()) {
                    if (item.Name == "content") {
                        byte[] data = Convert.FromBase64String(item.Value.ToString());
                        content = Encoding.UTF8.GetString(data);
                    }
                }
            } catch {
                // throw e;
                // Console.WriteLine("Content empty");
                // todo
                return "";
            }

            if (content == "" || content[0] != '{') {
                return "";
            } else {
                return content;
            }
        }
        
        public string[] readJson()
        {
            List<string> jsons = new List<string>();

            foreach (string url in this.urls) {
                string fileContent = loadFile(url);
                if (fileContent != "") {
                    JObject json = JObject.Parse(fileContent);
                    jsons.Add(json.ToString());
                }
            }

            return jsons.ToArray();
        }
    }  
}