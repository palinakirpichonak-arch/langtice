using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MainService.PL.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateParametersAttribute : ActionFilterAttribute
{
    private readonly string[] _parametersName;

    public ValidateParametersAttribute(params string[] parametersName)
    {
        _parametersName = parametersName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var pair in context.ActionArguments)
        {
            var name = pair.Key;
            var value = pair.Value;
            
            if(!_parametersName.Contains(name) && _parametersName != null)
                continue;
            
            string error = ValidateParameter(name, value)!;
            
            if (error != null)
            {
                context.Result = new BadRequestObjectResult(new ProblemDetails
                {
                    Title = "Invalid parameter",
                    Detail = error,
                    Status = StatusCodes.Status400BadRequest
                });
                return;
            }
        }
    }
    private static string? ValidateParameter(string name, object? value)
    {
        switch (value)
        {
            case null:
                return $"Parameter '{name}' cannot be null.";
            case Guid guid when guid == Guid.Empty:
                return $"Parameter '{name}' cannot be an empty GUID.";
            case int i when i < 0:
                return $"Parameter '{name}' must be positive integer.";
            case string s when string.IsNullOrWhiteSpace(s):
                return $"Parameter '{name}' cannot be empty or whitespace.";
            case DateTime dt when dt == default:
                return $"Parameter '{name}' cannot be default DateTime.";
            default:
                return null;
        }
    }
}