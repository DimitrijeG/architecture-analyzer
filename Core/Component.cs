using System;
using System.Text;
using System.Collections.Generic;

namespace Core
{
    public class Component
    {
        public string Name { get; set; }
        public List<Connection> Dependents {get; set;}
        public List<Connection> Dependencies {get; set;}

        public Component(string name) { 
            Name = name; 
            Dependents = new List<Connection>();
            Dependencies = new List<Connection>();
        }

        public void addDependent(ref Connection dependent)
        {
            this.Dependents.Add(dependent);
        }

        public void addDependency(Connection dependency)
        {
            this.Dependencies.Add(dependency);
        }

        public bool isEqual(Component component)
        {
             if (this.Name == component.Name) {
                 return true;
             }
             return false;
        }

        public double calculateInstability()
        {
            double result = Convert.ToDouble(Dependencies.Count) / (Dependencies.Count + Dependents.Count);
            return Math.Round(result, 2);
        }
    }
}