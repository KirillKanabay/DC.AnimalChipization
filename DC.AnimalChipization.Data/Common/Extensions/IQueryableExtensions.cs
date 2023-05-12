using System.Linq.Expressions;
using System.Reflection;
using DC.AnimalChipization.Data.Common.Entities;
using DC.AnimalChipization.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DC.AnimalChipization.Data.Common.Extensions
{
    public static class IQueryableExtensions
    {
        private const int DefaultSkipItemsCount = 0;
        private const int DefaultPageSize = 10;
        private const string DefaultSortingColumn = "Id";

        public static Task<List<TEntity>> ToPagedList<TEntity>(this IQueryable<TEntity> query, Paging paging, 
            Dictionary<string, Expression<Func<TEntity, object>>> sortingColumns = null)
            where TEntity : EntityBase
        {
            if (paging == null)
            {
                return query.ToListAsync();
            }

            if (sortingColumns != null)
            {
                if (!string.IsNullOrEmpty(paging.OrderBy) && sortingColumns.TryGetValue(paging.OrderBy, out var sortingColumn))
                {
                    query = query.OrderBy(sortingColumn);
                }
                else if (sortingColumns.TryGetValue(DefaultSortingColumn, out sortingColumn))
                {
                    query = query.OrderBy(sortingColumn);
                }
            }

            var from = paging.From < 0 ? DefaultSkipItemsCount : paging.From;
            var pageSize = paging.Size <= 0 ? DefaultPageSize : paging.Size;
            
            return query.Skip(from).Take(pageSize).ToListAsync();
        }
    }
}
