using System.Net;
using MainService.AL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MainService.PL.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
   
    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
           await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        
        ProblemDetails problem;

        switch (exception)
        {
            case DomainException domainEx:
                _logger.LogWarning(domainEx, "Handled domain exception");
                problem = _problemDetailsFactory.CreateProblemDetails(
                    httpContext,
                    statusCode: domainEx.StatusCode,
                    title: domainEx.GetType().Name.Replace("Exception", ""),
                    detail: domainEx.Message,
                    instance: httpContext.Request.Path);
                if (domainEx is BadRequestException badReq && badReq.Errors.Any())
                {
                    foreach (var err in badReq.Errors)
                        problem.Extensions[err.Key] = err.Value;
                }
                break;

            default:
                _logger.LogError(exception, "Unhandled exception");
                problem = _problemDetailsFactory.CreateProblemDetails(
                    httpContext,
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    title: "Internal Server Error",
                    detail: "An unexpected error occurred.",
                    instance: httpContext.Request.Path);
                break;
        }

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}