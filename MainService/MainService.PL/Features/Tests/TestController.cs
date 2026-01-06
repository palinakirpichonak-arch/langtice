using MainService.AL.Features.Tests.DTO.Request;
using MainService.AL.Features.Tests.Services;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Tests;

[Authorize]
[Tags("Tests")]
[Route("tests")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }
    
    [Authorize(Roles = "User, Admin")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTests(CancellationToken cancellationToken)
    {
        var test = await _testService.GetAllAsync(cancellationToken);
        return Ok(test);
    }
    
    [Authorize(Roles = "User, Admin")]
    [HttpGet("{id}")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTestById(string id, CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(id, cancellationToken);
        return Ok(test);
    }

    [Authorize(Roles = "User, Admin")]
    [HttpGet("{id}/active")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActiveTestById(string id, CancellationToken cancellationToken)
    {
        var test = await _testService.GetActiveTest(id, cancellationToken);
        return Ok(test);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTest([FromBody] TestDto dto, CancellationToken cancellationToken)
    {
        var test = await _testService.CreateAsync(dto, cancellationToken);
        return Ok(test);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTest(string id, [FromBody] TestDto dto, CancellationToken cancellationToken)
    {
        var test = await _testService.UpdateAsync(id, dto, cancellationToken);
        return Ok(test);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task DeleteTest(string id, CancellationToken cancellationToken)
    {
        await _testService.DeleteAsync(id, cancellationToken);
    }

    [HttpPost("{id}/submit")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmitTest(
    string id,
    [FromBody] UserTestSubmissionDto submission,
    CancellationToken cancellationToken)
{
    var result = await _testService.CheckTest(id, submission, cancellationToken);
    return Ok(new {result});
}
}