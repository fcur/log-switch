using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;

namespace LogSwitchWebApp;

[Route("api/[controller]")]
[ApiController]
public sealed class DebugController : ControllerBase
{
    private readonly ILogger<DebugController> _logger;
    private readonly ILogSwitchService _logSwitchService;

    public DebugController(ILogger<DebugController> logger, ILogSwitchService logSwitchService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(logSwitchService);

        _logger = logger;
        _logSwitchService = logSwitchService;
    }

    [HttpGet("level")]
    public ActionResult<string> GetLevel()
    {
        var now = DateTimeOffset.UtcNow.ToString();
         
        _logger.LogTrace("This is a trace message: '{Now}'", now);
        _logger.LogDebug("This is a debug message: '{Now}'", now);
        _logger.LogInformation("This is an information message: '{Now}'", now);
        _logger.LogWarning("This is a warning message: '{Now}'", now);
        _logger.LogError("This is an error message: '{Now}'", now);
        _logger.LogCritical("This is a critical message: '{Now}'", now);

        var result = GetCurrentLevel();
        return result;
    }
    
    [HttpPut("set/trace")]
    public ActionResult<string> SetTraceLevel()
    {
        _logSwitchService.SwitchLevel(LogEventLevel.Verbose);
        
        var result = GetCurrentLevel();
        return result;
    }
    
    [HttpPut("set/debug")]
    public ActionResult<string> SetDebugLevel()
    {
        _logSwitchService.SwitchLevel(LogEventLevel.Debug);
        
        var result = GetCurrentLevel();
        return result;
    }

    [HttpPut("set/information")]
    public ActionResult<string> SetInformationLevel()
    {
        _logSwitchService.SwitchLevel(LogEventLevel.Information);
        
        var result = GetCurrentLevel();
        return result;
    }

    [HttpPut("set/warning")]
    public ActionResult<string> SetWarningLevel()
    {
        _logSwitchService.SwitchLevel(LogEventLevel.Warning);
        
        var result = GetCurrentLevel();
        return result;
    }

    [HttpPut("set/error")]
    public ActionResult<string> SetErrorLevel()
    {
        _logSwitchService.SwitchLevel(LogEventLevel.Error);
        
        var result = GetCurrentLevel();
        return result;
    }

    [HttpPut("set/critical")]
    public ActionResult<string> SetCriticalLevel()
    {
        _logSwitchService.SwitchLevel(LogEventLevel.Fatal);
        
        var result = GetCurrentLevel();
        return result;
    }
        
    [HttpPut("set/default")]
    public ActionResult<string> RestoreLogging()
    {
        _logSwitchService.RestoreLogging();
        
        var result = GetCurrentLevel();
        return result;
    }

    private ActionResult<string> GetCurrentLevel()
    {
        var minSerilogLoggingLevel = Enum.GetValues<LogEventLevel>().Where(Log.IsEnabled).Min();
        var minGenericLoggingLevel = Enum.GetValues<LogLevel>().Where(_logger.IsEnabled).Min();
        
        var severityLevel = (LogLevel)minSerilogLoggingLevel;

        if (severityLevel != minGenericLoggingLevel)
        {
            throw new Exception($"Application logging level '{minGenericLoggingLevel}' is not equal to serilog level: '{severityLevel}'.");
        }
        
        return Ok(severityLevel.ToString());
    }
}