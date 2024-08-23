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

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }

  public List<Keep> GetAll()
  {
    throw new NotImplementedException();
  }

  public Keep GetById(int id)
  {
    throw new NotImplementedException();
  }

  public Keep Update(Keep data)
  {
    throw new NotImplementedException();
  }

  private Keep JoinCreator(Keep keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }
}

