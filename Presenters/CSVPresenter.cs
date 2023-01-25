using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Analyzers;
using Core;

namespace Presenters
{
    public class CSVPresenter
    {
        private ModelStructure modelStructure;

        public CSVPresenter() {
            modelStructure = new ModelStructure();
        }

        public CSVPresenter(ModelStructure modelStructure) {
            this.modelStructure = modelStructure;
            this.modelStructure.sortComponentsByInstability("descending");
        }

        public void presentModelLinks(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Dependent,Dependency");

            foreach (Component dependent in this.modelStructure.getComponents()) {
                foreach (Connection dependency in dependent.Dependencies) {
                    sb.AppendLine(dependent.Name+" (I="+dependent.calculateInstability()+
                    "),"+dependency.dependency.Name+" (I="+dependency.dependency.calculateInstability()+")");
                }
            }
            // sb.Length--;

            using (StreamWriter sw = new StreamWriter(filePath)) {
                sw.Write(sb.ToString());
            }
        }

        public void presentModelComponents(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Component");

            foreach (Component component in this.modelStructure.getComponents()) {
                sb.AppendLine(component.Name+" (I="+component.calculateInstability()+"),");
            }
            sb.Length--;

            using (StreamWriter sw = new StreamWriter(filePath)) {
                sw.Write(sb.ToString());
            }
        }
    }
}