using Serilog.Core;
using Serilog.Events;

namespace LogSwitchWebApp;

public interface ILogSwitchService
{
    void SaveSwitch(string switchName, LoggingLevelSwitch levelSwitch);
    void SwitchLevel(LogEventLevel  level);

    void RestoreLogging();

}