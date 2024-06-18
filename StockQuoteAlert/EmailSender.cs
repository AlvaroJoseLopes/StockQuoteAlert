using System;
using System.Net;
using System.Net.Mail;
using CLI;
using ProgramSettings;
using static ProgramSettings.Settings.SmtpSettings;

public class EmailSender
{
	private SmtpClient _client;
	private Settings.SmtpSettings _settings;

	public EmailSender(Settings.SmtpSettings settings)
	{
		_settings = settings;
		_client = new SmtpClient(settings.Host)
		{
			Port = settings.Port,
			Credentials = new NetworkCredential(settings.Username, settings.Password),
			EnableSsl=true,
		};
	}

	public void sendSellMessage(CLIArguments cliArgs, decimal price)
	{
		Console.WriteLine($"Sending email to each target suggesting selling {cliArgs.targetStock}");
        MailMessage message = new()
        {
            From = new MailAddress(_settings.Sender),
            Subject = $"Hello! It's a good time to SELL your {cliArgs.targetStock}!",
            Body = @$"Hello,
					We have been monitoring your {cliArgs.targetStock} asset and advise you to consider selling it. 
					The current market price is {price}, which is below your configured selling price of {cliArgs.sellPrice}.

					Best regards,
					StockQuoteAlert Team",
            IsBodyHtml = true,
        };
        foreach (var targetEmail in _settings.TargetEmails)
            message.To.Add(targetEmail.Email);

        _client.Send(message);
        Console.WriteLine("Emails sent!");
    }

    public void sendBuyMessage(CLIArguments cliArgs, decimal price)
    {
        Console.WriteLine($"Sending email to each target suggesting buying {cliArgs.targetStock}");
        MailMessage message = new()
        {
            From = new MailAddress(_settings.Sender),
            Subject = $"Hello! It's a good time to BUY your {cliArgs.targetStock}!",
            Body = @$"Hello,
					We have been monitoring your {cliArgs.targetStock} asset and advise you to consider buying it.
					The current market price is {price}, which is below your configured buying price of {cliArgs.buyPrice}.

					Best regards,
					StockQuoteAlert Team",
            IsBodyHtml = true,
        };
        foreach (var targetEmail in _settings.TargetEmails)
            message.To.Add(targetEmail.Email);

        _client.Send(message);
        Console.WriteLine("Emails sent!");
    }

}
