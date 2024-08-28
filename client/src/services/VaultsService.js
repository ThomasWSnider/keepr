import { Vault } from "@/models/Vault"
import { api } from "./AxiosService"
import { AppState } from "@/AppState"
import { Kept } from "@/models/VaultKeep"


class VaultsService{
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
    AppState.ActiveVaultKeeps = activeVaultKeeps
  }
}

export const vaultsService = new VaultsService()