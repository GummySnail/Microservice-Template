namespace Area.Template.Domain.Templates;

public interface ITemplateRepository
{
    Task<IReadOnlyList<TemplateModel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TemplateModel?> GetByIdAsync(TemplateId id, CancellationToken cancellationToken = default);

    Task<TemplateModel?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    void Add(TemplateModel model);

    void Update(TemplateModel model);

    void Remove(TemplateModel model);
}