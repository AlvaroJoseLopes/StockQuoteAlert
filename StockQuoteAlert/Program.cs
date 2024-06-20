using StockAPI;

internal static class Program
{
    const string settingsFile = "settings.json";
    public static async Task Main(string[] args)
    {
        var cliArgs = Cli.Parser.Parse(args);
        var settings = ProgramSettings.Parser.Parse(settingsFile);
        var apiClient = new Client(settings.Api.Token);
        var sender = new Email.Email(settings.Smtp);

        while (true)
        {
            var price = await apiClient.GetMarketPrice(cliArgs.targetStock);
            Console.WriteLine($"Current price for {cliArgs.targetStock} is {price}.");
                
            if (price >= cliArgs.sellPrice)
                sender.sendSellMessage(cliArgs, price);
            else if (price <= cliArgs.buyPrice)
                sender.sendBuyMessage(cliArgs, price);

            await Task.Delay(settings.Api.Delay);
        }
    }
}
