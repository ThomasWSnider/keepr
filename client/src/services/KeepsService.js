import { api } from "./AxiosService"
import { Keep } from "@/models/Keep";
import { AppState } from "@/AppState";
import { logger } from "@/utils/Logger";

class KeepsService{
  
  activateKeep(keepId){
    AppState.activeKeep = null
    const activeKeep = AppState.keeps.find((keep) => keep.id == keepId)
    AppState.activeKeep = activeKeep
    logger.log(AppState.activeKeep);
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