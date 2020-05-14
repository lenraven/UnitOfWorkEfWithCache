using Microsoft.Extensions.Logging;

namespace UnitOfWorkSampleTest
{
    public class Logger
    {
        public static readonly ILoggerFactory EfLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
