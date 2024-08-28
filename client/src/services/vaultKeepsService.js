import { logger } from "@/utils/Logger"
import { api } from "./AxiosService"
import { AppState } from "@/AppState"


class VaultKeepsService {
  destroyVaultKeep(vaultKeepId) {
    logger.log(`I understand that you are looking for a kept rather than a keep with the id of ${vaultKeepId}`)
  }
  
  async saveKeep(vaultKeepData) {
    await api.post(`api/vaultKeeps`, vaultKeepData)
    const vault = AppState.accountVaults.find((vault) => vault.id == vaultKeepData.vaultId)
    return `Keep Saved To ${vault.name}`
  }
}

export const vaultKeepsService = new VaultKeepsService()