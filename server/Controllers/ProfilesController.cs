using Microsoft.AspNetCore.Http.HttpResults;

namespace keeper.Controllers;

[ApiController]
[Route("api/profiles")]

public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    private readonly Auth0Provider _auth;
    public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
    {
        _profilesService = profilesService;
        _auth = auth;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Profile profile = _profilesService.GetProfileById(profileId);
            return profile;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
        try
        {
            List<Keep> keeps = _profilesService.GetKeepsByProfileId(profileId);
            return keeps;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _profilesService.GetVaultsByProfileId(profileId, userInfo);
            return vaults;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}