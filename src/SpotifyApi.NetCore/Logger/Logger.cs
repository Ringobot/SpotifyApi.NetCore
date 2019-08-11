using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Simple Class Library Logger
    /// </summary>
    public static class Logger
    {
        private static ILoggerFactory _Factory = null;

        /// <summary>
        /// Instance of <see cref="ILoggerFactory"/>.
        /// </summary>
        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_Factory == null)
                {
                    _Factory = new LoggerFactory();
                }
                return _Factory;
            }
            set { _Factory = value; }
        }

        /// <summary>
        /// Create an instance of <see cref="ILogger"/> with the given category.
        /// </summary>
        /// <param name="category">The category description for this instance of the logger.</param>
        /// <returns>Instance of <see cref="ILogger"/></returns>
        public static ILogger CreateLogger(string category) => LoggerFactory.CreateLogger(category);

        private static string Category(string className, string memberName) => $"SpotifyApi.NetCore:{className}.{memberName}";

        /// <summary>
        /// Log a message at Debug level using a category name derived from className and Member name
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="className">Optional. The name of the class emiting the log message. e.g. `nameof(RestHttpClient)`.</param>
        /// <param name="memberName">The compiler will set the caller member name. Can be overidden.</param>
        /// <param name="sourceFilePath">The compiler will set the source file path. Can be overidden.</param>
        /// <param name="sourceLineNumber">The compiler will set the source line number. Can be overidden.</param>
        public static void Debug(
            string message,
            string className = null,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string sourceFilePath = null,
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            //SpotifyWebApi.Get: This is the message. c:\path\file.cs:10
            //.Get: This is the message
            //.: This is the message

            string fullMessage = $"{message}\r\n{sourceFilePath}:{sourceLineNumber}";
            string category = Category(className, memberName);
            Trace.WriteLine(fullMessage, category);
            CreateLogger(category).LogDebug(fullMessage);
        }

        /// <summary>
        /// Log a message at Information level using a category name derived from className and Member name
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="className">Optional. The name of the class emiting the log message. e.g. `nameof(RestHttpClient)`.</param>
        /// <param name="memberName">The compiler will set the caller member name. Can be overidden.</param>
        /// <param name="sourceFilePath">The compiler will set the source file path. Can be overidden.</param>
        /// <param name="sourceLineNumber">The compiler will set the source line number. Can be overidden.</param>
        public static void Information(string message, string className = null, [CallerMemberName] string memberName = "")
        {
            string category = Category(className, memberName);
            Trace.TraceInformation($"{category}: {message}");
            CreateLogger(category).LogInformation(message);
        }

        /// <summary>
        /// Log a message at Warning level using a category name derived from className and Member name
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="className">Optional. The name of the class emiting the log message. e.g. `nameof(RestHttpClient)`.</param>
        /// <param name="memberName">The compiler will set the caller member name. Can be overidden.</param>
        /// <param name="sourceFilePath">The compiler will set the source file path. Can be overidden.</param>
        /// <param name="sourceLineNumber">The compiler will set the source line number. Can be overidden.</param>
        public static void Warning(string message, string className = null, [CallerMemberName] string memberName = "")
        {
            string category = Category(className, memberName);
            Trace.TraceWarning($"{category}: {message}");
            CreateLogger(category).LogWarning(message);
        }

        /// <summary>
        /// Log a message at Error level using a category name derived from className and Member name
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="exception">Optional. An Exception to log.</param>
        /// <param name="className">Optional. The name of the class emiting the log message. e.g. `nameof(RestHttpClient)`.</param>
        /// <param name="memberName">The compiler will set the caller member name. Can be overidden.</param>
        /// <param name="sourceFilePath">The compiler will set the source file path. Can be overidden.</param>
        /// <param name="sourceLineNumber">The compiler will set the source line number. Can be overidden.</param>
        public static void Error(
            string message,
            Exception exception = null,
            string className = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            string category = Category(className, memberName);
            string fullMessage = $"{category}: {message}\r\n{sourceFilePath}:{sourceLineNumber}";

            if (exception == null)
            {
                Trace.TraceError(fullMessage);
                CreateLogger(category).LogError(message);
            }
            else
            {
                Trace.TraceError(fullMessage);
                CreateLogger(category).LogError(exception, message);
            }
        }
    }
}
