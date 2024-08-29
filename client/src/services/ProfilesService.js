import { Profile } from "@/models/Profile"
import { api } from "./AxiosService"
import { AppState } from "@/AppState"
import { Vault } from "@/models/Vault"
import { Keep } from "@/models/Keep"


class ProfilesService{

  async getProfileData(profileId){
    AppState.focusedProfile = null
    AppState.keeps = []
    AppState.profileVaults = []
    const response = await api.get(`api/profiles/${profileId}`)
    const focusedProfile = new Profile(response.data)
    AppState.focusedProfile = focusedProfile
  }

  async getProfileKeeps(profileId){
    const response = await api.get(`api/profiles/${profileId}/keeps`)
    const profileKeeps = response.data.map((keep) => new Keep(keep))
    AppState.keeps = profileKeeps
  }
  
  async getProfileVaults(profileId){
    const response = await api.get(`api/profiles/${profileId}/vaults`)
    const profileVaults = response.data.map((vault) => new Vault(vault))
    AppState.profileVaults = profileVaults
  }
  
}
export const profilesService = new ProfilesService()