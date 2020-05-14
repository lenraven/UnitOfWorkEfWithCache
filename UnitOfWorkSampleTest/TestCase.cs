using System;

using Microsoft.Extensions.DependencyInjection;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace UnitOfWorkSampleTest
{
    public class TestCase : IDisposable
    {
        private readonly Container _container;

        public TestCase(string name, Action<Container> configureContainer)
        {
            if (configureContainer == null) throw new ArgumentNullException(nameof(configureContainer));

            Name = name ?? throw new ArgumentNullException(nameof(name));
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            configureContainer(_container);
        }
        
        public string Name { get; }

        public IDisposable BeginScope()
        {
            return AsyncScopedLifestyle.BeginScope(_container);
        }

        public TService GetService<TService>()
        {
            return _container.GetService<TService>();
        }

        public void Verify()
        {
            _container.Verify();
        }

        public override string ToString()
        {
            return Name;
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}