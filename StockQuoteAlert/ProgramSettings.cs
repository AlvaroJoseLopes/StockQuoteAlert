using System;
using System.Text.Json;
namespace ProgramSettings;


public class Settings
{
    public required APISettings Api { get; set; }
    public required SmtpSettings Smtp { get; set; }

    public class APISettings
    {
        public required string Token { get; set; }
        public int Delay { get; set; } = 10; // in seconds
    }

    public class SmtpSettings
    {
        public required string Host { get; set; }
        public int Port { get; set; } = 587; // Default for Mailtrap
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Sender { get; set; } = "noreply@stockalert.com";

        public class TargetEmail
        {
            public required string Email { set; get; }
            public required string Name { set; get; }
        }
        public required IList<TargetEmail> TargetEmails { get; set; }
    }


}

public class Parser
{
    public static Settings Parse(string filePath)
    {
        string content = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Settings>(content);
    }
}
