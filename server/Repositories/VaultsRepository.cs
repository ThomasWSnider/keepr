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

  public void Delete(int vaultId)
  {
    string sql = @"
    DELETE FROM vaults 
    WHERE id = @vaultId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { vaultId });
    if (rowsAffected > 1) throw new Exception($"Was unable to delete vault with id of {vaultId}");
    if (rowsAffected < 1) throw new Exception($"Deleted more than one vault");
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

  public Vault Update(Vault vaultToUpdate)
  {
    string sql = @"
    UPDATE vaults
    SET name = @Name, description = @Description, img = @Img, isPrivate = @IsPrivate
    WHERE id = @Id LIMIT 1;
    
    SELECT vaults.*, accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = @Id;";

    Vault updatedVault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, vaultToUpdate).FirstOrDefault();
    return updatedVault;
  }

  private Vault JoinCreator(Vault vault, Profile profile)
  {
    vault.Creator = profile;
    return vault;
  }
}

