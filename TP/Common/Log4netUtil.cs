using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace TP.Common
{
    public class Log4netUtil
    {

        static Log4netUtil()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Using :For(this) to get a default logger
        /// </summary>
        /// <param name="LoggedObject"></param>
        /// <returns></returns>
        public static ILog For(object LoggedObject)
        {
            if (LoggedObject != null)
                return For(LoggedObject.GetType());
            else
                return LogManager.GetLogger(string.Empty);
        }

        /// <summary>
        /// Using :For(this) to get a default logger
        /// </summary>
        /// <param name="ObjectType"></param>
        /// <returns></returns>
        public static ILog For(Type ObjectType)
        {
            if (ObjectType != null)
                return LogManager.GetLogger(ObjectType.Name);
            else
                return LogManager.GetLogger(string.Empty);
        }

        /// <summary>
        /// Using For("LoggerName") to get a specific logger which defined in log4net.xml
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns></returns>
        public static ILog For(String loggerName)
        {

            return LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Logging the previous method which call this to debug log.
        /// </summary>
        /// <param name="loggerName"></param>
        public static void LogLastCallMethodName(string loggerName)
        {

            LogLastCallMethodName(loggerName, 1);

        }


        /// <summary>
        /// Logging the previous method which call this to debug log.
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="seqOfMethods">the desc sequence of previous method which call this method</param>
        public static void LogLastCallMethodName(string loggerName, int seqOfMethods)
        {
            try
            {
                string logStr = String.Format("Method: {0} has been called", getCurrentCallMethodName(seqOfMethods + 2));

                LogManager.GetLogger(loggerName).Debug(logStr);
            }
            catch (Exception exception)
            {
                LogManager.GetLogger(String.Empty).Error(exception);
            }
        }

        protected static string getCurrentCallMethodName(int indexOfFrame)
        {

            var frames = new StackTrace().GetFrames();
            var currFrame = new StackTrace().GetFrames()[Math.Min(frames.Length, indexOfFrame)];
            var currMethod = currFrame.GetMethod();

            return String.Format("{0}.{1}", currMethod.DeclaringType, currMethod.Name);
        }

    }
}
