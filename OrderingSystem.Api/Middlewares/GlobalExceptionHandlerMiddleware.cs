using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderingSystem.Api.ViewModels;

namespace OrderingSystem.Api.Middlewares
{
  public class GlobalExceptionHandlerMiddleware : IMiddleware
  {
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
      ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (Exception exception)
      {
        _logger.LogError($"Unexpected error: {exception}");
        await HandleExceptionAsync(context, exception);
      }
    }

    private static Task HandleExceptionAsync(
      HttpContext context,
      Exception exception)
    {
      const int statusCode = StatusCodes.Status500InternalServerError;

      var json = JsonConvert.SerializeObject(new ResultViewModel
      {
        Success = false,
        Message = "An error occurred while processing your request",
        Data = exception
      });

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/json";

      return context.Response.WriteAsync(json);
    }
  }
}