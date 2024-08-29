import { api } from "./AxiosService"
import { AppState } from "@/AppState"


class VaultKeepsService {
  async destroyVaultKeep(vaultKeepId) {
    const response = await api.delete(`api/vaultKeeps/${vaultKeepId}`)
    const keptIndex = AppState.keeps.findIndex((kept) => kept.id == vaultKeepId)
    AppState.keeps.splice(keptIndex, 1)
    return response.data
    }
  
  async saveKeep(vaultKeepData, keep) {
    vaultKeepData.keepId = keep.id
    await api.post(`api/vaultKeeps`, vaultKeepData)
    keep.keepCount++
    const vault = AppState.accountVaults.find((vault) => vault.id == vaultKeepData.vaultId)
    return `Keep Saved To ${vault.name}`
  }
}

export const vaultKeepsService = new VaultKeepsService()