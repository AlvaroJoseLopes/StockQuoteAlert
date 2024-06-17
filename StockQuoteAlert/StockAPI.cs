using System;
using System.Text.Json.Nodes;
namespace StockAPI;

public class Client
{
    private string _token;
    private HttpClient _client;

    public Client(string token)
    {
        _token = token;
        _client = new HttpClient();
    }

    public async Task<decimal> GetMarketPrice(string stock)
    {
        string content = String.Empty;
        string url = Url(stock);

        try
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            content = await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to get {stock} market price");
            Console.WriteLine($"Exception: {e.Message}");
        }


        return JsonNode.Parse(content)!["results"]![0]!["regularMarketPrice"]!.GetValue<decimal>();
    }

    private string Url(string stock)
    {
        return $"https://brapi.dev/api/quote/{stock}?token={_token}";
    }
}


