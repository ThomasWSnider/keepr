import { DbItem } from "./DbItem";
import { Profile } from "./Profile";


export class Vault extends DbItem{
  /**
   * @param {{ name: String; description: String; img: String; isPrivate: Boolean; creatorId: String; creator: Profile; }} data
   */
  constructor(data){
    super(data)
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.isPrivate = data.isPrivate
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }

  get imgUrl(){
    return `url(${this.img})`
  }
}