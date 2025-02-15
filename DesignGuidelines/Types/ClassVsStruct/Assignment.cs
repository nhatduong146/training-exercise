namespace DesignGuidelines.Types.ClassVsStruct.Assignment
{
    public class ProductClass
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public struct ProductStruct
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class Assignment
    {
        /*
         * The assignment operation is cheap because only the reference (pointer) is copied.
         * No duplication of the object occurs; they both point to the same memory location.
         */
        public void AssignClass()
        {
            ProductClass productA = new() { Name = "Laptop", Price = 50 };
            ProductClass productB = productA; // Reference assignment

            productB.Price = productB.Price - 10; // Modify price using productB

            Console.WriteLine($"Product A Price: {productA.Price}"); // Output: 40
            Console.WriteLine($"Product B Price: {productB.Price}"); // Output: 40
        }

        /*
         * The assignment operation is expensive because the entire value is copied.
         * For large structs or complex data types, this can involve significant memory and CPU overhead, as the entire value needs to be duplicated.
         */
        public void AssignStruct()
        {
            ProductStruct productA = new() { Name = "Laptop", Price = 100 };
            ProductStruct productB = productA; // Value copy

            productB.Price = productB.Price - 10; // Modify price using productB

            Console.WriteLine($"Product A Price: {productA.Price}"); // Output: 100
            Console.WriteLine($"Product B Price: {productB.Price}"); // Output: 90
        }
    }
}
