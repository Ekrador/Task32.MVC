﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using Task32.MVC.Models.Db;
using System.Security.Policy;

namespace Task32.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private void LogConsole(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private async Task LogFile(HttpContext context)
        {
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
            }
            catch(Exception e){ Console.WriteLine(e.Message); }
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }

        private async Task LogDB(HttpContext context, ILoggingRepository logRequests)
        {
           await logRequests.Log(new Request() { Url = $"http://{context.Request.Host.Value + context.Request.Path}" });
        }
        public async Task InvokeAsync(HttpContext context, ILoggingRepository logRequests)
        {
            LogConsole(context);
            await LogFile(context);
            await LogDB(context, logRequests);
            await _next.Invoke(context);
        }
    }
}
