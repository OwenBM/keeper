using System.Data.Common;

namespace keeper.Repositories;

public class VaultKeepRepository
{
    private readonly IDbConnection _db;

    public VaultKeepRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO 
        vaultkeeps(
        keep_id,
        vault_id,
        creator_id
        )
        VALUES(
        @KeepId,
        @VaultId,
        @CreatorId
        );
        SELECT * FROM vaultkeeps
        WHERE id = LAST_INSERT_ID();
        ;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).SingleOrDefault();
        return vaultKeep;
    }

    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = @"
        DELETE FROM vaultkeeps WHERE vaultkeeps.id = @vaultKeepId
        ;";
        _db.Execute(sql, new { vaultKeepId });
    }

    internal List<VaultKeepKeeps> GetKeepsByVaultId(int vaultId)
    {
        string sql = @"
        SELECT 
        vaultkeeps.*,
        keeps.*,
        accounts.*
        FROM vaultkeeps
        JOIN keeps ON keeps.id = vaultkeeps.keep_id
        JOIN accounts on accounts.id = keeps.creator_id
        WHERE vaultkeeps.vault_id = @vaultId
        ;";

        List<VaultKeepKeeps> keeps = _db.Query(sql, (VaultKeep vaultKeep, VaultKeepKeeps keep, Account account) =>
        {
            keep.VaultKeepId = vaultKeep.Id;
            keep.Creator = account;
            return keep;
        }, new { vaultId }).ToList();
        return keeps;

    }

    internal VaultKeep getVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT * FROM vaultkeeps WHERE vaultkeeps.id = @vaultKeepId
        ;";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();
        return vaultKeep;
    }

    internal void AddKeepCount(VaultKeep vaultKeep)
    {
        string sql = "UPDATE keeps SET kept = kept + 1 WHERE id = @KeepId;";
        _db.Execute(sql, vaultKeep);
    }
    internal void DecreaseKeepCount(VaultKeep vaultKeep)
    {
        string sql = "UPDATE keeps SET kept = kept - 1 WHERE id = @KeepId;";
        _db.Execute(sql, vaultKeep);
    }

}