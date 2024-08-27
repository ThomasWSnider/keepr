import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Keep.js').Keep[]} keeps from the database*/
  keeps: [],
  /** @type {import('./models/Keep.js').Keep} active keep for keep details modal*/
  activeKeep: null,
  /** @type {import('./models/Vault.js').Vault[]} all vaults tied to account*/
  accountVaults: [],
  /** @type {import('./models/Vault.js').Vault[]} all public vaults tied to a profile*/
  profileVaults: [],
  /** @type {import('./models/VaultKeep.js').Kept[]} all public vaults tied to a profile*/
  vaultKeeps: [],
})

