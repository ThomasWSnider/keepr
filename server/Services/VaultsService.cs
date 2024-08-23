

namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repository;

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault newVault = _repository.Create(vaultData);
    return newVault;
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _repository.GetById(vaultId) ?? throw new Exception($"No Vault found with the id of {vaultId}");
    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception($"No Vault found with the id of {vaultId}");
    return vault;
  }

  internal Vault UpdateVault(int vaultId, Vault vaultData, string userId)
  {
    Vault vaultToUpdate = GetVaultById(vaultId, userId);
    if (vaultToUpdate.CreatorId != userId) throw new Exception("You can not update a Vault you did not create!");

    vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
    vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
    vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
    vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

    Vault updatedVault = _repository.Update(vaultToUpdate);
    return updatedVault;
  }
}
