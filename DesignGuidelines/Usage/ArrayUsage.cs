using System.Collections.Immutable;

namespace DesignGuidelines.Usage
{
    public class ArrayUsage
    {
        // AVOID using a Read-only Array Field
        public readonly string[] ProductCategories = { "Electronics", "Clothing", "Books" };

        // SHOULD using an Immutable Collection
        public static readonly ImmutableArray<string> ImmutableProductCategories = ["Electronics", "Clothing", "Books"];


        public void Main()
        {
            ProductCategories[0] = "IT";  // Allowed, even though the field is read-only!

            ImmutableProductCategories[0] = "IT";  // Compile-time error
        }
    }
}
