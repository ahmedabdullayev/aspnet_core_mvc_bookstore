using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForConfirmation(UserEmailOptions userEmailOptions);
    }
}