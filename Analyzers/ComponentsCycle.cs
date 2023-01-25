using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Core;

namespace Analyzers
{
    public class ComponentsCycle
    {
        public List<Component> components {get; set;}

        public ComponentsCycle()
        {
            this.components = new List<Component>();
        }
        public ComponentsCycle(List<Component> components) { 
            this.components = components;
        }

        public bool isDuplicate(ComponentsCycle componentCycle)
        {
            List<Component> components = this.components.OrderBy(o => o.Name).ToList();
            List<Component> toCompareWith = componentCycle.components.OrderBy(o => o.Name).ToList();

            if (components.Count != componentCycle.components.Count) {
                return false;
            }

            for (int i = 0; i < components.Count; i++) {
                if (!components[i].isEqual(toCompareWith[i])) {
                    return false;
                }
            }

            return true;
        }
    }
}