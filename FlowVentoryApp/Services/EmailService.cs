using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using System;
using FlowVentoryApp.Models;

namespace FlowVentoryApp.Services;

public class EmailService
{
    private readonly SMTPSettings _smtpSettings;

    public EmailService(IOptions<SMTPSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public void SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("FlowVentory", _smtpSettings.Username));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpSettings.Host, _smtpSettings.Port, true);
                client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
                client.Send(message);
                client.Disconnect(true);
            }

            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}
