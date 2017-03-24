using log4net;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Bill2Pay.ExceptionLogger
{
    /// <summary>
    /// Logger class used to define various logging methods in application.
    /// ALL
    /// DEBUG
    /// INFO
    /// WARN
    /// ERROR
    /// FATAL
    /// OFF
    /// </summary>
    public sealed class Logger : ILogger
    {
        #region Datamembers
        /// <summary>
        /// ILog interface of log4Net framework used for logging message in application.
        /// </summary>
        private static ILog log = null;

        #endregion

        #region Class Initializer

        /// <summary>
        /// Private logger instance
        /// </summary>
        private static readonly Lazy<Logger> logger = new Lazy<Logger>(() => new Logger());

        /// <summary>
        /// Singleton public instance for logger
        /// </summary>
        public static Logger LogInstance { get { return logger.Value; } }

        /// <summary>
        /// Default constructor of logger class.
        /// </summary>
        public Logger()
        {
            log4net.Config.BasicConfigurator.Configure();
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.GlobalContext.Properties["host"] = Environment.MachineName;
        }
        #endregion

        #region ILogger Members

        #region Trace
        /// <summary>
        /// Log the name of the method when execution starts.
        /// https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callermembernameattribute(v=vs.110).aspx
        /// </summary>
        /// <param name="message">Custom message</param>
        /// <param name="memberName">Method Name</param>
        /// <param name="sourceFilePath">File Pahth</param>
        /// <param name="sourceLineNumber">Line Number</param>
        public void LogTrace(string message, [CallerMemberName] string memberName = "",
         [CallerFilePath] string sourceFilePath = "",
         [CallerLineNumber] int sourceLineNumber = 0)
        {
            string formattedMessage = string.Format(CultureInfo.InvariantCulture, "{0} - Method: '{1}' File: '{2}' Line: '{3}'", message, memberName, sourceFilePath, sourceLineNumber);
            LogDebug(formattedMessage);
            //Leaving 
            //Entering
#if DEBUG
            Debug.WriteLine(formattedMessage);
#endif

        }

        #endregion

        #region Fatal

        /// <summary>
        /// Log system exception.
        /// </summary>
        /// <param name="exception">Exception object.</param>
        public void LogException(Exception exception)
        {
            if (exception != null && !string.IsNullOrEmpty(exception.Message))
            {
                log.Fatal(exception.Message, exception);
#if DEBUG
                Debug.WriteLine(exception.Message);
#endif
            }
        }

        /// <summary>
        /// Logs system exception with custom error message.
        /// </summary>
        /// <param name="message">Custom error message.</param>
        /// <param name="exception">Exception object.</param>
        public void LogException(string message, Exception exception)
        {
            if (exception != null && !string.IsNullOrEmpty(exception.Message))
            {
                string formattedMessage = string.Format(CultureInfo.InvariantCulture, "{0} - The exception was: {1}", message, exception.Message);
                log.Fatal(formattedMessage, exception);

#if DEBUG
                Debug.WriteLine(formattedMessage);
#endif
            }
        }

        /// <summary>
        /// Logs system exception with formatted custom error message.
        /// </summary>
        /// <param name="exception">Exception </param>
        /// <param name="format">Message Format</param>
        /// <param name="args">Arguments</param>
        public void LogException(string format, Exception exception, params object[] args)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            LogException(message, exception);
        }


        /// <summary>
        ///  Log system exception..
        /// </summary>
        /// <param name="message">Custom error message.</param>
        public void LogFatal(string message)
        {
            log.Fatal(message);
#if DEBUG
            Debug.WriteLine(message);
#endif
        }

        /// <summary>
        /// Log formatted system exception.
        /// </summary>
        /// <param name="format">Format of Message</param>
        /// <param name="args">arguments for format</param>
        public void LogFatal(string format, params object[] args)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            LogFatal(message);
        }
        #endregion

        #region Error
        /// <summary>
        /// Log custom error message based application logic and validations.
        /// </summary>
        /// <param name="message">Custom error message.</param>
        public void LogError(string message)
        {
            log.Error(message);
#if DEBUG
            Debug.WriteLine(message);
#endif
        }

        /// <summary>
        /// Log formatted custom error message based on application logic and validations.
        /// </summary>
        /// <param name="format">Format of Message</param>
        /// <param name="args">arguments for format</param>
        public void LogError(string format, params object[] args)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            LogError(message);
        }
        #endregion

        #region Warning
        /// <summary>
        /// Log custom warning message based application logic and validations.
        /// </summary>
        /// <param name="message">Custom warning message.</param>
        public void LogWarning(string message)
        {
            log.Warn(message);
#if DEBUG
            Debug.WriteLine(message);
#endif
        }

        /// <summary>
        /// Log formatted custom warning message based application logic and validations.
        /// </summary>
        /// <param name="format">Format of Message</param>
        /// <param name="args">arguments for format</param>
        public void LogWarning(string format, params object[] args)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            LogWarning(message);
        }
        #endregion

        #region Info
        /// <summary>
        /// Logs custom information and provides status.
        /// </summary>
        /// <param name="message">Custom information message.</param>
        public void LogInfo(string message)
        {
            log.Info(message);
#if DEBUG
            Debug.WriteLine(message);
#endif
        }

        /// <summary>
        /// Log formatted custom information and provides status.
        /// </summary>
        /// <param name="format">Format of Message</param>
        /// <param name="args">arguments for format</param>
        public void LogInfo(string format, params object[] args)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            LogInfo(message);
        }
        #endregion

        #region Debug
        /// <summary>
        /// Logs debug information and provides status.
        /// </summary>
        /// <param name="message">Custom information message.</param>
        public void LogDebug(string message)
        {
            log.Debug(message);
#if DEBUG
            Debug.WriteLine(message);
#endif
        }

        /// <summary>
        /// Log formatted debug information and provides status.
        /// </summary>
        /// <param name="format">Format of Message</param>
        /// <param name="args">arguments for format</param>
        public void LogDebug(string format, params object[] args)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            LogDebug(message);
        }
        #endregion

        #endregion

    }
}
