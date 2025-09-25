using MainService.AL.Words.DTO;
using Microsoft.AspNetCore.Mvc;
namespace MainService.PL.Words.Controllers;

[Route("userwords")]
[ApiController]
public class UserWordsController : ControllerBase
{
    private readonly IUserWordService _service;

    public UserWordsController(IUserWordService service)
    {
        _service = service;
    }

   
}