namespace FUT_23_LATEST_NEWS.API
{
    public class AppSettings
    {
        public string TelegramToken { get; }
        public string[] Recipients { get; }

        private AppSettings(string telegramToken, string[] recipients)
        {
            TelegramToken = telegramToken;
            Recipients = recipients;
        }

        public static AppSettings FromConfig(IConfiguration config)
        {
            var telegramToken = config.GetValue<string>("TELEGRAM_TOKEN") ?? throw new ArgumentNullException(nameof(config));
            var recipients = config.GetValue<string>("TELEGRAM_RECIPIENTS")?.Split(',') ?? new string[0];
            return new AppSettings(telegramToken, recipients);
        }
    }
}
