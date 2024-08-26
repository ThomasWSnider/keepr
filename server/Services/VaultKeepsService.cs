

namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    VaultKeep vaultKeep = _repository.Create(vaultKeepData);
    return vaultKeep;
  }

  internal List<VaultKeep> GetKeepsByVaultId(int vaultId, string userId)
  {
    Vault vaultToValidate = _vaultsService.GetVaultById(vaultId, userId);
    List<VaultKeep> vaultKeeps = _repository.GetKeepsByVaultId(vaultId);
    return vaultKeeps;
  }
}
