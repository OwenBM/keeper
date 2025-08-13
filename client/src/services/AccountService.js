import { Profile } from '@/models/Profile.js'
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'
import { Modal } from 'bootstrap'

class AccountService {
  async editAccount(profileData) {
    const response = await api.put('/account', profileData)
    logger.log("new profile!", response.data)
    // AppState.account = new Account(response.data);
    AppState.profile = new Profile(response.data);
    Modal.getOrCreateInstance('#EditProfileModal').hide()
  }

  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
