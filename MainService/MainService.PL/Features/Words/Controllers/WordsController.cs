using MainService.AL.Words.DTO;
using MainService.AL.Words.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Words.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _wordService;

        public WordsController(IWordService wordService)
        {
            _wordService = wordService;
        }
        
    }
}