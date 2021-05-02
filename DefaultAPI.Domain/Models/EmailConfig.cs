using DefaultAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class EmailConfig
    {
        public MailAddress emailFrom { get; set; }
        public string emailTitle { get; set; }
        public string emailTo { get; set; }
        public string emailSubject { get; set; }
        public string emailBody { get; set; }
        public EnumMailPriority emailPriority { get; set; }
        public EnumEmailDisplay enumEmailDisplay { get; set; }
        public EnumEmailTemplate enumEmailTemplate { get; set; }
        public bool emailIsHtml { get; set; }
        public EmailSettings emailSettings { get; set; }
        public string userName { get; set; }

        public EmailConfig(string emailTitle, string emailTo, string emailSubject, string emailBody, string userName, EnumEmailDisplay enumEmailDisplay = EnumEmailDisplay.Padrao, EnumMailPriority emailPriority = EnumMailPriority.Normal, EnumEmailTemplate enumEmailTemplate = EnumEmailTemplate.DefaultAPI, bool emailIsHtml = true)
        {
            this.emailTitle = emailTitle;
            this.emailTo = emailTo;
            this.emailSubject = emailSubject;
            this.emailBody = emailBody;
            this.enumEmailDisplay = enumEmailDisplay;
            this.emailPriority = emailPriority;
            this.enumEmailTemplate = enumEmailTemplate;
            this.emailIsHtml = emailIsHtml;
            this.userName = userName;
        }

        public EmailConfig(EmailSettings emailSettings, EmailConfig emailConfig)
        {
            this.emailFrom = new MailAddress(emailSettings.UsernameEmail, emailConfig.emailTitle);
            this.emailTo = emailConfig.emailTo;
            this.emailSubject = emailConfig.emailSubject;
            this.emailBody = emailConfig.emailBody;
            this.enumEmailDisplay = emailConfig.enumEmailDisplay;
            this.emailPriority = emailConfig.emailPriority;
            this.enumEmailTemplate = emailConfig.enumEmailTemplate;
            this.emailIsHtml = emailConfig.emailIsHtml;
            this.userName = emailConfig.userName;
            this.emailSettings = emailSettings;
        }
    }
}
