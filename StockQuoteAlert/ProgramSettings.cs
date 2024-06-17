using System;
using System.Text.Json;
namespace ProgramSettings;


public class Settings
{
    public APISettings? Api { get; set; }
    /*public SmtpSettings? Smtp { get; set; }*/

    public class APISettings
    {
        public string Token { get; set; } = string.Empty;
        public int Delay { get; set; } = 10; // in seconds
    }
/*
    public class SmtpSettings
    {

    }
*/

}

public class Parser
{
    public static Settings? Parse(string filePath)
    {
        string content = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Settings>(content);
    }
}
