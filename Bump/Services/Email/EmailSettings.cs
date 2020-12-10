// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Bump.Services.Email {

    // ReSharper disable once ClassNeverInstantiated.Global
    public class EmailSettings {

        public const string SectionName = "MailSettings";

        public string Mail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }

}