using Serilog.Core;
using Serilog.Events;

namespace LogSwitchWebApp;

public sealed class LogSwitchService(IDictionary<string, LoggingLevelSwitch> switches, IDictionary<string, LogEventLevel> defaultLevels) : ILogSwitchService
{
    public static LogSwitchService Create()
    {
        return new LogSwitchService(new Dictionary<string, LoggingLevelSwitch>(), new Dictionary<string, LogEventLevel>());
    }

    public void SaveSwitch(string switchName, LoggingLevelSwitch levelSwitch)
    {
        switches[switchName] = levelSwitch;
        defaultLevels[switchName] = levelSwitch.MinimumLevel;
    }

    public void SwitchLevel(LogEventLevel level)
    {
        var keys = switches.Keys.ToArray();

        foreach (var key in keys)
        {
            switches[key].MinimumLevel = level;
        }
    }

    public void RestoreLogging()
    {
        var keys = switches.Keys.ToArray();
        
        foreach (var key in keys)
        {
            switches[key].MinimumLevel = defaultLevels[key];
        }
    }
}