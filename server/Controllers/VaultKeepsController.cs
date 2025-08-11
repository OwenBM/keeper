using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace keeper.Controllers;

[ApiController]
[Route("api/vaultkeeps")]

public class VaultKeepsController : ControllerBase
{
    private readonly Auth0Provider _auth;
    private readonly VaultKeepService _vaultKeepService;

    public VaultKeepsController(Auth0Provider auth, VaultKeepService vaultKeepService)
    {
        _auth = auth;
        _vaultKeepService = vaultKeepService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep VaultKeepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            VaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _vaultKeepService.CreateVaultKeep(VaultKeepData);
            return Ok(vaultKeep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<String>> DeleteVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultKeepService.DeleteVaultKeep(vaultKeepId, userInfo);
            return message;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}