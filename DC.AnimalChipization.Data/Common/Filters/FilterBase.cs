using System.Linq.Expressions;
using DC.AnimalChipization.Data.Common.Entities;

namespace DC.AnimalChipization.Data.Common.Filters
{
    public abstract class FilterBase<TEntity> where TEntity : EntityBase
    {
        protected FilterBase() : this(new List<Expression<Func<TEntity, object>>>())
        {
        }

        protected FilterBase(List<Expression<Func<TEntity, object>>> includes)
        {
            _includes = includes ?? throw new ArgumentNullException(nameof(includes));
        }

        private readonly List<Expression<Func<TEntity, object>>> _includes;

        public IReadOnlyList<Expression<Func<TEntity, object>>> Includes => _includes;
        
        public FilterBase<TEntity> Include(Expression<Func<TEntity, object>> propertyFunc)
        {
            _includes.Add(propertyFunc);

            return this;
        }
    }
}
