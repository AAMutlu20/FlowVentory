namespace FlowVentoryApp.Models;

public class SMTPSettings
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string FromEmail { get; set; }
}