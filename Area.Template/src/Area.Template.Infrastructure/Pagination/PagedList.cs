using Area.Template.Domain.Abstractions.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Area.Template.Infrastructure.Pagination;

public class PagedList<T> : IPagedList<T>
{
    private PagedList(List<T> items, int pageIndex, int pageSize, int totalCount)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Items { get; }

    public int PageIndex { get; }
    public int PageSize { get; }

    public int TotalCount { get; }

    public bool HasNextPage => PageIndex * PageSize < TotalCount;

    public bool HasPreviousPage => PageIndex > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize)
    {
        var totalCount = await query.CountAsync();

        if (pageIndex == 0 && pageSize == 0)
            return new(await query.ToListAsync(), pageIndex, pageSize, totalCount);
        
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, pageIndex, pageSize, totalCount);
    }
}
