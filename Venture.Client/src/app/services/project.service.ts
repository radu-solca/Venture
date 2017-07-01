import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { Project } from '../models/project.model';

@Injectable()
export class ProjectService {

	private projects: Project[];

	private apiUrl = "http://localhost:49403/v1/projects/";

	constructor(private http: Http) { }

	public getAll(): Observable<Project[]> {
		return this.http.get(this.apiUrl)
			.map(response => response.json())
	}

	public getById(id: string): Observable<Project> {
		return this.http.get(this.apiUrl + id + "/")
			.map(response => response.json())
	}
}
