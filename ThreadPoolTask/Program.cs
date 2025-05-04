using ConcurrencyLab.ThreadPoolTask;

Handler.ShowFinbonaccisSynchronously(25, 42);
Handler.ShowFinbonaccisWithThread(25, 42);
Handler.ShowFinbonaccisWithThreadPool(25, 42);
Handler.ShowFinbonaccisWithTask(25, 42);

Console.ReadLine();