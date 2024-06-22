namespace Cli;

public class CliArguments
{
	public readonly string targetStock;
	public readonly decimal sellPrice;
	public readonly decimal buyPrice; 

	public CliArguments(string targetStock, decimal sellPrice, decimal buyPrice)
	{
        if (sellPrice < buyPrice)
        {
            throw new Exception($"Buying price should be greater than selling price.");
        }

        this.targetStock = targetStock;
		this.sellPrice = sellPrice;
		this.buyPrice = buyPrice;
	}
}

internal class Parser
{
	public static CliArguments Parse(string[] args)
	{
		if (args.Count() != 3)
		{
			throw new Exception("Incorrect number of CLI arguments. Expected 3: targetStock sellPrice buyPrice");
		}

		string targetStock = args[0];
		if (!decimal.TryParse(args[1], out decimal sellPrice))
		{
			throw new Exception($"Couldn't parse selling price: {args[1]}");
		}

        if (!decimal.TryParse(args[2], out decimal buyPrice))
        {
            throw new Exception($"Couldn't parse buying price: {args[2]}");
        }


        return new CliArguments(targetStock, sellPrice, buyPrice);
	}
}
