namespace DesignGuidelines.Exceptions
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double TotalPrice { get; set; }

        public void CalculateTotalPrice(List<Product> product)
        {
            TotalPrice = product[0].Price;
        }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }


    public class ExceptionDefinition
    {
        public static void Main()
        {
            try 
            {
                var order = new Order();
                order.CalculateTotalPrice(null);
            }
            catch (Exception ex)
            {
                // Message: A human-readable description of the error that occurred, providing details about why the exception was thrown.
                Console.WriteLine(ex.Message); // Object reference not set to an instance of an object.

                // StackTrace: A string representation of the frames on the call stack at the time the current exception was thrown.
                Console.WriteLine(ex.StackTrace); // at DesignGuidelines.Exceptions.Order.CalculateTotalPrice(List`1 product) in C:\Users\user\source\repos\csharp-design-guidelines\DesignGuidelines\Exceptions\ExceptionDefinition.cs:line 52

                // InnerException: The exception that is the cause of the current exception.
                Console.WriteLine(ex.InnerException); // null

                // HelpLink: A link to the help file associated with this exception.
                Console.WriteLine(ex.HelpLink); // null

                // Source: The name of the application or the object that causes the error.
                Console.WriteLine(ex.Source); // DesignGuidelines.Exceptions

                // TargetSite: The method that throws the current exception.
                Console.WriteLine(ex.TargetSite); // Void CalculateTotalPrice(System.Collections.Generic.List`1[DesignGuidelines.Exceptions.Product])

                // Data: Gets a collection of key/value pairs that provide additional user-defined information about the exception.
                Console.WriteLine(ex.Data); // System.Collections.ListDictionaryInternal

                // HResult: Gets or sets HRESULT, a coded numerical value that is assigned to a specific exception.
                Console.WriteLine(ex.HResult); // -2147467261

                // ToString: Returns a string representation of the current exception.
                Console.WriteLine(ex.ToString()); // System.NullReferenceException: Object reference not set to an instance of an object.

                // GetBaseException: When overridden in a derived class, returns the Exception that is the root cause of one or more subsequent exceptions.
                Console.WriteLine(ex.GetBaseException()); // System.NullReferenceException: Object reference not set to an instance of an object.

                // GetType: Gets the runtime type of the current instance.
                Console.WriteLine(ex.GetType()); // System.NullReferenceException
            }

        }
    }
}
