
export class Keep{
constructor(data){
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.description = data.description
    this.name = data.name
    this.img = data.img
    this.kept = data.kept
    this.views = data.views
    this.creatorId = data.creatorId
    this.creator = data.creator
}
}