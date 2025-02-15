using DesignGuidelines.Exceptions;
using System.Collections;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace DesignGuidelines.Usage
{
    /*
     * DO prefer using collections over arrays in public APIs. 
     * Dynamic Sizing: Easy to add or remove products.
     * LINQ Support: Simplifies querying, sorting and filtering.
     */
    public class CollectionUsage
    {
        // ProductService.cs
        public List<Product> GetFeaturedProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 1200 },
                new Product { Id = 2, Name = "Smartphone", Price = 800 }
            };
        }

        // Filtering Example
        public List<Product> GetDiscountedProducts()
        {
            return GetFeaturedProducts()
                .Where(p => p.Price < 1000)
                .ToList();
        }

        // AVOID using weakly typed collection
        public ArrayList GetOrders()
        {
            var orders = new ArrayList();
            orders.Add(new Order { Id = 1, TotalPrice = 500000 });
            orders.Add("Invalid Data");  // No compile-time check
            return orders;
        }


        /*
         * AVOID using ArrayList, Collection, or List as a method parameter 
         * if the method does not add or remove elements.
         */
        public void ProcessOrders(IEnumerable<Order> orders) // Good practice
        {
            int count = orders.Count(); 
            if (count > 0)
            {
                Console.WriteLine($"Processing {count} orders...");
            }
        }

        // Bad pratice: Returning List<T> Directly
        // Clients can modify the list
        private readonly List<Product> _readonlyProducts = new List<Product>();

        // Good pratice: Returning ReadOnlyCollection<T>
        // Clients cannot modify the collection
        private readonly ReadOnlyCollection<Product> _readOnlyCollectionProducts = new List<Product>().AsReadOnly();

        public void Main()
        {
            var usage = new CollectionUsage();
            var products = usage.GetDiscountedProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
            }

            // Be able to Modify _readonlyProducts
            _readonlyProducts.Add(new Product { Id = 3, Name = "Tablet", Price = 500 });

            // Not be able to modify _readOnlyCollectionProducts
            _readOnlyCollectionProducts.Add(new Product { Id = 4, Name = "Smartwatch", Price = 300 }); // Compile-time error
        }
    }
}
