using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Api.Resources
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, 404, ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                await HandleExceptionAsync(httpContext, 400, ex.Message);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(httpContext, 500, "Error interno del servidor");
            }

        }
        private Task HandleExceptionAsync(HttpContext context, int statusCode , string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                ErrorList = new Dictionary<string, List<string>>() 
                {
                    {
                        "error",
                        new List<string>() { message }
                    } 
                }
            }.ToString());
        }
    }
}
