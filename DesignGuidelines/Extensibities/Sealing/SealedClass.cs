namespace DesignGuidelines.Extensibities.Sealing
{
    public sealed class SealedClass
    {
    }

    //A sealed class is a class that cannot be inherited from. It prevents other classes from deriving or extending it.
    public class Child : SealedClass // Compile-time error
    {
    }
}