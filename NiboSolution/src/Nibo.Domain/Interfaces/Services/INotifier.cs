using Nibo.Domain.Services;
using System.Collections.Generic;

namespace Nibo.Domain.Interfaces.Services
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notificacao);
    }
}