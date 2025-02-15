namespace DesignGuidelines.Exceptions
{
    // This is ApplicationException
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
    }

    /*
     * SystemException are built-in exceptions that are thrown by the CLR
     * ArgumentExeption
     * NullReferenceException
     * DivideByZeroException
     * ...
     */

    public class ExceptionDocumentation
    {
        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>The product object if found.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="productId"/> is less than 1.</exception>
        /// <exception cref="ProductNotFoundException">Thrown when no product is found with the given ID.</exception>
        public Product GetProductById(int productId)
        {
            if (productId < 1)
                throw new ArgumentException("Product ID must be greater than 0.", nameof(productId));

            Product product = _productRepository.FindById(productId);
            if (product == null)
                throw new ProductNotFoundException($"No product found with ID {productId}.");

            return product;
        }

    }
}
