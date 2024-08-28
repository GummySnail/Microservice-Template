using Area.Template.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace Area.Template.Infrastructure.Repositories;

internal sealed class TemplateRepository(ApplicationDbContext dbContext)
    : Repository<TemplateModel, TemplateId>(dbContext), ITemplateRepository
{
    public async Task<TemplateModel?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext.Templates.FirstOrDefaultAsync(e => (string)e.Name == name, cancellationToken);
    }
}
