using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Analyzers;
using Core;

namespace Presenters
{
    public class TextFilePresenter
    {
        private ModelStructure modelStructure;

        public TextFilePresenter() {
            modelStructure = new ModelStructure();
        }

        public TextFilePresenter(ModelStructure modelStructure) {
            this.modelStructure = modelStructure;
            this.modelStructure.sortComponentsByInstability("descending");
        }

        public void presentModel(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath)) {
                sw.Write(StringPresenter.modelStructureToString(this.modelStructure));
            }
        }

        public void presentAnalysis(string filePath)
        {
            int componentNumber = this.modelStructure.getComponents().Count;
            
            int relationNumber = 0;
            foreach (Component component in modelStructure.getComponents()) {
                relationNumber += component.Dependencies.Count();
            }

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);
            int cycleNumber = analyzer.getUniqueCycles().Count;

            List<string> content = new List<string>();
            content.Add($"Total number of components: {componentNumber}");
            content.Add($"Total number of relations: {relationNumber}");
            content.Add($"Number of detected cycles: {cycleNumber}");

            string analysis = String.Join("\n", content);

            using (StreamWriter sw = new StreamWriter(filePath)) {
                sw.Write(analysis);

                if (cycleNumber != 0) {
                    sw.WriteLine("\n\nCycles:");
                    sw.Write(analyzer.cycelsToString());
                } 
            }
        }
    }
}