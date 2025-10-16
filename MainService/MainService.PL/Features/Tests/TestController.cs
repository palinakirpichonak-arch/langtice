using MainService.AL.Features.Tests.DTO.Request;
using MainService.AL.Features.Tests.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Tests;

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

    [HttpGet]
    public async Task<IActionResult> GetAllTests(CancellationToken cancellationToken)
    {
        var test = await _testService.GetAllAsync(cancellationToken);
        if (test == null) return NotFound();
        return Ok(test);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestById(string id, CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(id, cancellationToken);
        if (test == null) return NotFound();
        return Ok(test);
    }
    
    [HttpGet("{id}/active")]
    public async Task<IActionResult> GetActiveTestById(string id, CancellationToken cancellationToken)
    {
        var test = await _testService.GetActiveTest(id, cancellationToken);
        if (test == null) return NotFound();
        return Ok(test);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] TestDto dto, CancellationToken cancellationToken)
    {
        if (dto == null) return BadRequest();

        var test = await _testService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetTestById), new { id = test.Id }, test);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTest(string id, [FromBody] TestDto dto, CancellationToken cancellationToken)
    {
        if (dto == null) return BadRequest();

        var test = await _testService.UpdateAsync(id, dto, cancellationToken);
        return Ok(test);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTest(string id, CancellationToken cancellationToken)
    {
        await _testService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/submit")]
    public async Task<IActionResult> SubmitTest(string id, [FromBody] UserAnswerDto userTest, CancellationToken cancellationToken)
    {
        var result = await _testService.CheckTest(id, userTest, cancellationToken);
        return Ok(new { Correct = result.correct, Mistakes = result.mistake });
    }
}
