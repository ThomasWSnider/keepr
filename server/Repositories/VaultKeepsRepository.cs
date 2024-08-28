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

  public void Delete(int vaultKeepId)
  {
    string sql = @"
    DELETE FROM vaultKeeps
    WHERE id = @vaultKeepId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { vaultKeepId });
    if (rowsAffected < 1) throw new Exception("Unable to delete Vault-Keep relationship");
    if (rowsAffected > 1) throw new Exception("Deleted more than one Vault-Keep relationship");
  }

  public VaultKeep GetById(int vaultKeepId)
  {
    string sql = @"
    SELECT * FROM vaultKeeps
    WHERE id = @vaultKeepId;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
    return vaultKeep;
  }

  internal List<Kept> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT vaultKeeps.*, keeps.*, accounts.*
    FROM vaultKeeps
    JOIN keeps ON keeps.id = vaultKeeps.keepId
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE vaultKeeps.vaultId = @vaultId;";

    List<Kept> vaultKeeps = _db.Query<VaultKeep, Kept, Profile, Kept>(sql, (vaultKeep, kept, profile) =>
    {
      kept.VaultKeepId = vaultKeep.Id;
      kept.VaultKeepCreatorId = vaultKeep.CreatorId;
      kept.Creator = profile;
      return kept;
    }, new { vaultId }).ToList();
    return vaultKeeps;
  }

  public VaultKeep Update(VaultKeep data)
  {
    throw new NotImplementedException();
  }

}

