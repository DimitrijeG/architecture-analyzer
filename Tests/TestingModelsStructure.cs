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
    public class TestingModelsStructure : AbstractTesting
    {
        [Fact, Trait("Category", "Active")]
        public void allRootWithPrefix()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"GoodJson/allRootWithoutPrefix/C1.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C2.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C3.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C4.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C5.json"
            );

            string output = StringPresenter.modelStructureToString(modelStructure);

            TextFilePresenter textFilePresenter = new TextFilePresenter(modelStructure);
            textFilePresenter.presentModel(rootPath+"visualizer.txt");

            FileReader fileReader = new FileReader();
            string model = fileReader.LoadFile(rootPath+"GoodJson/GoodJsonTest.txt");


            Assert.Equal(model, output);
        }

        [Fact]
        public void allRootWithoutPrefix()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"GoodJson/allRootWithoutPrefix/C1.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C2.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C3.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C4.json",
                rootPath+"GoodJson/allRootWithoutPrefix/C5.json"
            );

            string output = StringPresenter.modelStructureToString(modelStructure);

            // TextFilePresenter.presentModel(modelStructure, rootPath+"visualizer.txt");

            FileReader fileReader = new FileReader();
            string model = fileReader.LoadFile(rootPath+"GoodJson/GoodJsonTest.txt");


            Assert.Equal(model, output);
        }
    }
}
