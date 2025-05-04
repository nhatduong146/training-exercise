using System.Diagnostics;
using System.Threading;

namespace ConcurrencyLab.ThreadPoolTask
{
    public static class Handler
    {
        // Synchronous operation
        public static void ShowFinbonaccisSynchronously(int from, int to)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = from; i <= to; i++)
            {
                var result = NumberFactory.FindFibonacci(i);
                Console.WriteLine($"Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}, " +
                    $"Thread ID: {Thread.CurrentThread.ManagedThreadId}\", Fibonacci({i}): {result}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Time consumed by ShowFinbonaccisSynchronously is : " +
                $"{stopwatch.ElapsedTicks / (double)Stopwatch.Frequency}s\n\n");
        }



        // Asynchronous operation using Thread
        public static void ShowFinbonaccisWithThread(int from, int to)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Thread> threads = new();

            for (int i = from; i <= to; i++)
            {
                var number = i;
                var thread = new Thread(() =>
                {
                    var result = NumberFactory.FindFibonacci(number);
                    Console.WriteLine($"Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}, " +
                        $"Thread ID: {Thread.CurrentThread.ManagedThreadId}\", Fibonacci({number}): {result}");
                });

                thread.Start();
                threads.Add(thread);
            }

            // Join all threads after starting them
            foreach (var t in threads)
            {
                t.Join();
            }

            stopwatch.Stop();
            Console.WriteLine($"Time consumed by ShowFinbonaccisWithThread is : " +
                $"{stopwatch.ElapsedTicks / (double)Stopwatch.Frequency}s\n\n");
        }



        // Asynchronous operation using ThreadPool
        public static void ShowFinbonaccisWithThreadPool(int from, int to)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using var countdownEvent = new CountdownEvent(to - from + 1);

            for (int i = from; i <= to; i++)
            {
                var number = i;
                ThreadPool.QueueUserWorkItem(state =>
                {
                    var result = NumberFactory.FindFibonacci(number);
                    Console.WriteLine($"Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}, " +
                        $"Thread ID: {Thread.CurrentThread.ManagedThreadId}\", Fibonacci({number}): {result}");
                    countdownEvent.Signal();
                });
            }

            countdownEvent.Wait();
            stopwatch.Stop();
            Console.WriteLine($"Time consumed by ShowFinbonaccisWithThreadPool is : " +
                $"{stopwatch.ElapsedTicks / (double)Stopwatch.Frequency}s\n\n");
        }


        // Asynchronous operation using Task
        // Benifits: Support async/await (non bloking thread), cancellation support, exception handling
        public static async void ShowFinbonaccisWithTask(int from, int to)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(10));

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = new List<Task>();

            for (int i = from; i <= to; i++)
            {
                var number = i;
                tasks.Add(Task.Run(() =>
                {
                    var result = NumberFactory.FindFibonacci(number);
                    Console.WriteLine($"Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}, " +
                        $"Thread ID: {Thread.CurrentThread.ManagedThreadId}\", Fibonacci({number}): {result}");
                }));
            }
            try
            {
                await Task.WhenAll(tasks).WaitAsync(cts.Token);
                Console.WriteLine($"Time consumed by ShowFinbonaccisWithTask is : " +
                    $"{stopwatch.ElapsedTicks / (double)Stopwatch.Frequency}s\n\n");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("The operation was cancelled due to timeout.");
            }
        }
    }
}
