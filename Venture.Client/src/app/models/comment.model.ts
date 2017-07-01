export class Comment{
	public id : string;
	public postedOn : string;
	public content : string;
	public authorId : string;
	public authorName : string;

	public constructor(init?:Partial<Comment>) {
        Object.assign(this, init);
    }
}