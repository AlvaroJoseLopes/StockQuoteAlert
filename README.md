# Stock Quote Alert

## Summary

Stock Quote Alert is a CLI application written in C# that tracks a stock price and notifies a set of users by email suggesting buying or selling it, based on a specified threshold configured by the user.
[Brapi API](https://brapi.dev/docs) was used to track stock prices and [Mailtrap](https://mailtrap.io/) for sending emails to users.

This is my solution for the take-home assignment for the Software Engineer position at Inoa Systems.

## How to run

### Setup

Before running the project, it's necessary to configure the program settings, which will be used to authenticate requests to BrapiAPI and Mailtrap, and provide the list of target emails to be sent. The `settings.json` should follow the standard below:

```json
{
	"Api": {
		"Token": "YOUR_BRAPI_API_TOKEN",
		"Delay": 10000
	},
	"Smtp": {
		"Host": "SMTP_HOST",
		"Port": 587, 
		"Username": "SMTP_USERNAME",
		"Password": "SMTP_PASSWORD",
		"Sender": "noreply@stockalert.com",
		"TargetEmails": [
			{
				"Email": "example1@domain.com",
				"Name": "Example1"
			},
			{
				"Email": "example2@domain.com",
				"Name": "Example2"
			}
		]
	}
}
```

### Compiling and Running

The solution was developed using Visual Studio 2022, so the best way to compile and run is to clone this repo, open and compile it in Visual Studio.

The CLI application takes 3 positional arguments:
* Stock symbol to monitor
* Selling price
* Buying price

```bash
StockQuoteAlert.exe PETR4 22,67 22,59
```

## Project Structure

* [Program.cs](StockQuoteAlert/Program.cs): contains the main application logic.
* [CLI.cs](StockQuoteAlert/CLI.cs): CLI arguments parser.
* [ProgramSettings.cs](StockQuoteAlert/ProgramSettings.cs): JSON program settings parser.
* [StockAPI.cs](StockQuoteAlert/StockAPI.cs): Wrapper for Brapi's API that gets the current price of the monitored stock.
* [EmailSender.cs](StockQuoteAlert/EmailSender.cs): Wrapper for the SMTP client that notifies users by email.

## Next Steps

Currently, this program only tracks one stock symbol and supports a single selling/buying price.
So, one next step is to expand the program to track multiple stocks and send personalized email notifications with user-defined price stocks of interest and thresholds. To handle this, a pub/sub pattern could be used.
