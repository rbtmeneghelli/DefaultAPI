using DefaultAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class EmailSettings
    {
        public string Host { get; set; }

        public string PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public string UsernameFrom { get; set; }

        public string UsernameEmail { get; set; }

        public string UserPassword { get; set; }

        public bool EnableSsl { get; set; }

        public bool IsDev { get; set; }

        public bool UseDefaultEmail { get; set; }

        public EmailSettings()
        {

        }

        public EmailSettings(EmailSettings emailSettings)
        {
            if (emailSettings is not null)
            {
                emailSettings.Host = emailSettings.Host.ToUpper();

                EnumEmail email = emailSettings.Host.Equals("GMAIL") ? EnumEmail.Gmail :
                                  emailSettings.Host.Equals("HOTMAIL") ? EnumEmail.Hotmail :
                                  EnumEmail.Outlook;

                string domain = getEmailDomain(email);
                Host = emailSettings.Host;
                PrimaryDomain = domain == string.Empty ? emailSettings.PrimaryDomain : domain;
                PrimaryPort = emailSettings.PrimaryPort;
                UsernameFrom = emailSettings.UsernameFrom;
                UsernameEmail = emailSettings.UsernameEmail;
                UserPassword = emailSettings.UserPassword;
                EnableSsl = emailSettings.EnableSsl;
                IsDev = emailSettings.IsDev;
            }
        }

        private string getEmailDomain(EnumEmail email)
        {
            Dictionary<EnumEmail, string> dictionary = new Dictionary<EnumEmail, string>();
            dictionary.Add(EnumEmail.Gmail, "smtp.gmail.com");
            dictionary.Add(EnumEmail.Hotmail, "smtp.live.com");
            dictionary.Add(EnumEmail.Outlook, "smtp.office365.com");
            return dictionary[email];
        }
    }
}
