import { api } from "./AxiosService"
import { AppState } from "@/AppState"


class VaultKeepsService {
  async destroyVaultKeep(vaultKeepId) {
    const response = await api.delete(`api/vaultKeeps/${vaultKeepId}`)
    const keptIndex = AppState.activeVaultKeeps.findIndex((kept) => kept.id == vaultKeepId)
    AppState.activeVaultKeeps.splice(keptIndex, 1)
    return response.data
    }
  
  async saveKeep(vaultKeepData, keepId) {
    vaultKeepData.keepId = keepId
    await api.post(`api/vaultKeeps`, vaultKeepData)
    const vault = AppState.accountVaults.find((vault) => vault.id == vaultKeepData.vaultId)
    return `Keep Saved To ${vault.name}`
  }
}

export const vaultKeepsService = new VaultKeepsService()