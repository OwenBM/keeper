import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { Modal } from "bootstrap";


class VaultKeepsService{
    deleteVaultKeep(keepId) {
        logger.log("vaultkeep id", keepId)
    }
    async createVaultKeep(vaultKeepData) {
        // logger.log
        const response = await api.post("api/vaultkeeps", vaultKeepData)
        logger.log(response.data)
    }

}

export const vaultKeepsService = new VaultKeepsService();