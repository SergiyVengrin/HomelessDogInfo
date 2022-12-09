using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmail(EmailModel emailModel);
    }
}
