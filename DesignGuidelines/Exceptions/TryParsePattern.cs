namespace DesignGuidelines.Exceptions
{
    // The Try-Parse Pattern is a coding pattern used to avoid throwing exceptions when parsing or converting values
    public class TryParsePattern
    {
        public static int ParseNumber(string numberStr)
        {
            return int.Parse(numberStr);  // Throws FormatException if parsing fails
        }

        // DO use the prefix "Try" and Boolean return type for methods implementing this pattern.
        public static bool TryParseNumber(string numberStr, out int number)
        {
            return int.TryParse(numberStr, out number);  // Returns false if parsing fails
        }
    }
}
