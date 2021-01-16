using AutoMapper;
using DhubSolutions.Core.Domain.Adapters;
using System;
using System.Linq;

namespace DhubSolutions.Core.Application.Adapters
{
    public class AutoMapperTypeAdapterFactory : ITypeAdapterFactory
    {
        protected readonly MapperConfiguration configuration;
        private ITypeAdapter typeAdapter;
        public AutoMapperTypeAdapterFactory()
        {
            //scan all assemblies finding Automapper Profile
            var profiles = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.BaseType == typeof(Profile));

            configuration = new MapperConfiguration(cfg =>
            {
                foreach (var item in profiles.Where(item => !item.FullName.StartsWith("AutoMapper")))
                {
                    cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });

            //configuration.AssertConfigurationIsValid();
        }
        public ITypeAdapter Create()
        {
            return typeAdapter ?? (typeAdapter = new AutoMapperTypeAdapter(configuration));
        }
    }
}
