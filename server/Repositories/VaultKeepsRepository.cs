using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultKeepsRepository : IRepository<VaultKeep>
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  public VaultKeep Create(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO vaultKeeps(keepId, vaultId, creatorId)
    VALUES(@KeepId, @VaultId, @CreatorId);
    
    SELECT *
    FROM vaultKeeps
    WHERE vaultKeeps.id = LAST_INSERT_ID()";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
    return vaultKeep;
  }

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }

  public VaultKeep GetById(int id)
  {
    throw new NotImplementedException();
  }

  public VaultKeep Update(VaultKeep data)
  {
    throw new NotImplementedException();
  }
}

