using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace DegreeProjectsSystem.Utilidades
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
