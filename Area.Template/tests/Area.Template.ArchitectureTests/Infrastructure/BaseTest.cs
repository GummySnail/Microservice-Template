using Area.Template.Api;
using Area.Template.Application.Abstractions.Messaging;
using Area.Template.Domain.Abstractions;
using Area.Template.Infrastructure;
using System.Reflection;

namespace Area.Template.ArchitectureTests.Infrastructure;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Entity<>).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
