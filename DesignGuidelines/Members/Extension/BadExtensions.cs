namespace DesignGuidelines.Members.Extension
{
    public static class BadExtensions
    {
        /*
         * AVOID defining extension methods on System.Object
         * Since System.Object is the base class for all types in .NET, 
         * an extension method defined on System.Object becomes available on every type
         */
        public static void ConvertToString(this object obj)
        {
            // Convert object to string
        }

        /*
         * CONSIDER defining extension methods because they can be used everywhere
         * This is public for every string type
         */
        public static void GetDiscount(string productCode)
        {
            // Get discount logic
        }
    }
}
