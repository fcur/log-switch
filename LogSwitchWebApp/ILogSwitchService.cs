using Serilog.Core;
using Serilog.Events;

namespace LogSwitchWebApp;

public interface ILogSwitchService
{
    /// <summary>
    /// Saves logging switches while reading the configuration.
    /// </summary>
    void SaveSwitch(string switchName, LoggingLevelSwitch levelSwitch);
    
    /// <summary>
    /// Switches all logging switchers to the specified level.
    /// </summary>
    /// <param name="level">target logging level</param>
    void SwitchLevel(LogEventLevel level);

    /// <summary>
    /// Restores initial logging levels for application.
    /// </summary>
    void RestoreLogging();
}