namespace DesignGuidelines.Types.StaticClass
{
    public static class PaymentHelper
    {
        // All members of a static class are implicitly static.
        public static string DISCOUNT_CODE = "ABX_001";

        // Static classes cannot have instance constructors, only static constructors.
        public PaymentHelper() // Compile-time error
        {
        }

        // Static constructors
        // Runs only once when the class is first used.
        static PaymentHelper() // Correct
        {
        }

        // Static method
        public static void CalculateTotalPrice()
        {
        }
    }

    // A static class cannot be inherited because it is implicitly sealed.
    public class CreditPaymentHelper : PaymentHelper // Compile-time error
    {
    }

    public class StaticClass
    {
        public static void Main()
        {
            // Cannot create an instance of a static class using the new keyword.
            PaymentHelper helper = new PaymentHelper(); // Compile-time error

            // Access members of a static class using the class name.
            var discountCode = PaymentHelper.DISCOUNT_CODE;
        }
    }


    // Static class is useful for grouping utility or helper methods that don't require instance-specific data.
    public static class DatetimeConverter
    {
        public static DateTime ConvertToUtc(DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }

        public static DateTime ConvertToLocal(DateTime dateTime)
        {
            return dateTime.ToLocalTime();
        }

        public static string GetDayOfWeek(DateTime dateTime)
        {
            return dateTime.DayOfWeek.ToString();
        }
    }
}
