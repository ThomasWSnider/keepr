using keepr.Interfaces;

namespace keepr.Repositories;

public class KeepsRepository : IRepository<Keep>
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Keep Create(Keep keepData)
  {
    string sql = @"
    INSERT INTO 
    keeps(name, description, img, creatorId)
    VALUES(@Name, @Description, @Img, @CreatorId);
    
    SELECT keeps.*, accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    
    WHERE keeps.id = LAST_INSERT_ID();";

    Keep newKeep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, keepData).FirstOrDefault();
    return newKeep;
  }

  public void Delete(int keepId)
  {
    string sql = @"
    DELETE FROM keeps
    WHERE id = @keepId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { keepId });
    if (rowsAffected < 1) throw new Exception($"Could not delete keep with id of {keepId}");
    if (rowsAffected > 1) throw new Exception("Deleted more than one keep");
  }

  public List<Keep> GetAll()
  {
    string sql = @"
    SELECT keeps.*, accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    ORDER BY keeps.createdAt DESC;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, JoinCreator).ToList();
    return keeps;
  }

  public Keep GetById(int keepId)
  {
    string sql = @"
    SELECT keeps.*, accounts.*
    FROM keeps
    JOIN accounts On accounts.id = keeps.creatorId
    WHERE keeps.id = @keepId;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, new { keepId }).FirstOrDefault();
    return keep;
  }

  internal List<Keep> GetProfileKeeps(string profileId)
  {
    string sql = @"
      SELECT keeps.*, accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.creatorId = @profileId;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { profileId }).ToList();
    return keeps;
  }

  public Keep Update(Keep keepToUpdate)
  {
    string sql = @"
    UPDATE keeps
    SET name = @Name, description = @Description, img = @Img, views = @views
    WHERE id = @Id LIMIT 1;
    
    SELECT keeps.*, accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = @Id";

    Keep updatedKeep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, keepToUpdate).FirstOrDefault();
    return updatedKeep;
  }

  private Keep JoinCreator(Keep keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }
}

