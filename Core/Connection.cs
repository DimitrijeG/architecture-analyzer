using System;


namespace Core
{
    public class Connection
    {
        public Component dependent;
        public Component dependency;
        
        public string dependencyVersion;

        public Connection(ref Component dependent, ref Component dependency, string dependencyVersion)
        {
            this.dependency = dependency;
            this.dependent = dependent;
            this.dependencyVersion = dependencyVersion;
        }
    }
}