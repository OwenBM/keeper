import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Profile } from "@/models/Profile.js"
import { Keep } from "@/models/Keep.js"
import { Vault } from "@/models/Vault.js"


class ProfileService{
    async getMyVaults() {
        const response = await api.get(`api/profiles/${AppState.account.id}/vaults`)
        // logger.log("api vaults", response.data)
        AppState.vaults = response.data.map(pojo => new Vault(pojo))
        // logger.log("vault appstate", AppState.vaults)

    }
    async getKeepsByProfileId(profileId) {
        AppState.Keeps = []
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        // logger.log(response.data)
        AppState.Keeps = response.data.map(pojo => new Keep(pojo))
        logger.log("profile keeps", AppState.Keeps)
    }
    async getVaultsByProfileId(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        // logger.log(response.data)
        AppState.vaults = response.data.map(pojo => new Vault(pojo))
        logger.log("profile vaults", AppState.vaults)
    }
    async getProfilebyId(profileId) {
        AppState.profile = null
        const response = await api.get(`api/profiles/${profileId}`)
        logger.log(response.data)
        AppState.profile = new Profile(response.data)
    }

}

export const profileService = new ProfileService()