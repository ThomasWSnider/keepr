import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async editAccountInfo(editableAccountData) {
    await api.put(`account`, editableAccountData)
    let account = AppState.account
    account.name = editableAccountData.name ?? account.name
    account.picture = editableAccountData.picture ?? account.picture
    account.coverImg = editableAccountData.coverImg ?? account.coverImg
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
