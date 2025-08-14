import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { Modal } from "bootstrap";
import { AppState } from "@/AppState.js";


class VaultKeepsService{
    async deleteVaultKeep(vaultKeep) {
        const response = await api.delete(`api/vaultkeeps/${vaultKeep.vaultKeepId}`)
        // logger.log(response.data)
        // logger.log(vaultKeep)
      const newKeeps = AppState.vaultKeeps.filter(pojo => pojo['id'] !== vaultKeep.id)
        AppState.vaultKeeps = newKeeps
    }
    async createVaultKeep(vaultKeepData) {
        // logger.log
        const response = await api.post("api/vaultkeeps", vaultKeepData)
        // logger.log(response.data)
    }

}

export const vaultKeepsService = new VaultKeepsService();