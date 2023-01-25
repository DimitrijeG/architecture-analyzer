using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Core
{
    public class RawComponentListCleaner
    {
        public List<ComponentRawInfo> rawComponentList;
        private Config config;

        public RawComponentListCleaner(List<ComponentRawInfo> rawComponentList, Config config)
        {
            this.rawComponentList = rawComponentList;
            this.config = config;
        }

        public List<ComponentRawInfo> refine()
        {
            List<ComponentRawInfo> refinedList = new List<ComponentRawInfo>();

            foreach (ComponentRawInfo component in this.rawComponentList)
            {
                ComponentRawInfo newComponent = new ComponentRawInfo();

                newComponent.Name = component.Name;
                if (componentWithDesiredPrefix(newComponent.Name)) {
                    newComponent.Name = removeDesiredPrefixFromName(newComponent.Name);
                }
                
                
                List<JProperty> require = new List<JProperty>();

                foreach (var item in component.Require)
                {
                    if(componentWithDesiredPrefix(item.Name))
                    {
                        string requirementName = removeDesiredPrefixFromName(item.Name);
                        JToken requirementVersion = item.Value;
                        JProperty requirement = new JProperty(requirementName, requirementVersion);

                        require.Add(requirement);
                    }
                }

                newComponent.Require = require;

                refinedList.Add(newComponent);
            }

            return refinedList;
        }

        protected bool componentWithDesiredPrefix(string name)
        {
            string desiredPrefix = config.AnalyseComponentRequireWithNamePrefix;
            
            if (
                desiredPrefix != null
                && (
                    name.Length < desiredPrefix.Length
                    || name.Substring(0, desiredPrefix.Length) != desiredPrefix
                )
            ) {
                return false;
            }

            return true;
        }

        public string removeDesiredPrefixFromName(string name)
        {
            return name.Substring(config.AnalyseComponentRequireWithNamePrefix.Length);
        }
    }
}