internal static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var cliArgs = CLI.Parser.Parse(args);
            var settings = ProgramSettings.Parser.Parse("settings.json");
            var client = new StockAPI.Client(settings!.Api!.Token);
            while (true)
            {
                var price = await client.GetMarketPrice(cliArgs.targetStock);
                Console.WriteLine(price);
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
