export class User{
	public id : string;
	public name : string;
	public password : string;

	public constructor(init?:Partial<User>) {
        Object.assign(this, init);
    }
}