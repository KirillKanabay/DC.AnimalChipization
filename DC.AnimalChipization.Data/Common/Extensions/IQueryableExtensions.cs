using DC.AnimalChipization.Data.Common.Entities;
using DC.AnimalChipization.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Common.Extensions
{
    public static class IQueryableExtensions
    {
        private const int DefaultSkipItemsCount = 0;
        private const int DefaultPageSize = 10;

        public static Task<List<TEntity>> ToPagedList<TEntity>(this IQueryable<TEntity> query, Paging paging)
            where TEntity : EntityBase
        {
            if (paging == null)
            {
                return query.ToListAsync();
            }

            var from = paging.From < 0 ? DefaultSkipItemsCount : paging.From;
            var pageSize = paging.Size <= 0 ? DefaultPageSize : paging.Size;

            return query.Skip(from).Take(pageSize).ToListAsync();
        }
    }
}
