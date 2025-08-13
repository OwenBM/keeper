


using Microsoft.AspNetCore.Http;

namespace keeper.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _profilesRepository;
    public ProfilesService(ProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        List<Keep> keeps = _profilesRepository.GetKeepsByProfileId(profileId);
        return keeps;
    }

    internal Profile GetProfileById(string profileId)
    {
        Profile profile = _profilesRepository.GetProfileById(profileId);
        return profile;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        List<Vault> vaults = _profilesRepository.GetVaultsByProfileId(profileId);
        return vaults;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId, Account userInfo)
    {
        List<Vault> vaults = GetVaultsByProfileId(profileId);
        return vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userInfo?.Id);
    }
}