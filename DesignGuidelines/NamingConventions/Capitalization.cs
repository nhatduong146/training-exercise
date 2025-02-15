namespace DesignGuidelines.NamingConventions // PascalCase for namespace
{
    public class Product // PascalCase for class
    {
        public string Name { get; set; } // PascalCase for properties

        public double Price { get; set; }

        public void Init(string name, double price) // camelCase for parameters
        {
            Name = name;
            Price = price; 
        }

        public double GetPriceWithDiscount(double percentageDiscount)
        {
            var discount = Price * percentageDiscount / 100; // camelCase for variables
            return Price - discount;
        }
    }

    public interface IProduceService // PascalCase for inteface
    {
    }

    // DO NOT capitalize each word in so-called closed-form compound words.
    public class Website 
    {
        public string Endpoint { get; set; } // Instead of EndPoint

        public string Namespace { get; set; } // Instead of NameSpace  

        public void Callback() // Instead of CallBack
        {
        }
    }
}
