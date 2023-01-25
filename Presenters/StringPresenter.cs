using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Core;

namespace Presenters
{
    public static class StringPresenter
    {
        public static string modelStructureToString(ModelStructure modelStructure)
        {
            StringBuilder output = new StringBuilder("----------------------\n\n");
            modelStructure.sortComponentsByInstability("descending");

            List<Component> components = modelStructure.getComponents();

            foreach(var component in components)
            {
                output.Append(componentToString(component));
                output.Append("\n\n----------------------\n\n");
            }

            output.Length -= 2;

            return output.ToString();
        }

        private static string componentToString(Component component)
        {
            StringBuilder componentString = new StringBuilder();
            componentString.AppendLine(
                "Name: "+ component.Name+
                $" (I {component.calculateInstability()})"
            );

            componentString.AppendLine("Dependents: ");
            foreach(Connection connection in component.Dependents)
            {
                componentString.AppendLine(
                    "\t"+"- "+connection.dependent.Name+
                    $" (I {connection.dependent.calculateInstability()})"+
                    $" v{connection.dependencyVersion}"
                );
            }

            componentString.AppendLine("Dependencies: ");

            foreach(Connection connection in component.Dependencies)
            {
                componentString.AppendLine(
                    "\t"+"- "+connection.dependency.Name+
                    $" (I {connection.dependency.calculateInstability()})"
                );
            }

            componentString.Length--;

            return componentString.ToString();
        }
    }
}