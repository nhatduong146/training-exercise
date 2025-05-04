//Limiting the maximum degree of parallelism to 3
var options = new ParallelOptions()
{
    MaxDegreeOfParallelism = Environment.ProcessorCount
};
//A maximum of three threads are going to execute the code parallelly
Parallel.For(1, 11, i =>
{
    Thread.Sleep(900);
    Console.WriteLine($"Value of i = {i}, Thread = {Thread.CurrentThread.ManagedThreadId}");
});

Console.ReadLine();