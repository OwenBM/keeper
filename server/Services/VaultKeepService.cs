

namespace keeper.Services;

public class VaultKeepService
{
    private readonly VaultKeepRepository _vaultKeepRepository;
    private readonly VaultsService _vaultsService;

    public VaultKeepService(VaultKeepRepository vaultKeepRepository, VaultsService vaultsService)
    {
        _vaultKeepRepository = vaultKeepRepository;
        _vaultsService = vaultsService;
    }

    internal string DeleteVaultKeep(int vaultKeepId, Account userInfo)
    {
        VaultKeep vaultKeep = getVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != userInfo.Id) throw new Exception("you cannot delete another user's vaultkeep!");
        _vaultKeepRepository.DeleteVaultKeep(vaultKeepId);
        string message = $"The vaultkeep with the id of '{vaultKeepId} has been deleted!";
        return message;
    }

    private VaultKeep getVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _vaultKeepRepository.getVaultKeepById(vaultKeepId);
        return vaultKeep;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        VaultKeep vaultKeep = _vaultKeepRepository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }



    internal List<VaultKeepKeeps> GetKeepsByVaultId(int vaultId, Account userInfo)
    {
        Vault vault = _vaultsService.GetVaultById(vaultId);
        if (vault.IsPrivate == true && vault.CreatorId != userInfo.Id) throw new Exception("You cannot add keeps to another user's vault");
        List<VaultKeepKeeps> vaultKeepKeeps = _vaultKeepRepository.GetKeepsByVaultId(vaultId);
        return vaultKeepKeeps;
    }
}