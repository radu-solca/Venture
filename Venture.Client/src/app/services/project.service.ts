import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Project } from '../models/project.model';
import { Comment } from '../models/comment.model';
import { HttpService } from "app/services/httpService";
import { AuthService } from "app/services/auth.service";

@Injectable()
export class ProjectService extends HttpService {

	constructor(
		http: Http,
		private authService: AuthService) { 
		super(http, "http://localhost:40000/v1/projects");
	}

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

		if(!this.authService.isLoggedIn()){
			throw new Error("Not logged in!");
		}

		let token = this.authService.token;
		let headers = new Headers({'Authorization': 'Bearer ' + token});

		return this.post("/" + id + "/chat", comment, headers);
	}

	public create(title: string, description: string): Observable<Response>{

		if(!this.authService.isLoggedIn()){
			throw new Error("Not logged in!");
		}

		let token = this.authService.token;
		let headers = new Headers({'Authorization': 'Bearer ' + token});
		var payload = new Project({title: title, description: description});

		return this.post("/", payload, headers);
	}
}
