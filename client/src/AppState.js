import { reactive } from 'vue'
import { Keep } from "./models/Keep.js"
import { Kept } from "./models/VaultKeep.js"

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,

  /** @type {import('./models/Profile.js').Profile} profile info from the database*/
  focusedProfile: null,

  /** @type {Keep[] | Kept[]} keeps from the database*/
  keeps: [],
  /** @type {import('./models/Keep.js').Keep | import('./models/VaultKeep.js').Kept} active keep for keep details modal*/
  activeKeep: null,

  /** @type {import('./models/Vault.js').Vault[]} all vaults tied to account*/
  accountVaults: [],
  /** @type {import('./models/Vault.js').Vault[]} all public vaults tied to a profile*/
  profileVaults: [],
  
  /** @type {import('./models/Vault.js').Vault} vault information for the vault page*/
  activeVault: null,
})

