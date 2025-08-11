using System.Threading.Tasks;

namespace keeper.Controllers;

[ApiController]
[Route("api/vaults")]

public class VaultsController : ControllerBase
{
    private readonly Auth0Provider _auth;
    private readonly VaultsService _vaultsService;
    private readonly VaultKeepService _vaultKeepService;

    public VaultsController(Auth0Provider auth, VaultsService vaultsService, VaultKeepService vaultKeepService)
    {
        _auth = auth;
        _vaultsService = vaultsService;
        _vaultKeepService = vaultKeepService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public ActionResult<Vault> GetVaultById(int vaultId)
    {
        try
        {
            Vault vault = _vaultsService.GetVaultById(vaultId);
            return vault;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault newVault = _vaultsService.EditVault(vaultData, vaultId);
            return newVault;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }
    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<String>> DeleteVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultsService.DeleteVault(vaultId, userInfo);
            return message;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepKeeps>>> GetKeepsByVault(int vaultId)
    {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        List<VaultKeepKeeps> vaultKeepKeeps = _vaultKeepService.GetKeepsByVaultId(vaultId, userInfo);
        return vaultKeepKeeps;
    }
}