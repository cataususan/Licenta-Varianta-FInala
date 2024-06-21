using log4net.Config;
using log4net;
using System.Reflection;

namespace Backend_TimTour.Configuration
{
    public static class Log4NetExtensions
    {
        public static void AddLog4Net(this IServiceCollection services, string configFileName = "log4net.config")
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(configFileName));
        }
    }
}
