using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Simple opinionated logger
    /// </summary>
    public static class Logger
    {
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

            Trace.WriteLine($"DEBUG: {className}.{memberName}: {message}\r\n{sourceFilePath}:{sourceLineNumber}");
        }

        public static void Information(string message, string className = null, [CallerMemberName] string memberName = "")
        {
            Trace.TraceInformation($"{className}.{memberName}: {message}");
        }

        public static void Warning(string message, string className = null, [CallerMemberName] string memberName = "")
        {
            Trace.TraceWarning($"{className}.{memberName}: {message}");
        }

        public static void Error(
            string message,
            Exception exception = null,
            string className = null, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            Trace.TraceError(
                $"{className}.{memberName}: {message}\r\n{exception}:{exception.Message}\r\n{sourceFilePath}:{sourceLineNumber}");
        }
    }
}
