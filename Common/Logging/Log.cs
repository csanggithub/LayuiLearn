using System;
//using Microsoft.Practices.ServiceLocation;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static ILogger GetLog()
        {
            try
            {
                //return ServiceLocator.Current.GetInstance<ILogger>("logger");
                return new Log4NetLogger();
            }
            catch
            {
                return new Log4NetLogger();
            }
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="msg"></param>
        ///<param name="contextInfo"></param>
        public static void Info(string msg, string contextInfo)
        {
            GetLog().Info(msg, contextInfo);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="msg"></param>
        ///<param name="contextInfo"></param>
        public static void Debug(string msg, string contextInfo)
        {
            GetLog().Debug(msg, contextInfo);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="startTime"></param>
        ///<param name="contextInfo"></param>
        public static void Perf(DateTime startTime, string contextInfo)
        {
            GetLog().Perf(startTime, contextInfo);
        }

        ///<summary>
        ///</summary>
        ///<param name="msg"></param>
        ///<param name="contextInfo"></param>
        public static void Warn(string msg, string contextInfo)
        {
            GetLog().Warn(msg, contextInfo);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="msg"></param>
        ///<param name="contextInfo"></param>
        public static void Error(string msg, string contextInfo)
        {
            GetLog().Error(msg, contextInfo);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="ex"></param>
        ///<param name="msg"></param>
        ///<param name="contextInfo"></param>
        public static void Error(Exception ex, string msg, string contextInfo)
        {
            GetLog().Error(ex, msg, contextInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="funcCode"></param>
        /// <param name="msg"></param>
        /// <param name="contextInfo"></param>
        public static void Error(string moduleID, string funcCode, string msg, string contextInfo)
        {
            GetLog().Error(moduleID, funcCode, msg, contextInfo);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="msg"></param>
        ///<param name="contextInfo"></param>
        public static void Fatal(string msg, string contextInfo)
        {
            GetLog().Fatal(msg, contextInfo);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="ex"></param>
        ///<param name="msg"></param>
        ///<param name="contextInfo"></param>
        public static void Fatal(Exception ex, string msg, string contextInfo)
        {
            GetLog().Fatal(ex, msg, contextInfo);
        }
    }
}