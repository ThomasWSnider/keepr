import { Vault } from "@/models/Vault"
import { api } from "./AxiosService"
import { AppState } from "@/AppState"


class VaultsService{
  async getAccountVaults(){
    const response = await api.get("account/vaults")
    const accountVaults = response.data.map((vaultPOJO) => new Vault(vaultPOJO))
    AppState.accountVaults = accountVaults
  }
}

export const vaultsService = new VaultsService()