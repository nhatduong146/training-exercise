namespace DesignGuidelines.Types.ClassVsStruct.ArrayValue
{
    /*
     * Reference Types
     * Allocated on the heap, Garbage collection, Access via reference
     */
    public class Product
    {
        public string Name { get; set; }
    }

    /*
    * Value Types
    * Allocated on the stack or inline, Deallocation is automatic, Access by value
    */
    public struct Price
    {
        public decimal Amount { get; set; }
    }

    public class ArrayValue
    {
        public void ArrayOfClasses()
        {
            // Array of reference types
            // The array 'products' is on the heap, and each element points to an object on the heap
            Product[] products =
            [
                new Product { Name = "Laptop" },  // Reference to an object on the heap
                new Product { Name = "Tablet" },  // Another reference to an object on the heap
                new Product { Name = "Phone" },  // Same here
            ]; 

        }

        public void ArrayOfStructs()
        {
            // Array of value types
            // The entire array and its elements are stored contiguously in memory
            Price[] prices =
            [
                new Price { Amount = 100.00m },
                new Price { Amount = 200.00m },
                new Price { Amount = 300.00m },
            ]; 
        }
    }
}
