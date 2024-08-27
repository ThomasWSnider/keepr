

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
    Vault vaultToConfirm = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
    if (vaultToConfirm.CreatorId != vaultKeepData.CreatorId) throw new Exception("You cannot add a keept to a vault you did not create");
    VaultKeep vaultKeep = _repository.Create(vaultKeepData);
    return vaultKeep;
  }

  internal string DeleteVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != userId) throw new Exception("You cannot delete a keep-vault relationship you did not create");
    _repository.Delete(vaultKeepId);
    return "Vault-keep relationship successfully deleted";
  }

  internal List<Kept> GetKeepsByVaultId(int vaultId, string userId)
  {
    _vaultsService.GetVaultById(vaultId, userId);
    List<Kept> keptKeeps = _repository.GetKeepsByVaultId(vaultId);
    return keptKeeps;
  }

  private VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetById(vaultKeepId) ?? throw new Exception($"Now Vault Keep found with id of {vaultKeepId}");
    return vaultKeep;
  }
}
