import { DbItem } from "./DbItem";
import { Profile } from "./Profile";

export class Keep extends DbItem{
  constructor(data){
    super(data)
      this.name = data.name
      this.description = data.description
      this.img = data.img
      this.views = data.views
      this.keepCount = data.keepCount
      this.creatorId = data.creatorId
      this.creator = new Profile(data.creator)
  }
}

