using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Web;
using log4net;

namespace LayuiLearn.Common
{
    [Serializable]
    public class Log4NetLogger : ILogger
    {
        private readonly string _parentMethodDeclaringType;
        private readonly string _parentMethodName;

        private static readonly string loggerPrefix = ConfigurationManager.AppSettings["LoggerPrefix"];

        private readonly ILog logdebug = LogManager.GetLogger(loggerPrefix + ".Debug");
        private readonly ILog logerror = LogManager.GetLogger(loggerPrefix + ".Error");
        private readonly ILog loginfo = LogManager.GetLogger(loggerPrefix + ".Info");
        private readonly ILog logwarn = LogManager.GetLogger(loggerPrefix + ".Warn");

        //private readonly ILog logdebug = LogManager.GetLogger("DemoController.Debug");
        //private readonly ILog logerror = LogManager.GetLogger("DemoController.Error");
        //private readonly ILog loginfo = LogManager.GetLogger("DemoController.Info");
        //private readonly ILog logwarn = LogManager.GetLogger("DemoController.Warn");

        public Log4NetLogger()
        {
            try
            {
                var st = new StackTrace(true);
                _parentMethodDeclaringType = st.GetFrame(0).GetMethod().DeclaringType.ToString();
                _parentMethodName = st.GetFrame(0).GetMethod().ToString();
            }
            catch (NullReferenceException)
            {
                _parentMethodDeclaringType = "找不到类型";
                _parentMethodName = "找不到方法";
            }
        }

        public Log4NetLogger(ILog log)
        {
            logdebug = log;
            logerror = log;
            loginfo = log;
            logwarn = log;
        }

        #region ILogger Members

        public void Info(string msg, string contextInfo)
        {
            LogInfo(_parentMethodDeclaringType, _parentMethodName, 0, msg, contextInfo);
        }

        public void Debug(string msg, string contextInfo)
        {
            LogDebug(_parentMethodDeclaringType, _parentMethodName, msg, contextInfo);
        }

        public void Perf(DateTime startTime, string contextInfo)
        {
            LogWarn(startTime, _parentMethodDeclaringType, _parentMethodName, contextInfo);
        }

        public void Warn(string msg, string contextInfo)
        {
            LogError(_parentMethodDeclaringType, _parentMethodDeclaringType, AppError.WARN, 9999, string.Empty,
                            msg, contextInfo);
        }

        public void Error(string msg, string contextInfo)
        {
            LogError(_parentMethodDeclaringType, _parentMethodName, AppError.EROR, 9999, string.Empty, msg,
                            contextInfo);
        }

        public void Error(Exception ex, string msg, string contextInfo)
        {
            LogError(_parentMethodDeclaringType, _parentMethodName, AppError.EROR, 9999, ex, msg, contextInfo);
        }

        public void Error(string moduleID, string funcCode, string msg, string contextInfo)
        {
            LogError(moduleID, funcCode, AppError.EROR, 9999, string.Empty, msg, contextInfo);
        }

        public void Fatal(string msg, string contextInfo)
        {
            LogError(_parentMethodDeclaringType, _parentMethodName, AppError.FATL, 9999, string.Empty, msg,
                            contextInfo);
        }

        public void Fatal(Exception ex, string msg, string contextInfo)
        {
            LogError(_parentMethodDeclaringType, _parentMethodName, AppError.FATL, 9999, ex, msg, contextInfo);
        }

        #endregion

