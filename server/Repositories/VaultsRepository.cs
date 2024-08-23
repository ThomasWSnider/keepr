using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultsRepository : IRepository<Vault>
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Vault Create(Vault vaultData)
  {
    string sql = @"
    INSERT INTO vaults(name, description, img, isPrivate, creatorId)
    VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);
    
    SELECT vaults.*, accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = LAST_INSERT_ID();";

    Vault newVault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, vaultData).FirstOrDefault();
    return newVault;
  }

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }

  public List<Vault> GetAll()
  {
    throw new NotImplementedException();
  }

  public Vault GetById(int vaultId)
  {
    string sql = @"
    SELECT vaults.*, accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = @vaultId;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, new { vaultId }).FirstOrDefault();
    return vault;
  }

  public Vault Update(Vault data)
  {
    throw new NotImplementedException();
  }

  private Vault JoinCreator(Vault vault, Profile profile)
  {
    vault.Creator = profile;
    return vault;
  }
}

