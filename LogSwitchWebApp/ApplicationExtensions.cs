using Serilog;
using Serilog.Settings.Configuration;

namespace LogSwitchWebApp;

public static class ApplicationExtensions
{
    public static WebApplicationBuilder AddSerilogWithSwitch(this WebApplicationBuilder builder)
    {
        var logSwitchService = LogSwitchService.Create();
        
        builder.Host.UseSerilog((context, configuration) =>
        {
            var options = new ConfigurationReaderOptions
            {
                OnLevelSwitchCreated = (switchName, levelSwitch) =>
                {
                    logSwitchService.SaveSwitch(switchName, levelSwitch);
                }
            };
    
            configuration.ReadFrom.Configuration(context.Configuration,options);
        });
        
        builder.Services.AddSingleton<ILogSwitchService>(logSwitchService);
        
        return builder;
    }
}