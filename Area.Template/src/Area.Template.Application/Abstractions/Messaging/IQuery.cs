using Area.Template.Domain.Abstractions;
using MediatR;

namespace Area.Template.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
