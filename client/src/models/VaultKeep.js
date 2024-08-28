import { DbItem } from "./DbItem";
import { Keep } from "./Keep";

export class VaultKeep extends DbItem{
  /**
   * @param {{ keepId: Number; vaultId: Number; creatorId: String; }} data
   */
  constructor(data){
    super(data)
    this.keepId = data.keepId
    this.vaultId = data.vaultId
    this.creatorId = data.creatorId
  }
}

export class Kept extends Keep{
  /**
   * @param {{ vaultKeepId: Number; vaultKeepCreatorId: String; }} data
   */
  constructor(data){
    super(data)
    this.vaultKeepId = data.vaultKeepId
    this.vaultKeepCreatorId = data.vaultKeepCreatorId
  }
}