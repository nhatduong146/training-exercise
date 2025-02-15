namespace DesignGuidelines.NamingConventions
{
    // When an enum represents a set of distinct, non-overlapping values, it should use a singular type name
    // DO NOT use an "Enum" suffix in enum type names 
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Canceled
    }

    public enum Color
    {
        Red,
        Blue,
        Black
    }

    // DO use a plural type name for an enumeration with bit fields as values, also called flags enum.
    // DO NOT use an "Enum" suffix in enum type names 
    [Flags]
    public enum FilePermissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 3,
        All = Read | Write | Execute

    }
}
