using MainService.AL.Features.Lessons.DTO;
using MainService.AL.Features.Lessons.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using MainService.AL.Features.Lessons.Services;
using MongoDB.Bson;

namespace MainService.PL.Features.Lessons.Controllers;

[Route("tests")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    // GET /tests/{id} - Get a single test for editing for example
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var test = await _testService.GetAllAsync(cancellationToken);
        if (test == null) return NotFound();
        return Ok(test);
    }
    
    // GET /tests/{id} - Get a single test for editing for example
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestById(string id, CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(id, cancellationToken);
        if (test == null) return NotFound();
        return Ok(test);
    }
    
    // GET /tests/{id} - Get an active test to user
    [HttpGet("{id}/active")]
    public async Task<IActionResult> GetActiveTestById(string id, CancellationToken cancellationToken)
    {
        var test = await _testService.GetActiveTest(id, cancellationToken);
        if (test == null) return NotFound();
        return Ok(test);
    }


    // POST /tests - Create a new test
    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] TestDto dto, CancellationToken cancellationToken)
    {
        if (dto == null) return BadRequest();

        var test = await _testService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetTestById), new { id = test.Id }, test);
    }

    // PUT /tests/{id} - Update an existing test
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTest(string id, [FromBody] TestDto dto, CancellationToken cancellationToken)
    {
        if (dto == null) return BadRequest();

        var test = await _testService.UpdateAsync(id, dto, cancellationToken);
        return Ok(test);
    }

    // DELETE /tests/{id} - Delete a test
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTest(string id, CancellationToken cancellationToken)
    {
        await _testService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    // POST /tests/{id}/submit - Submit answers for a test
    [HttpPost("{id}/submit")]
    public async Task<IActionResult> SubmitTest(string id, [FromBody] UserTestDto userTest, CancellationToken cancellationToken)
    {
        var result = await _testService.CheckTest(id, userTest, cancellationToken);
        return Ok(new { Correct = result.correct, Mistakes = result.mistake });
    }
}
