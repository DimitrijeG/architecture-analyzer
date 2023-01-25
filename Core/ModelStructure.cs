using System;
using System.Linq;
using System.Collections.Generic;


namespace Core
{
    public class ModelStructure{
        public List<Component> components;

        public ModelStructure(List<Component> comps = null)
        {
            components = new List<Component>();

            if (comps != null) {
                foreach(Component component in comps) {
                    addComponent(component);
                }
            }
            
        }

        public void addComponent(Component component)
        {
            // proveravanje duplikata

            components.Add(component);
        }

        public List<Component> getComponents() 
        {
            return this.components;
        }

        public void sortComponentsByInstability(string argument) 
        {
            if (argument == "ascending") {
                this.components = this.components.
                OrderBy(o=>o.calculateInstability()).ToList();

                foreach (Component component in this.components) {
                    component.Dependencies = component.Dependencies.
                    OrderBy(o=>o.dependency.calculateInstability()).ToList();

                    component.Dependents = component.Dependents.
                    OrderBy(o=>o.dependent.calculateInstability()).ToList();
                }
            } else if (argument == "descending") {
                this.components = this.components.
                OrderByDescending(o=>o.calculateInstability()).ToList();

                foreach (Component component in this.components) {
                    component.Dependencies = component.Dependencies.
                    OrderByDescending(o=>o.dependency.calculateInstability()).ToList();

                    component.Dependents = component.Dependents.
                    OrderByDescending(o=>o.dependent.calculateInstability()).ToList();
                }
            }
        }
    }
}