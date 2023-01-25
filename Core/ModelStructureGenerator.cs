using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Core
{
    public class ModelStructureGenerator
    {
        private List<ComponentRawInfo> rawComponentList;

        private List<Component> components;

        public ModelStructureGenerator(List<ComponentRawInfo> componentList)
        {
            rawComponentList = new List<ComponentRawInfo>();

            foreach (var item in componentList) {
                if(item.Name != "") {
                    rawComponentList.Add(item);
                }
            }

            this.components = new List<Component>();
        }

        protected bool isExistingComponent(Component newComponent)
        {
            foreach (Component component in this.components) {
                if (component.isEqual(newComponent)) {
                    return true;
                }
            }
            return false;
        }

        protected void createAllCompoments()
        {
            foreach (ComponentRawInfo rowComponent in this.rawComponentList) {

                Component newComponent = new Component(rowComponent.Name);
                if (!this.isExistingComponent(newComponent)) {
                    this.components.Add(newComponent);
                }

                foreach (var item in rowComponent.Require) {
                    string name = item.Name;

                    newComponent = new Component(name);
                    if (!this.isExistingComponent(newComponent)) {
                        this.components.Add(newComponent);
                    }
                }
            }
        }

        public Component getComponent(string componentName)
        {
            foreach (Component component in this.components) {
                if (componentName == component.Name) {
                    return component;
                }
            }
            return null;
        }

        protected void createAllConnections()
        {
            foreach (ComponentRawInfo rowComponent in this.rawComponentList) {

                Component dependent = this.getComponent(rowComponent.Name);

                foreach (var dependencyItem in rowComponent.Require) {

                    string dependencyName = dependencyItem.Name;
                    string dependencyVersion = dependencyItem.Value.ToString();

                    Component dependency = this.getComponent(dependencyName);
                    Connection connection = new Connection(ref dependent, ref dependency, dependencyVersion);
                    dependent.addDependency(connection);
                    dependency.addDependent(ref connection);
                }
            }
        }


        public ModelStructure construct()
        {
            this.createAllCompoments();

            this.createAllConnections();

            return new ModelStructure(this.components);
        }
    }
}
