using MailKit.Net.Smtp;
using MimeKit;
using Serilog;

namespace AssetManagement.API.DAL.Services.EmailService;

public class EmailService(IConfiguration config) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string htmlBody)
    {
        var settings = config.GetSection("EmailSettings");
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(settings["SenderName"], settings["SenderEmail"]));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = htmlBody
        };
        
        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(settings["SmtpServer"], int.Parse(settings["SmtpPort"]!), false);
            await client.AuthenticateAsync(settings["SenderEmail"], settings["SenderPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            Log.Information("[Email] Sent email to {To}: {Subject}", to, subject);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "[Email] Failed to send email to {To}", to);
        }
    }
}