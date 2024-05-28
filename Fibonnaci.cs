using System;
using System.ComponentModel;
using System.Threading;

class Fibonnaci
{
    public void Fibo()
    {
        Console.WriteLine("Enter i:");
        int i = int.Parse(Console.ReadLine());

        BackgroundWorker fibWorker = new BackgroundWorker();
        fibWorker.WorkerReportsProgress = true;
        fibWorker.DoWork += FibonacciWorker_DoWork;
        fibWorker.ProgressChanged += FibonacciWorker_ProgressChanged;
        fibWorker.RunWorkerCompleted += FibonacciWorker_RunWorkerCompleted;

        fibWorker.RunWorkerAsync(i);

        Console.WriteLine("Calculating...");
        Console.ReadLine();
    }

    private static void FibonacciWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        int n = (int)e.Argument;
        int fibNMinus2 = 0;
        int fibNMinus1 = 1;
        int fibCurrent = 0;

        for (int i = 2; i <= n; i++)
        {
            fibCurrent = fibNMinus1 + fibNMinus2;
            fibNMinus2 = fibNMinus1;
            fibNMinus1 = fibCurrent;

            Thread.Sleep(5); // Spowolnienie obliczeń

            int progressPercentage = (int)((double)i / n * 100);
            (sender as BackgroundWorker).ReportProgress(progressPercentage, fibCurrent);
        }

        e.Result = fibCurrent;
    }

    private static void FibonacciWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        Console.WriteLine($"Progress: {e.ProgressPercentage}%");
    }

    private static void FibonacciWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Console.WriteLine($"An error occurred: {e.Error.Message}");
        }
        else
        {
            int fibResult = (int)e.Result;
            Console.WriteLine($"Fibonacci Result: {fibResult}");
        }
    }
}

