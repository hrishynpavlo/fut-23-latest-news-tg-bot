using Telegram.Bot;
using Microsoft.Extensions.Logging;
using FUT_23_LATEST_NEWS.Infrastructure.Models;
using FUT_23_LATEST_NEWS.Notifier.Formatters;
using Telegram.Bot.Types.Enums;

namespace FUT_23_LATEST_NEWS.Notifier.Telegram
{
    public class TelegramFutContentNotifier : IFutContentNotifier
    {
        private readonly TelegramBotClient _bot;
        private readonly string[] _recipients;
        private readonly TelegramFormatter _formatter;
        private readonly ILogger<IFutContentNotifier> _logger;

        public TelegramFutContentNotifier(string telegramToken, string[] recipients, ILogger<IFutContentNotifier> logger)
        {
            _bot = new TelegramBotClient(telegramToken);
            _recipients = recipients;
            _formatter = new TelegramFormatter();
            _logger = logger;
        }

        public async Task<bool> NotifyAllAsync(List<FutContent> notification)
        {
            try
            {
                var messages = _formatter.ToTelegramMessage(notification);
                foreach(var chatId in _recipients)
                {
                    foreach(var message in messages)
                    {
                        await _bot.SendTextMessageAsync(chatId, message, parseMode: ParseMode.Html);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "");
                return false;
            }
        }
    }
}
