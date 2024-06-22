using System.Text.Json;
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
        string url = Url(stock);
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var responseStream = await response.Content.ReadAsStreamAsync();
        ThrowExceptionIfNull(responseStream);
        
        var parsedContent = await JsonNode.ParseAsync(responseStream);
        ThrowExceptionIfNull(parsedContent);
        
        var results = parsedContent!["results"];
        ThrowExceptionIfNull(results);
        
        var priceField = results![0];
        ThrowExceptionIfNull(priceField);
        
        var price = priceField!["regularMarketPrice"];
        ThrowExceptionIfNull(price);
        
        var priceValue = price!.GetValue<decimal>();
        return priceValue;
    }

    private void ThrowExceptionIfNull(object? x)
    {
        if (x == null)
        {
            throw new JsonException("Couldn't access correct fields from payload to retrieve current market price");
        }
    }

    private string Url(string stock)
    {
        return $"https://brapi.dev/api/quote/{stock}?token={_token}";
    }
}


