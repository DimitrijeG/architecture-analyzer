using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Core
{
    public class RawJsonParser
    {
        private JObject Json { get; set; }

        public RawJsonParser(string jsonString)
        {
            if (jsonString == "") {  
                Json = new JObject();
            } else {
                Json = JObject.Parse(jsonString);
            }
        }

        public ComponentRawInfo Parse()
        {
            string name = "";
            List<JProperty> require = new List<JProperty>();

            foreach(JProperty item in Json.Children<JProperty>()) {
                if((item.Name).Equals("name")) {
                    name = (item.Value).ToString();
                } else if ((item.Name).Equals("require")) {
                    foreach (JProperty requirement in item.Value.Children<JProperty>()) {
                        // Console.WriteLine("Name: "+requirement.Name+"   Version: "+requirement.Value);
                        require.Add(requirement);
                    }
                }
            }

            ComponentRawInfo json = new ComponentRawInfo();
            json.Name = name;
            json.Require = require;

            return json;
        }
    }
}