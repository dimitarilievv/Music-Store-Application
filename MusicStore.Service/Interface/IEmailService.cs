using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Domain.Email;

namespace MusicStore.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
