





namespace keeper.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO
        keeps(
            name,
            description,
            img,
            creator_id
        )
        VALUES(
            @Name,
            @Description,
            @Img,
            @CreatorId
        );
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        WHERE keeps.id = LAST_INSERT_ID();
        ;";

        Keep newKeep = _db.Query(sql, (Keep keep, Account account) =>
        {
            keep.Creator = account;
            return keep;
        }, keepData).SingleOrDefault();
        return newKeep;

    }

    internal void DeleteKeep(int KeepId)
    {
        string sql = "DELETE FROM keeps WHERE id = @KeepId LIMIT 1;";

        _db.Execute(sql, new { KeepId });
    }

    internal void EditKeep(Keep originalKeep)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img
        WHERE id = @Id LIMIT 1;";
        _db.Execute(sql, originalKeep);
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        ;";

        List<Keep> keeps = _db.Query(sql, (Keep keep, Account account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        WHERE keeps.id = @KeepId
        ;";

        Keep keep = _db.Query(sql, (Keep keep, Account account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { KeepId = keepId }).SingleOrDefault();
        return keep;
    }

    internal void UpdateViews(Keep keep)
    {
        string sql = @"UPDATE keeps SET views = @Views WHERE id = @Id LIMIT 1;";

        _db.Execute(sql, keep);
    }
}