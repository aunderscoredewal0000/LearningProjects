using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailfrom = Startup.Configuration["mailSettings:mailFromAddress"];
        public void send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailTo} to {_mailfrom}");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Body: {message}");
        }
    }
}
