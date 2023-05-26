using FUT_23_LATEST_NEWS.Infrastructure.Models;
using System.Text;

namespace FUT_23_LATEST_NEWS.Notifier.Formatters
{
    public class TelegramFormatter
    {
        public List<string> ToTelegramMessage(List<FutContent> contents)
        {
            var result = new List<string>();

            foreach(var content in contents)
            {
                var sb = new StringBuilder();

                var title = content.Type switch
                {
                    FutContentType.NewPacks => "НОВІ ПАКИ!!!",
                    FutContentType.NewSBCs => "НОВІ СБЧ!!!",
                    _ => ""
                };

                sb.AppendLine($"<b>{title}</b>");
                sb.AppendLine();

                int number = 1;

                foreach (var item in content.ContentItems)
                {
                    sb.AppendLine($"<b>{number}. {item.Name}</b>");
                    sb.AppendLine($"<i>{item.Description}</i>");
                    sb.AppendLine();
                    number++;
                }
                result.Add(sb.ToString());
            }

            return result;
        }
    }
}
