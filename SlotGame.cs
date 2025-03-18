

using System.Security.Cryptography.X509Certificates;

public class SlotGame
{
    private int tokens;
    private readonly Reel reel1;
    private readonly Reel reel2;
    private readonly Reel reel3;

    public SlotGame(int tokens)
    {
        this.tokens = tokens;
        reel1 = new Reel(3, 4, 3, 2, 1);
        reel2 = new Reel(3, 4, 2, 2, 1);
        reel3 = new Reel(3, 4, 3, 2, 1);
    }

    public void PullLever()
    {
        if (tokens > 0)
        {
            tokens--;
            reel1.Spin();
            reel2.Spin();
            reel3.Spin();
            System.Console.WriteLine("Spinning . . .\n");
            System.Console.WriteLine("\t┏━━━━━━━┓");
            System.Console.WriteLine("\t┃╔═╦═╦═╗┃");
            System.Console.WriteLine($"\t┃║{reel1.GetSymbol()}║{reel2.GetSymbol()}║{reel3.GetSymbol()}║┃ O");
            System.Console.WriteLine("\t┃╚═╩═╩═╝┃ ┃");
            System.Console.WriteLine("\t┃ ┄┄┄┄┄ ┣━┛");
            int score = GetScore();
            if (score > 0)
            {
                System.Console.WriteLine("\t┃ !WIN! ┃");
                System.Console.WriteLine("\t┃ ┄┄┄┄┄ ┃");
                System.Console.WriteLine("\t┗━━━━━━━┛\n");
                tokens += score;
                System.Console.WriteLine($"You won {score} tokens!");
            }
            else
            {
                System.Console.WriteLine("\t┃       ┃");
                System.Console.WriteLine("\t┃ ┄┄┄┄┄ ┃");
                System.Console.WriteLine("\t┗━━━━━━━┛\n");
                System.Console.WriteLine("You lost!");
            }
            System.Console.WriteLine();
            System.Console.WriteLine($"You have {tokens} tokens.\n");
        }
        else
        {
            System.Console.WriteLine("You don't have enough tokens to play.\n");
        }

    }

    public int GetScore()
    {
        if (reel1.GetSymbol() == reel2.GetSymbol() && reel2.GetSymbol() == reel3.GetSymbol())
        {
            return reel1.GetSymbol() switch
            {
                "S" => 1000,
                "A" => 100,
                "B" => 20,
                "C" => 10,
                "D" => 5,
                _ => 0,
            };
        }
        else
        {
            return 0;
        }
    }
    

}