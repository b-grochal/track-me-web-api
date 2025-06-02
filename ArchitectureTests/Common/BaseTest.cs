using Domain.Common;
using System.Reflection;

namespace ArchitectureTests.Common
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
        protected static readonly Assembly ApplicationAssembly = typeof(Application.DependencyInjection).Assembly;
        protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.DependencyInjection).Assembly;
        protected static readonly Assembly WebApiAssembly = typeof(WebApi.DependencyInjection).Assembly;
    }
}
