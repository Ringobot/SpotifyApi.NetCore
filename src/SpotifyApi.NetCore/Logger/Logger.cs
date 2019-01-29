using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Simple opinionated logger
    /// </summary>
    public static class Logger
    {
        private static ILoggerFactory _Factory = null;

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
        public static ILogger CreateLogger(string category) => LoggerFactory.CreateLogger(category);

        private static string Category(string className, string memberName) => $"SpotifyApi.NetCore.{className}.{memberName}";

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

        public static void Information(string message, string className = null, [CallerMemberName] string memberName = "")
        {
            string category = Category(className, memberName);
            Trace.TraceInformation($"{category}: {message}");
            CreateLogger(category).LogInformation(message);
        }

        public static void Warning(string message, string className = null, [CallerMemberName] string memberName = "")
        {
            string category = Category(className, memberName);
            Trace.TraceWarning($"{category}: {message}");
            CreateLogger(category).LogWarning(message);
        }

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
