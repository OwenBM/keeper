




namespace keeper.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO
        vaults(
            name,
            description,
            is_private,
            img,
            creator_id
        )
        VALUES(
            @Name,
            @Description,
            @IsPrivate,
            @Img,
            @CreatorId
        );
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creator_id = accounts.id
        WHERE vaults.id = LAST_INSERT_ID();
        ;";

        Vault vault = _db.Query(sql, (Vault vault, Account account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).SingleOrDefault();
        return vault;
    }

    internal void DeleteVault(int vaultId)
    {
        string sql = @"
        DELETE FROM vaults WHERE id = @vaultId LIMIT 1
        ;";
        _db.Execute(sql, new { vaultId });
    }

    internal void EditVault(Vault originalVault)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        is_private = @IsPrivate
        WHERE id = @Id LIMIT 1;";
        _db.Execute(sql, originalVault);
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        string sql = @"
        SELECT * FROM vaults WHERE vaults.creator_id = @userId
        ;";
        List<Vault> vault = _db.Query<Vault>(sql, new { userId }).ToList();
        return vault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts on vaults.creator_id = accounts.id
        WHERE vaults.id = @VaultId;";

        Vault vault = _db.Query(sql, (Vault vault, Account account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { VaultId = vaultId }).FirstOrDefault();
        return vault;
    }
}