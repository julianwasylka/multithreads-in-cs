using System;
using System.Threading.Tasks;

class Program
{
    static async Task Newton()
    {
        NewtonSymbol ns = new NewtonSymbol();

        ns.UsingTask();

        ns.UsingDelegate();

        await ns.UsingAsync();
    }

    static async Task Main(string[] args)
    {
        await Newton();

        Fibonnaci fibonnaci = new Fibonnaci();
        fibonnaci.Fibo();
    }
}