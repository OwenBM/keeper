using Microsoft.AspNetCore.Http.HttpResults;

namespace keeper.Controllers;

[ApiController]
[Route("api/profiles")]

public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    public ProfilesController(ProfilesService profilesService)
    {
        _profilesService = profilesService;
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
    public ActionResult<List<Vault>> GetVaultsByProfileId(string profileId)
    {
        try
        {
            List<Vault> vaults = _profilesService.GetVaultsByProfileId(profileId);
            return vaults;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}