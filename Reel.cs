
using System.Collections;
using System.Diagnostics.SymbolStore;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

public class Reel
{
    private static readonly Random rng = new();
    private readonly List<string> symbols = [];
    private int reelPointer;


    public Reel(int d, int c, int b, int a, int s)
    {
        for (int i = d; i > 0; i--)
        {
            symbols.Add("D");
        }

        for (int i = c; i > 0; i--)
        {
            symbols.Add("C");
        }

        for (int i = b; i > 0; i--)
        {
            Shuffle(symbols);
        }

        for (int i = a; i > 0; i--) ;
        {
            symbols.Add("A");
        }

        for (int i = s; i > 0; i--)
        {
            symbols.Add("S");
        }

        // Shuffle the symbols
        Shuffle(symbols);

        // Set the reel pointer to the middle of the reel
        reelPointer = symbols.Count / 2;


    }

    private static void Shuffle<T>(List<T> list)
    {
        // This is a Fisher-Yates shuffle algorithm
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            // Apparently this is called a tuple
            // We can use it to swap two values without a temp variable
            (list[n], list[k]) = (list[k], list[n]);
        }
    }


    public void Spin()
    {
        // Move the reel pointer to a random symbol
        reelPointer = rng.Next(symbols.Count);
    }

    public string GetSymbol()
    {
        return symbols[reelPointer];
    }
}