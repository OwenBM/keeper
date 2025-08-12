import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"
import { Keep, VaultKeep } from "@/models/Keep.js"


class VaultsService{
    async deleteVault(vaultId) {
        const response = await api.delete(`api/vaults/${vaultId}`)
        logger.log(response.data)

    }

    async getKeepsByVaultId(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log("vaultkeeps", response.data)
        AppState.vaultKeeps = response.data.map(pojo => new VaultKeep(pojo))
    }

    async createAlbum(albumData) {
        const response = await api.post("api/vaults", albumData)
        logger.log(response.data)
        AppState.vaults.push(new Vault(response.data))
    }

    async getAlbumById(vaultId){
        const response = await api.get(`api/vaults/${vaultId}`)
        logger.log(response.data)
        AppState.vault = new Vault(response.data)
    }


}

export const vaultsService = new VaultsService()