        /// <summary>
        /// 记录程序正常运行的日志
        /// </summary>
        /// <param name="moduleID">模块编号 （每个BLL类有一个模块编号）</param>
        /// <param name="funcCode">功能编号</param>
        /// <param name="userid">用户编号</param>
        /// <param name="msg">在异常或错误情况下给出的进一步的说明信息</param>
        /// <param name="contextInfo">上下文信息（由调用方生成，以名值对方式组织，如userid=123;username=test）</param>
        public void LogInfo(string moduleID, string funcCode, int userid, string msg, string contextInfo)
        {
            StringBuilder logMessage = new StringBuilder();

            logMessage.AppendFormat("Time={0}; ModuleID={1}; FuncCode={2}; UserID={3}; ", DateTime.Now, moduleID, funcCode, userid);

            if (!string.IsNullOrEmpty(contextInfo))
            {
                logMessage.AppendLine();
                logMessage.AppendFormat("[ContextInfo]={0}", contextInfo);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                logMessage.AppendLine();
                logMessage.AppendFormat("[Custom Message]={0}", msg);
            }

            logMessage.AppendLine();

            if (loginfo.IsInfoEnabled) loginfo.Info(logMessage.ToString());
        }

        /// <summary>
        /// 记调试日志
        /// </summary>
        /// <param name="moduleID">模块编号</param>
        /// <param name="funcCode">功能编号</param>
        /// <param name="msg">在异常或错误情况下给出的进一步的说明信息</param>
        /// <param name="contextInfo">上下文信息（由调用方生成，以名值对方式组织，如userid=123;username=test）</param>
        public void LogDebug(string moduleID, string funcCode, string msg, string contextInfo)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.AppendFormat("Time={0}; ModuleID={1}; FuncCode={2}; ", DateTime.Now, moduleID, funcCode);

            if (!string.IsNullOrEmpty(contextInfo))
            {
                logMessage.AppendLine();
                logMessage.AppendFormat("[ContextInfo]={0}", contextInfo);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                logMessage.AppendLine();
                logMessage.AppendFormat("[Custom Message]={0}", msg);
            }

            logMessage.AppendLine();

            if (logdebug.IsDebugEnabled) logdebug.Debug(logMessage.ToString());
        }

        /// <summary>
        /// 记错误日志或异常日志
        /// </summary>
        /// <param name="moduleID">模块编号</param>
        /// <param name="funcCode">功能编号</param>
        /// <param name="level">日志级别，3级（WARN，EROR，FATL）</param>
        /// <param name="errCode">错误码，对于没有明确错误码的场合需要给一个系统级的通用错误码</param>
        /// <param name="ex">发生的异常</param>
        /// <param name="msg">错误信息</param>
        /// <param name="contextInfo">错误上下文</param>
        public void LogError(string moduleID, string funcCode, AppError level, int errCode, Exception ex, string msg, string contextInfo)
        {
            LogError(moduleID, funcCode, level, errCode, BuildExceptionMessage(ex), msg, contextInfo);
        }

        /// <summary>
        /// 记错误日志或异常日志
        /// </summary>
        /// <param name="moduleID">模块编号</param>
        /// <param name="funcCode">功能编号</param>
        /// <param name="level">日志级别，3级（WARN，EROR，FATL）</param>
        /// <param name="errCode">错误码，对于没有明确错误码的场合需要给一个系统级的通用错误码</param>
        /// <param name="ex">发生的异常</param>
        /// <param name="msg">错误信息</param>
        /// <param name="contextInfo">错误上下文</param>
        public void LogError(string moduleID, string funcCode, AppError level, string errCode, Exception ex, string msg, string contextInfo)
        {
            LogError(moduleID, funcCode, level, errCode, BuildExceptionMessage(ex), msg, contextInfo);
        }

        /// <summary>
        /// 记错误日志或异常日志
        /// </summary>
        /// <param name="moduleID">模块编号</param>
        /// <param name="funcCode">功能编号</param>
        /// <param name="level">日志级别，3级（WARN，EROR，FATL）</param>
        /// <param name="errCode">错误码，对于没有明确错误码的场合需要给一个系统级的通用错误码</param>
        /// <param name="ex">发生的异常</param>
        /// <param name="msg">错误信息</param>
        /// <param name="contextInfo">错误上下文</param>
        public void LogError(string moduleID, string funcCode, AppError level, int errCode, string ex, string msg, string contextInfo)
        {
            LogError(moduleID, funcCode, level, errCode.ToString(), ex, msg, contextInfo);
        }

