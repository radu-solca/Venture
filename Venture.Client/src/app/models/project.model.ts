export class Project{
	public id : string;
	public title : string;
	public description : string;
	public ownerId : string;
	public ownerName : string;

	public constructor(init?:Partial<Project>) {
        Object.assign(this, init);
    }
}