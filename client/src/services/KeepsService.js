import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"

class KeepsService{
  async getKeepById(keepId) {
    AppState.Keep = null
    const response = await api.get(`api/keeps/${keepId}`)
    logger.log(response.data)
    AppState.Keep = new Keep(response.data)
  }


  async getAllKeeps() {
    const response = await api.get("api/keeps")
    logger.log(response.data)
    const newKeep = response.data.map(keep => new Keep(keep))
    AppState.Keeps = newKeep
  }

}

export const keepsService = new KeepsService()