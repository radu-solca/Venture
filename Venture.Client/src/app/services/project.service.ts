import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Project } from '../models/project.model';
import { Comment } from '../models/comment.model';

@Injectable()
export class ProjectService {

	private projects: Project[];

	private static apiEndpoint : string = "http://localhost:49403/v1/projects";

	constructor(private http: Http) { }

	public getAll(): Observable<Project[]> {
		return this.get("/");
	}

	public getById(id: string): Observable<Project> {
		return this.get("/" + id)
	}

	public getChat(id: string): Observable<Comment[]> {
		return this.get("/" + id + "/chat")
	}

	public postInChat(id: string, comment: Comment): Observable<Response> {
		console.log(comment)
		return this.post("/" + id + "/chat", comment);
	}


	private post(path: string, payload: object): Observable<Response> {

		console.log("hi")
		let headers = new Headers({ 'Content-Type': 'application/json' });
    	let options = new RequestOptions({ headers: headers });
console.log("hisss")
		return this.http.post(
			ProjectService.apiEndpoint + path,
			payload,
			options)
			.map(response => response.json());
	}

	private get(path: string): Observable<any> {

		return this.http.get(ProjectService.apiEndpoint + path)
			.map(response => response.json());
	}
}
