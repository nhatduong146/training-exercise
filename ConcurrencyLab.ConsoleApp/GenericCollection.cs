using System.Collections;

namespace ConcurrencyLab.ConsoleApp
{
    public class GenericCollection
    {
        public static void CreateNonGenericCollection()
        {
            var list = new ArrayList()
            {
                1, 2, 3, "None"
            };

            foreach (object item in list)
            {
                // Will throw InvalidCastException if you try:
                int number = (int)item; 
                Console.WriteLine(number);
            }
        }

        public static void CreateGenericCollection()
        {
            var numbers = new List<int>()
            {
                1, 2, 3
            };

            foreach (int number in numbers)
            {
                Console.WriteLine(number); 
            }
        }
    }
}
