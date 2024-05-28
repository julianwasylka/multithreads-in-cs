using System;
using System.Threading.Tasks;

internal class NewtonSymbol
{
    private int N;
    private int K;

    public NewtonSymbol()
    {
        Console.WriteLine("Enter N:");
        this.N = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter K:");
        this.K = int.Parse(Console.ReadLine());
    }

    public void UsingTask()
    {
        var taskN = Task.Run(() => BinomialCoefficientN(N, K));
        var taskK = Task.Run(() => BinomialCoefficientK(N, K));
        taskN.Wait(); 
        taskK.Wait();
        Console.WriteLine($"Binomial Coefficient (Task): {taskN.Result / taskK.Result}");
    }

    private delegate long BinomialCoefficientDelegate(int N, int K);

    public void UsingDelegate() 
    {
        BinomialCoefficientDelegate delN = new BinomialCoefficientDelegate(BinomialCoefficientN);
        BinomialCoefficientDelegate delK = new BinomialCoefficientDelegate(BinomialCoefficientK);

        long binomN = delN(N, K);
        long binomK = delK(N, K);

        Console.WriteLine($"Binomial Coefficient (Delegate): {binomN / binomK}");
    }

    static private Task<long> ComputeBinomialCoefficientAsyncN(int N, int K)
    {
        return Task.Run(() => BinomialCoefficientN(N, K));
    }
    static private Task<long> ComputeBinomialCoefficientAsyncK(int N, int K)
    {
        return Task.Run(() => BinomialCoefficientK(N, K));
    }
    public async Task UsingAsync()
    {
        long binomN = await ComputeBinomialCoefficientAsyncN(N, K);
        long binomK = await ComputeBinomialCoefficientAsyncK(N, K);
        Console.WriteLine($"Binomial Coefficient (async-await): {binomN / binomK}");
    }

    static private long BinomialCoefficientN(int N, int K)
    {
        long numerator = 1;
        for (int i = 0; i < K; i++)
        {
            numerator *= (N - i);
        }

        return numerator;
    }

    static private long BinomialCoefficientK(int N, int K)
    { 
        long denominator = 1;
        for (int i = 1; i <= K; i++)
        {
            denominator *= i;
        }

        return denominator;
    }
}