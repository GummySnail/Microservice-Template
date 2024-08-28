using Area.Template.Application.Abstractions.Clock;

namespace Area.Template.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
