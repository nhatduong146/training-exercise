namespace DesignGuidelines.Exceptions
{
    /*
     * The Tester-Doer Pattern is a design pattern used in .NET 
     * to avoid throwing exceptions in common scenarios by first testing conditions 
     * before performing an action that might throw an exception.
     */
    public class TesterDoerParttern
    {
        // Bad practice: Accessing an array element without checking the index
        public void AccessElementBadPratice(int[] array, int index)
        {
            try
            {
                // Trying to access an index that may be out of range
                int value = array[index];
                Console.WriteLine($"Value at index {index}: {value}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Exception: Index is out of range.");
            }
        }



        // Tester method: Checks if the index is valid
        public bool IsValidIndex(int[] array, int index)
        {
            return index >= 0 && index < array.Length;
        }

        // Doer method: Executes the operation, assuming the preconditions are met
        public void AccessElement(int[] array, int index)
        {
            if (IsValidIndex(array, index)) // Check first
            {
                int value = array[index];
                Console.WriteLine($"Value at index {index}: {value}");
            }
            else
            {
                Console.WriteLine("Error: Index is out of range.");
            }
        }
    }
}
