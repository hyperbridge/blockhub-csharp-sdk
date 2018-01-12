using Hyperbridge.Services.Abstract;
using StructureMap;

namespace Hyperbridge.StructureMap
{
    public class ApplicationDependencyRoot : IDependencyRoot
    {
        private Container container { get; }
        public ApplicationDependencyRoot(string profileDirectory)
        {
            var registry = new ApplicationRegistry(profileDirectory);
            container = new Container(ce =>
            {
                ce.AddRegistry(registry);
            });
        }

        public T Resolve<T>()
        {
            return container.GetInstance<T>();
        }
    }
}
