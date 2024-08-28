using Area.Template.Domain.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Area.Template.Infrastructure.Configurations;

internal sealed class TemplateConfiguration : IEntityTypeConfiguration<TemplateModel>
{
    public void Configure(EntityTypeBuilder<TemplateModel> builder)
    {
        builder.ToTable("Templates");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
           .HasConversion(p => p.Value, value => new TemplateId(value));

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasConversion(p => p.Value, value => new Name(value));

        builder.Property(e => e.Description)
            .HasMaxLength(4000)
            .HasConversion(p => p.Value, value => new Description(value));

        builder.HasIndex(e => e.Name).IsUnique();
    }
}
