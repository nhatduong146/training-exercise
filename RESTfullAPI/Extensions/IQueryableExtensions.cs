using Mapster;
using Microsoft.EntityFrameworkCore;
using RESTfullAPI.Domain.Entities;
using RESTfullAPI.Infrastructure.Helpers;
using RESTfullAPI.ViewModels.Pagination.Requests;
using RESTfullAPI.ViewModels.Pagination.Responses;
using System.Linq.Dynamic.Core;

namespace RESTfullAPI.Extensions;

public static class IQueryableExtensions
{
    public static async Task<PaginatedResponse<TResponse>> ToPaginatedResponseAsync<TEntity, TResponse>(
        this IQueryable<TEntity> query,
        PaginationFilterRequest filter,
        CancellationToken cancellationToken) where TEntity : BaseEntity
    {
        int skip = (filter.PageNumber - 1) * filter.PageSize;
        int totalCount = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            var sortOrder = (filter.SortType == SortType.Ascending) ? "ASC" : "DESC";
            query = query.OrderBy($"{filter.SortColumn} {sortOrder}");
        }

        var items = await query
            .Skip(skip)
            .Take(filter.PageSize)
            .ProjectToType<TResponse>() 
            .ToListAsync(cancellationToken);

        return new PaginatedResponse<TResponse>(totalCount, filter.PageNumber, filter.PageSize, items);
    }

    public static async Task<CursorPaginatedResponse<TResponse>> ToCursorPaginatedResponseAsync<TEntity, TResponse>(
        this IQueryable<TEntity> query,
        CursorPaginationFilterRequest filter,
        CancellationToken cancellationToken) where TEntity : BaseEntity
    {
        int totalCount = await query.CountAsync(cancellationToken);

        IQueryable<TEntity> filteredQuery = query.OrderBy(_ => _.CreatedOn).ThenBy(_ => _.Id);
        IQueryable<TEntity> paginatedQuery = filteredQuery;

        if (!string.IsNullOrEmpty(filter.Cursor))
        {
            var (createdOn, id) = CursorHelper.Decode(filter.Cursor);
            if( createdOn != null && id != null)
            {
                paginatedQuery = filteredQuery.Where(_ => _.CreatedOn > createdOn || (_.CreatedOn == createdOn && _.Id > id));
            }
        }

        var data = await paginatedQuery
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        string? nextCursor = null;
        if (data.Count == filter.PageSize)
        {
            var remainingItems = await paginatedQuery.Skip(filter.PageSize).AnyAsync(cancellationToken);
            if (remainingItems)
            {
                var lastItem = data.Last();
                nextCursor = CursorHelper.Encode(lastItem.CreatedOn, lastItem.Id);
            }
        }

        var responseData = data.Adapt<IEnumerable<TResponse>>();
        return new CursorPaginatedResponse<TResponse>(nextCursor, totalCount, filter.PageSize, responseData);
    }
}
