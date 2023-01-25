using System;
using System.Text;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Core;
using Readers;

namespace Collectors
{
    public class RepositoryUrlCollector
    {
        private string repositoryID;
        public string accessToken { get; }

        public RepositoryUrlCollector() {
            this.repositoryID = "";
        }

        public RepositoryUrlCollector(string repositoryUrl, string accessToken) {
            this.repositoryID = repositoryUrl;
            this.accessToken = accessToken;
        }

        private bool archived(JObject repo) {
            foreach (JProperty item in repo.Children<JProperty>()) {
                if (item.Name == "archived" && (item.Value.ToString()) == "true") {
                    return true;
                }
            }

            return false;
        }

        private List<string> adjustUrls(List<string> urls) {
            List<string> adjustedUrls = new List<string>();

            foreach (string url in urls) {
                adjustedUrls.Add(url.Replace(".git", "/-/raw/master/composer.json"));
            }
            
            return adjustedUrls;
        }

        public string[] collectUrls() {
            List<string> iDs = new List<string>();
            string link = @"https://gitlab.com/api/v4/groups/repoID/projects?include_subgroups=true&per_page=100";
            string output = "";
            // string output2 = "";

            using(WebClient client = new WebClient()) {
                client.Headers.Add("Private-Token", this.accessToken);

                string url1 = link.Replace("repoID", this.repositoryID);
                // string url2 = link.Replace("repoID", this.repositoryID)
                //                     .Replace("?include_subgroups=true", @"\?include_subgroups\=true");

                // Console.WriteLine("url1: "+url1);
                // Console.WriteLine("url2: "+url2);

                output = client.DownloadString(url1);
                // output2 = client.DownloadString(url2);
            }

            // return new string[]{output1, output2};

            JArray serialized1 = JArray.Parse(output);

            foreach (JObject repo in serialized1) {
                if (!archived(repo)) {
                    foreach (JProperty item in repo.Children<JProperty>()) {
                        if (item.Name == "id") {
                            iDs.Add(item.Value.ToString());
                        }
                    }
                }
            }

            // JArray serialized2 = JArray.Parse(output2);

            // foreach (JObject repo in serialized2) {
            //     if (!archived(repo)) {
            //         foreach (JProperty item in repo.Children<JProperty>()) {
            //             if (item.Name == "id") {
            //                 iDs.Add(item.Value.ToString());
            //             }
            //         }
            //     }
            // } 

            List<string> urls = new List<string>();

            foreach (string id in iDs) {
                urls.Add("https://gitlab.com/api/v4/projects/repoID/repository/files/composer.json?ref=master".Replace("repoID", id));
            }

            return adjustUrls(urls).ToArray();
        }
    }
}