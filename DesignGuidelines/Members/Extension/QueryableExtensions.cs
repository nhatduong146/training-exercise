using System.Linq.Expressions;

namespace DesignGuidelines.Members.Extension
{
    // DO use extension methods to add utility or helper methods.
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, 
            int pageNumber, 
            int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> ConditionWhere<T>(this IQueryable<T> query, 
            bool condition, 
            Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }
    }

    public class Extension
    {
        public void Main()
        {
            // Usage of the extension methods
            var query = new[] { 1, 2, 3, 4, 5 }.AsQueryable();

            var pagedQuery = query.Paginate(1, 2); // Result: 1, 2
            var filteredQuery = query.ConditionWhere(true, x => x > 2); // Result: 3, 4, 5
        }
    }
}
