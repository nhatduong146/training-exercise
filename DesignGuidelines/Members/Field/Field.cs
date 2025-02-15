using System.Collections.Generic;

namespace DesignGuidelines.Members.Field
{
    public class OrderStatus
    {
        // DO use constant fields for constants that will never change.
        public const string PendingStatus = "Pending";

        // Public static readonly field for a predefined instance
        public static readonly OrderStatus Pending = new OrderStatus(PendingStatus);
        public static readonly OrderStatus Shipped = new OrderStatus("Shipped");

        public string Status { get; }

        // Private constructor to prevent external instantiation
        private OrderStatus(string status)
        {
            Status = status;
        }
    }

    // DO NOT assign instances of mutable types to readonly fields.(class, objec, array...)
    public class User
    {
        // Mutable type assigned to readonly field
        public static readonly List<string> UserRoles = new List<string> { "Admin", "User" };

        public string Name { get; set; }

        public static void Main()
        {
            // The roles can still be modified because the List is mutable
            UserRoles.Add("Manager");  // This will work even though UserRoles is readonly
        }
    }

    // Do assign instances of immutable types to readonly fields.
    public class User1
    {
        // Immutable collection assigned to readonly field
        public static readonly IReadOnlyList<string> UserRoles = new List<string> { "Admin", "User" }.AsReadOnly();

        public string Name { get; set; }

        public static void Main()
        {
            // Now, UserRoles cannot be modified, ensuring immutability
            UserRoles.Add("Manager");  // Compile-time error
        }
    }
    
    public class Field
    {
    }
}
