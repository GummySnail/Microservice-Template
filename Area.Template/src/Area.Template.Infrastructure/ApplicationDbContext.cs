using Area.Template.Application.Abstractions.Clock;
using Area.Template.Application.Exceptions;
using Area.Template.Domain.Abstractions.Contracts;
using Area.Template.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace Area.Template.Infrastructure;

public sealed class ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider)
    : DbContext(options), IUnitOfWork
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TemplateModel> Templates { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
           
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }
}
