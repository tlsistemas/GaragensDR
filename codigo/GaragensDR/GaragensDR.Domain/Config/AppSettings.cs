using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace GaragensDR.Domain.Config
{
    public class AppSettings
    {
        public IConfiguration _configuration { get; }
        private static string IP { get; set; }
        public static string ConectionString { get; set; }


        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            if (Debugger.IsAttached)
            {
                ConectionString = _configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                ConectionString = _configuration.GetConnectionString("DefaultConnection");
            }
        }

    }
}