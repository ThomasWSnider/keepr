import { logger } from "@/utils/Logger";
import { api } from "./AxiosService"
import { Keep } from "@/models/Keep";
import { AppState } from "@/AppState";

class KeepsService{

  async getAllKeeps(){
    const response = await api.get('api/keeps')
    const keeps = response.data.map((keepPOJO) => new Keep(keepPOJO))
    AppState.keeps = keeps
  }
}

export const keepsService = new KeepsService()