using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Core;
using Presenters;
using Readers;
using Analyzers;
using Xunit;

namespace Tests
{
    public abstract class AbstractTesting
    {
        protected static string rootPath = "/Users/dimitrijegasic/Desktop/Dependencies/Tests/";

        protected ModelStructure GetModelStructure(params string[] filePaths)
        {
            FileReader fileReader = new FileReader(filePaths);
            string[] input = fileReader.readJson();
            
            List<ComponentRawInfo> rawComponentList = new List<ComponentRawInfo>();
            foreach (string json in input)
            {
                rawComponentList.Add((new RawJsonParser(json)).Parse());
            }

            Config config = new Config();
            config.AnalyseComponentRequireWithNamePrefix = "prefix/";
            RawComponentListCleaner cleaner = new RawComponentListCleaner(rawComponentList, config);

            ModelStructureGenerator modstruct = new ModelStructureGenerator(cleaner.refine());

            return modstruct.construct();
        }
    }
}
