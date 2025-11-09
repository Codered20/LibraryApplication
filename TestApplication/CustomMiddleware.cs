using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace TestApplication
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Request Path: {context.Request.Path}");

            await _next(context); // pass control to the next middleware

            Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
        }
    }
}
