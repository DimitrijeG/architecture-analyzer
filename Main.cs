using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Core;
using Readers;
using Presenters;
using Analyzers;
using Collectors;
using Newtonsoft.Json.Linq;

namespace Program
{
    class Program
    {

        // public static void printRawList(List<ComponentRawInfo> rawComponentList)
        // {
        //     foreach (ComponentRawInfo component in rawComponentList) {
        //         Console.WriteLine("Name: "+component.Name);

        //         Console.WriteLine("Require: ");
        //         foreach (var item in component.Require) {
        //             Console.WriteLine("\t-"+item.Key);
        //         }
        //         Console.WriteLine(".\n.");
        //     }
        // }

        private static string rootPath = "/Users/dimitrijegasic/Desktop/Dependencies/Program/";

        static void Main(string[] args)
        {
            RepositoryUrlCollector collector = new RepositoryUrlCollector(
                "10730441", "wApnzFMJs1GDxPqwKV6m");

            // string[] output = collector.collectUrls();

            UrlReader reader = new UrlReader(collector);

            RawComponentListParser rawList = new RawComponentListParser(reader.readJson());

            foreach (var item in rawList.parseRawList()) {
                Console.WriteLine(item.Name);
            }

            // testin gminor cange

            Config config = new Config();
            config.AnalyseComponentRequireWithNamePrefix = "softwarehaus/tpp-";

            RawComponentListCleaner cleaner = new RawComponentListCleaner(rawList.parseRawList(), config);

            ModelStructureGenerator modstruct = new ModelStructureGenerator(cleaner.refine());

            ModelStructure modelStructure = modstruct.construct();
        
            // string output = StringPresenter.modelStructureToString(modelStructure);

            TextFilePresenter textFilePresenter = new TextFilePresenter(modelStructure);

            textFilePresenter.presentModel(rootPath+"visualizer.txt");

            textFilePresenter.presentAnalysis(rootPath+"analysis.txt");

            CSVPresenter csvPresenter = new CSVPresenter(modelStructure);

            csvPresenter.presentModelLinks(rootPath+"connections.csv");

            csvPresenter.presentModelComponents(rootPath+"components.csv");
        }
    }
}
