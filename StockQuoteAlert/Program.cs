internal static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var cliArgs = CLI.Parser.Parse(args);
            var settings = ProgramSettings.Parser.Parse("settings.json");
            var api_client = new StockAPI.Client(settings.Api.Token);
            var sender = new EmailSender(settings.Smtp);

            while (true)
            {
                var price = await api_client.GetMarketPrice(cliArgs.targetStock);
                Console.WriteLine($"Current price for {cliArgs.targetStock} is {price}.");
                
                if (price >= cliArgs.sellPrice)
                    sender.sendSellMessage(cliArgs, price);
                else if (price <= cliArgs.buyPrice)
                    sender.sendBuyMessage(cliArgs, price);

                Thread.Sleep(settings.Api.Delay);
            }
                
        } 
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
            Environment.Exit(1);
        }
    }
}
