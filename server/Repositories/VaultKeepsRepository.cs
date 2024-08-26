using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultKeepsRepository : IRepository<VaultKeep>
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  public VaultKeep Create(VaultKeep data)
  {
    throw new NotImplementedException();
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

