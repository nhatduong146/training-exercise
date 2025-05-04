namespace ConcurrencyLab.ThreadPoolTask
{
    public static class NumberFactory
    {
        public static long FindFibonacci(int n)
        {
            if (n <= 1) return n;
            return FindFibonacci(n - 1) + FindFibonacci(n - 2);
        }
    }
}
