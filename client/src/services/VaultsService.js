import { Vault } from "@/models/Vault"
import { api } from "./AxiosService"
import { AppState } from "@/AppState"
import { Kept } from "@/models/VaultKeep"


class VaultsService{
  async CreateNewVault(vaultData) {
    const response = await api.post(`api/vaults`, vaultData)
    const newVault = new Vault(response.data)
    AppState.accountVaults.push(newVault)
    return newVault
  }
  async destroyVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`)
    const vaultIndex = AppState.accountVaults.findIndex((vault) => vault.id == vaultId)
    AppState.accountVaults.splice(vaultIndex, 1)
  }
  async getAccountVaults(){
    const response = await api.get("account/vaults")
    const accountVaults = response.data.map((vaultPOJO) => new Vault(vaultPOJO))
    AppState.accountVaults = accountVaults
  }
  async getVaultById(vaultId) {
    AppState.activeVault = null
    const response = await api.get(`api/vaults/${vaultId}`)
    const activeVault = new Vault(response.data)
    AppState.activeVault = activeVault
  }
  async getVaultKeepsByVaultId(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    const activeVaultKeeps = response.data.map((kept) => new Kept(kept))
    AppState.activeVaultKeeps = activeVaultKeeps
  }
}

export const vaultsService = new VaultsService()