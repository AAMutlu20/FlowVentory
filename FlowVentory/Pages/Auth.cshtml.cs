using FlowVentory.BLL.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowVentory.Pages.Shared;

public class Auth : PageModel
{
    private readonly IEmailService _emailService;

    public Auth(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task OnPostAsync()
    {
        // TO DO Registration logic
        
        // Send a confirmation email
        string subject = "Registration Confirmation";
        string body = "<h1>Thank you for registering!</h1>";
        await _emailService.SendEmailAsync("user@example.com", subject, body);
    }
}