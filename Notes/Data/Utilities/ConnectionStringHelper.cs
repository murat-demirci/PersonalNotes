using Microsoft.Extensions.Configuration;

namespace Data.Utilities
{
    public static class ConnectionStringHelper
    {
        private static IConfigurationRoot Configuration { get; set; }
        public static string GetConncetion()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            return Configuration.GetConnectionString("NotesDB");
        }
    }
}
