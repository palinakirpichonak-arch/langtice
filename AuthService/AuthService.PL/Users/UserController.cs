using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.PL.Users;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    
}