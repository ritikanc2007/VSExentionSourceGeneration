
using Restarted.System.Contracts.Interfaces.Services;

namespace Restarted.System.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
