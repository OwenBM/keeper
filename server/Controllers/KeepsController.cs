
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace keeper.Controllers;


[ApiController]
[Route("api/keeps")]

public class KeepController : ControllerBase
{
    private readonly KeepsService _KeepsService;
    private readonly Auth0Provider _auth;

    public KeepController(KeepsService KeepsService, Auth0Provider auth)
    {
        _KeepsService = KeepsService;
        _auth = auth;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep newKeep = _KeepsService.CreateKeep(keepData);
            return Ok(newKeep);
        }

        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
        try
        {
            List<Keep> keeps = _KeepsService.GetAllKeeps();
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{keepId}")]
    public async Task<ActionResult<Keep>> GetKeepById(int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _KeepsService.incrementViews(keepId, userInfo);
            return keep;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep newKeep = _KeepsService.EditKeep(keepData, keepId);
            return newKeep;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }
    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<String>> DeleteKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            string message = _KeepsService.DeleteKeep(keepId, userInfo);
            return message;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}