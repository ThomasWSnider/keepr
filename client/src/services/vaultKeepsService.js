import { logger } from "@/utils/Logger"


class VaultKeepsService {
  destroyVaultKeep(vaultKeepId) {
    logger.log(`I understand that you are looking for a kept rather than a keep with the id of ${vaultKeepId}`)
  }


}

export const vaultKeepsService = new VaultKeepsService()