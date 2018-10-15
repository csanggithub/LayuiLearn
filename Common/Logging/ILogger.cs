using System;

namespace Common
{
    public interface ILogger
    {
        void Info(string msg, string contextInfo);
        void Debug(string msg, string contextInfo);
        void Perf(DateTime startTime, string contextInfo);
        void Warn(string msg, string contextInfo);
        void Error(string msg, string contextInfo);
        void Error(Exception ex, string msg, string contextInfo);
        void Error(string moduleID, string funcCode, string msg, string contextInfo);
        void Fatal(string msg, string contextInfo);
        void Fatal(Exception ex, string msg, string contextInfo);
    }
}