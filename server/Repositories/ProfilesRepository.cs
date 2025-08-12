


namespace keeper.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;
    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        string sql = @"
        SELECT * FROM keeps WHERE keeps.creator_id = @profileId
        ;";
        List<Keep> keeps = _db.Query<Keep>(sql, new { profileId }).ToList();
        return keeps;
    }

    internal Profile GetProfileById(string profileId)
    {
        string sql = @"
        SELECT * FROM accounts WHERE accounts.id = @profileId
        ;";

        Profile profile = _db.Query<Profile>(sql, new { profileId }).SingleOrDefault();
        return profile;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        string sql = @"
        SELECT * 
        FROM vaults 
        WHERE vaults.creator_id = @profileId AND is_private = false
        ;";
        List<Vault> vaults = _db.Query<Vault>(sql, new { profileId }).ToList();
        return vaults;
    }
}