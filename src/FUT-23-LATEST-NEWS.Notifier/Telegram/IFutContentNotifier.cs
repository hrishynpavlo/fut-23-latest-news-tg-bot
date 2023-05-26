using FUT_23_LATEST_NEWS.Infrastructure.Models;

namespace FUT_23_LATEST_NEWS.Notifier.Telegram
{
    public interface IFutContentNotifier
    {
        Task<bool> NotifyAllAsync(List<FutContent> notification);
    }
}
