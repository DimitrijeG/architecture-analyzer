using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Core;

namespace Analyzers
{
    public class AnalyzerOfCycles
    {
        private ModelStructure modelStructure;

        public AnalyzerOfCycles(ModelStructure modelStructure)
        {
            this.modelStructure = modelStructure;
        }

        private void printCycles(List<ComponentsCycle> allCycles)
        {
            Console.WriteLine("All cycles: ");
            foreach (var cycle in allCycles) {
                printPath(cycle);
            }
        }
        
        private void printPath(ComponentsCycle path)
        {
            foreach (var comp in path.components){
                Console.Write($"{comp.Name} ");
            }
            Console.WriteLine();
        }

        private string pathToString(Stack<Component> path) {
            Component[] unwrapper = path.ToArray();
            StringBuilder sb = new StringBuilder();

            foreach (Component item in unwrapper) {
                sb.Append(item.Name+" ");
            }

            return sb.ToString();
        }

        private ComponentsCycle makeComponentCycleFromTraversalPath(Stack<Component> traversalPath)
        {
            Stack<Component> reversedStack = new Stack<Component>(traversalPath);
            return new ComponentsCycle(reversedStack.ToList());
        }

        private void detectCycle(
            Stack<Component> traversalPath, 
            Component rootNode, 
            Component node, 
            ref List<ComponentsCycle> allCycles
        )
        {
            // Console.WriteLine("\n\nCurrent path: "+pathToString(traversalPath));
            if (node != null) {
                if (node.isEqual(rootNode)){
                    allCycles.Add(
                        this.makeComponentCycleFromTraversalPath(traversalPath)
                    );
                    return;
                }
            } else {
                node = rootNode;
            }

            traversalPath.Push(node);

            if (node.Dependencies.Count >= 1) {
                foreach(Connection dependency in node.Dependencies) {
                    Component nextNode = dependency.dependency;
                    if(nextNode.isEqual(rootNode) || !traversalPath.Contains(nextNode)) {
                        detectCycle(traversalPath, rootNode, nextNode, ref allCycles);
                    }
                }
            }
            // Console.WriteLine("<- going back");
            traversalPath.Pop();
        }

        private List<ComponentsCycle> getAllCycles()
        {
            // Console.WriteLine("\n---------------------------NEW TEST CASE---------------------------");
            List<ComponentsCycle> allCycles = new List<ComponentsCycle>();

            foreach (Component component in modelStructure.getComponents()) {
                // Console.WriteLine($"\nRoot: {component.Name}");
                detectCycle(new Stack<Component>(), component, null, ref allCycles);
            }

            // printCycles(allCycles);

            return allCycles;
        }

        public List<ComponentsCycle> getUniqueCycles()
        {
            List<ComponentsCycle> uniqueCycles = new List<ComponentsCycle>();
            List<ComponentsCycle> allCycles = getAllCycles();

            foreach (ComponentsCycle cycle in allCycles) {
                bool exists = false;
                foreach (ComponentsCycle uniqueCycle in uniqueCycles) { 
                    if (cycle.isDuplicate(uniqueCycle)) {
                        exists = true;
                        break;
                    }
                }
                if (!exists) {
                    uniqueCycles.Add(cycle);
                }
            }

            return uniqueCycles;
        }

        public string cycelsToString()
        {
            List<string> allCycles = new List<string>();

            foreach (var cycle in getUniqueCycles()) {
                StringBuilder cycleStrBuilder = new StringBuilder();
                string[] componentNames = cycle.components.Select(o => o.Name).ToArray();

                string stringCycle = String.Join(", ", componentNames);
                allCycles.Add(stringCycle);
            }

            return String.Join("\n", allCycles);
        }
    }
}