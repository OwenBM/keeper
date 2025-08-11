

namespace keeper.Services;

public class VaultsService
{
    private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
        _vaultsRepository = vaultsRepository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _vaultsRepository.CreateVault(vaultData);
        return vault;
    }

    internal string DeleteVault(int vaultId, Account userInfo)
    {
        Vault vault = GetVaultById(vaultId);
        if (vault.CreatorId != userInfo.Id) throw new Exception("you cannot delete another user's vault!");
        _vaultsRepository.DeleteVault(vaultId);
        return $"The vault with the id of '{vaultId}' has been deleted!";
    }

    internal Vault EditVault(Vault vaultData, int vaultId)
    {
        Vault originalVault = GetVaultById(vaultId);
        if (originalVault.CreatorId != vaultData.CreatorId) throw new Exception("you cannot edit another user's vault!");
        originalVault.Name = vaultData.Name ?? originalVault.Name;
        originalVault.Description = vaultData.Description ?? originalVault.Description;
        originalVault.Img = vaultData.Img ?? originalVault.Img;
        originalVault.IsPrivate = vaultData.IsPrivate ?? originalVault.IsPrivate;

        _vaultsRepository.EditVault(originalVault);
        return originalVault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        Vault vault = _vaultsRepository.GetVaultById(vaultId);
        if (vault == null) throw new Exception($"Invalid id: {vaultId}");
        return vault;
    }
}