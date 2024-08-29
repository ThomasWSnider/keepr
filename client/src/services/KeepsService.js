import { api } from "./AxiosService"
import { Keep } from "@/models/Keep";
import { AppState } from "@/AppState";

class KeepsService{
  
  async activateKeep(keep, accountId){
    AppState.activeKeep = null
    await api.get(`api/keeps/${keep.id}`)
    if (accountId == keep.creatorId){
      AppState.activeKeep = keep
      return
    }
    keep.views++
    AppState.activeKeep = keep
  }

  async createNewKeep(keepData) {
    const response = await api.post("api/keeps", keepData)
    const newKeep = new Keep(response.data)
    // @ts-ignore
    AppState.keeps.unshift(newKeep)
  }
  
  async destroyKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`)
    const keepIndex = AppState.keeps.findIndex((keep) => keep.id == keepId)
    AppState.keeps.splice(keepIndex, 1)
  }

  async getAllKeeps(){
    AppState.keeps.length = 0
    const response = await api.get('api/keeps')
    const keeps = response.data.map((keepPOJO) => new Keep(keepPOJO))
    AppState.keeps = keeps
  }
}

export const keepsService = new KeepsService()