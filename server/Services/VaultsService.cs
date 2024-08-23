

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
    if (vault.IsPrivate && vault.CreatorId != userId) throw new Exception($"No Vault found with the id of {vaultId}");
    return vault;
  }
}
