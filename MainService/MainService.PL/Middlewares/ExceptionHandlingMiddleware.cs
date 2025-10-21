using System.Net;
using MainService.AL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MainService.PL.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ProblemDetailsFactory _problemDetailsFactory;
   
    public ExceptionHandlingMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
    {
        _next = next;
        _problemDetailsFactory = problemDetailsFactory;
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
        var problem = new ProblemDetails
        {
            Instance = httpContext.Request.Path,
            Detail = exception?.Message
        };

        switch (exception)
        {
            case BadRequestException badRequestException:
                problem.Status = (int)HttpStatusCode.InternalServerError;
                foreach (var validationResult in badRequestException.Errors)
                {
                    problem.Extensions.Add(validationResult.Key, validationResult.Value);
                }
                break;
            case NotFoundException notFoundException:
                problem.Status = (int)HttpStatusCode.NotFound;
                break;
            default:
                problem.Status = (int)HttpStatusCode.InternalServerError;
                break;
        }

        if (_problemDetailsFactory != null)
        {
            var problemDetails = _problemDetailsFactory.CreateProblemDetails(httpContext, statusCode: problem.Status);
            problem.Title = problemDetails.Title;
            problem.Type = problemDetails.Type;
        }

        var result = new ObjectResult(problem)
        {
            StatusCode = problem.Status
        };

        await httpContext.Response.WriteAsJsonAsync(result);
    }
}