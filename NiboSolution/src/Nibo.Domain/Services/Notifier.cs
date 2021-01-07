using Nibo.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Nibo.Domain.Services
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notificacao)
        {
            _notifications.Add(notificacao);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}