        /// <summary>
        /// 记错误日志或异常日志
        /// </summary>
        /// <param name="moduleID">模块编号</param>
        /// <param name="funcCode">权限代码</param>
        /// <param name="level">日志级别，3级（WARN，EROR，FATL）<</param>
        /// <param name="errCode">错误码，对于没有明确错误码的场合需要给一个系统级的通用错误码</param>
        /// <param name="ex">发生的异常</param>
        /// <param name="msg">错误信息</param>
        /// <param name="contextInfo">错误上下文</param>
        public void LogError(string moduleID, string funcCode, AppError level, string errCode, string ex, string msg, string contextInfo)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.AppendFormat("Time={0}; LogLevel={1}; ModuleID={2}; FuncCode={3}; ErrorCode={4}; ", DateTime.Now, level, moduleID, funcCode, errCode);

            if (!string.IsNullOrEmpty(contextInfo))
            {
                logMessage.AppendLine();
                logMessage.AppendFormat("[ContextInfo]={0}", contextInfo);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                logMessage.AppendLine();
                logMessage.AppendFormat("[Custom Message]={0}", msg);
            }

            if (ex != null)
            {
                logMessage.AppendLine();
                logMessage.AppendLine(ex);
                //logMessage.AppendLine(string.Format("Exception={0}", ex));
            }

            if (logerror.IsErrorEnabled) logerror.Error(logMessage.ToString());
        }

        /// <summary>
        /// 记性能日志
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="moduleID">模块编号</param>
        /// <param name="funcCode">功能编号</param>
        /// <param name="contextInfo">上下文信息（由调用方生成，以名值对方式组织，如userid=123;username=test）</param>
        public void LogWarn(DateTime startTime, string moduleID, string funcCode, string contextInfo)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime.Subtract(startTime);

            StringBuilder logMessage = new StringBuilder();
            logMessage.AppendFormat("StartTime={0}; EndTime={1}; Interval={2}(ms); ModuleID={3}; FuncCode={4}; ", startTime, DateTime.Now, ts.TotalMilliseconds, moduleID, funcCode);

            if (!string.IsNullOrEmpty(contextInfo))
            {
                logMessage.AppendLine();
                logMessage.AppendFormat("[ContextInfo]={0}", contextInfo);
            }

            logMessage.AppendLine();

            if (logwarn.IsWarnEnabled) logwarn.Warn(logMessage.ToString());
        }

        public static void Log(string logmsg, string loggerName)
        {
            LogManager.GetLogger(loggerName).Info(logmsg);
        }

        public static ILog GetLogger(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }

        #region Helper
        private static string BuildExceptionMessage(Exception x)
        {
            Exception logException = x;
            if (x.InnerException != null)
                logException = x.InnerException;

            string strErrorMsg = string.Empty;

            if (HttpContext.Current != null)
            {
                strErrorMsg += "[Error in Path]=" + HttpContext.Current.Request.Path;

                // Get the QueryString along with the Virtual Path
                strErrorMsg += Environment.NewLine + "[Raw Url]=" + HttpContext.Current.Request.RawUrl;
            }

            // Get the error message type
            strErrorMsg += Environment.NewLine + "[Exception Type]=" + logException.GetType().ToString();

            // Get the error message
            strErrorMsg += Environment.NewLine + "[Error Message]=" + logException.Message;

            // Source of the message
            strErrorMsg += Environment.NewLine + "[Source]=" + logException.Source;

            // Stack Trace of the error
            strErrorMsg += Environment.NewLine + "[Stack Trace]=" + logException.StackTrace;

            // Method where the error occurred
            strErrorMsg += Environment.NewLine + "[TargetSite]=" + logException.TargetSite;
            return strErrorMsg;
        }

        public enum AppError
        {
            WARN = 0,
            EROR = 1,
            FATL = 2
        }
        #endregion
    }
}