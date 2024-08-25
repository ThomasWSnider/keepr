import { api } from "./AxiosService"
import { Keep } from "@/models/Keep";
import { AppState } from "@/AppState";

class KeepsService{

  activateKeep(keepId){
    AppState.activeKeep = null
    const activeKeep = AppState.keeps.find((keep) => keep.id == keepId)
    AppState.activeKeep = activeKeep
  }

  async getAllKeeps(){
    const response = await api.get('api/keeps')
    const keeps = response.data.map((keepPOJO) => new Keep(keepPOJO))
    AppState.keeps = keeps
  }
}

export const keepsService = new KeepsService()