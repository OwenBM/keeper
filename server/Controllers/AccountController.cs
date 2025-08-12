using System.Security.Cryptography;

namespace keeper.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;

  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    List<Vault> vaults = _vaultsService.GetMyVaults(userInfo.Id);
    return vaults;
  }

  [HttpPut]
  public async Task<ActionResult<Account>> EditAccount([FromBody] Account accountData)
  {
    Account UserInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    Account newAccount = _accountService.Edit(accountData, UserInfo.Id);
    return newAccount;
  }

}
