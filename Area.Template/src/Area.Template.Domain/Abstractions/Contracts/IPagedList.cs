namespace Area.Template.Domain.Abstractions.Contracts;

public interface IPagedList<T>
{
    List<T> Items { get; }

    int PageIndex { get; }

    int PageSize { get; }

    int TotalCount { get; }

    bool HasNextPage { get; }

    bool HasPreviousPage { get; }
}