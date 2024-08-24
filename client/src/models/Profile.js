import { DbItem } from "./DbItem";


export class Profile extends DbItem {
  constructor(data) {
    super(data);
    this.name = data.name;
    this.picture = data.picture;
  }
}