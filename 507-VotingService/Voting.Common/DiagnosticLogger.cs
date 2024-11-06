using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Common
{
    public class DiagnosticLogger : IDiagnosticLogger
    {
        private readonly Serilog.Core.Logger SerilogLogger;
        public DiagnosticLogger()
        {
            SerilogLogger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), "c:/src/projects/DailyCodingProblems/507/Logs/Important.json", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
                .WriteTo.File("c:/src/projects/DailyCodingProblems/507/Logs/All", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .CreateLogger();
        }

        public void Error(string message)
        {
            SerilogLogger.Error(message);
        }

        public void Fatal(string message)
        {
            SerilogLogger.Fatal(message);
        }

        public void Info( string message)
        {
            SerilogLogger.Information(message);
        }

        public void Warn(string message)
        {
            SerilogLogger.Warning(message);
        }
    }
}
