using SaturnApi.Business.Interfaces;
using SaturnApi.Business.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SaturnApi.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,
            ILogExceptionRepository logExceptionRepository)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, logExceptionRepository);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception,
            ILogExceptionRepository logExceptionRepository)
        {
            //exception.Ship(context);
            await logExceptionRepository.Add(new LogException()
            {
                TimeStamp = DateTime.Now,
                IpAddress = context.Connection.RemoteIpAddress.ToString(),
                Message = exception.Message,
                RequestId = context.TraceIdentifier,
                RequestPath = context.Request.Path,
                Source = exception.Source,
                StackTrace = exception.StackTrace,
                Type = exception.GetType().ToString(),
                User = context.User.GetUserId()
            });

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
