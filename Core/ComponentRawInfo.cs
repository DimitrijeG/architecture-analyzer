using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Core
{
    public class ComponentRawInfo : JObject {

        public string Name { get; set; }

        public List<JProperty> Require { get; set; }
    }
}