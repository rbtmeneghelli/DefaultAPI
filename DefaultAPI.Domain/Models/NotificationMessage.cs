using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class NotificationMessage
    {
        public string Mensagem { get; }

        public NotificationMessage(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
