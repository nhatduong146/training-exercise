 using ConcurrencyLab.ConsoleApp;

//OrderConcurrencyBag.ShowOrderList();

ThreadPool.GetMinThreads(out int maxWorkerThreads, out int maxIoThreads);
Console.WriteLine($"Max Worker Threads: {maxWorkerThreads}, Max IO Threads: {maxIoThreads}");

//static int Fibonacci(int n)
//{
//    return (n < 2) ? n : Fibonacci(n - 1) + Fibonacci(n - 2);
//}

//Console.WriteLine("Main thread starts");

//// Start a new task (it starts running immediately)
//Task task1 = Task.Run(() =>
//{
//    var finbonacy = Fibonacci(40);
//    Console.WriteLine("Task 1 completed: {0}", finbonacy);
//});

//// Await task1 (this pauses execution here until task1 completes)
//await task1;

//Console.WriteLine("Main thread resumes after task 1.");
//Console.ReadLine();


/*
class Program
{
    static async Task Main()
    {
        Console.WriteLine("Main starts");

        DoWorkAsync(); // Call async method WITHOUT await (fire-and-forget)

        Console.WriteLine("Main continues without waiting...");

        await Task.Delay(3000); // Ensure the program doesn't exit immediately
    }

    static async Task DoWorkAsync()
    {
        await Task.Delay(2000); // Simulate a delay
        Console.WriteLine("DoWorkAsync completed.");
    }
}
*/