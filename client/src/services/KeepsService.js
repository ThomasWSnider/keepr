import { api } from "./AxiosService"
import { Keep } from "@/models/Keep";
import { AppState } from "@/AppState";
import { logger } from "@/utils/Logger";

class KeepsService{
  
  activateKeep(keepId){
    AppState.activeKeep = null
    let activeKept = null
    const activeKeep = AppState.keeps.find((keep) => keep.id == keepId)
    if (!activeKeep){
      activeKept = AppState.ActiveVaultKeeps.find((kept) => kept.id = keepId)
    }
    AppState.activeKeep = activeKeep ?? activeKept
  }

  async createNewKeep(keepData) {
    const response = await api.post("api/keeps", keepData)
    const newKeep = new Keep(response.data)
    AppState.keeps.unshift(newKeep)
  }
  
  async destroyKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`)
    const keepIndex = AppState.keeps.findIndex((keep) => keep.id == keepId)
    AppState.keeps.splice(keepIndex, 1)
  }

  async getAllKeeps(){
    const response = await api.get('api/keeps')
    const keeps = response.data.map((keepPOJO) => new Keep(keepPOJO))
    AppState.keeps = keeps
  }
}

export const keepsService = new KeepsService()