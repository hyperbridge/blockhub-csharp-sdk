using Hyperbridge.Services.Abstract;
using Hyperbridge.Wallet;
using System;
using System.Collections.Generic;

namespace Hyperbridge.Services
{
    public class DependencyRootResolver : IResolver
    {
        private Dictionary<ICoinCurrency, IDependencyRoot> Roots { get; }

        public DependencyRootResolver()
        {
            Roots = new Dictionary<ICoinCurrency, IDependencyRoot>();
        }

        public DependencyRootResolver(Dictionary<ICoinCurrency, IDependencyRoot> roots)
        {
            if (roots == null) throw new ArgumentNullException(nameof(roots));
            Roots = roots;
        }

        public void AddRoot<T>(T currency, IDependencyRoot root) where T : ICoinCurrency
        {
            Roots.Add(currency, root);
        }

        public T Resolve<T>(ICoinCurrency currency)
        {
            if (!Roots.ContainsKey(currency)) throw new KeyNotFoundException("Currency dependency root not found.");

            var root = Roots[currency];
            return root.Resolve<T>();
        }
    }
}
