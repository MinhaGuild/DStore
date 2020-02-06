using Database.Context;
using DStore.Helpers;
using System.Threading.Tasks;

namespace DStore.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string subject, string pass, string name, DatabaseContext _context, AppSettings _appSettings, bool create = true);

    }
}
