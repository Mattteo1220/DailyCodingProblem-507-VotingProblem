namespace Voting.Common
{
    public interface IDiagnosticLogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
    }
}