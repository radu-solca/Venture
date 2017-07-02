import { Injectable } from '@angular/core';
import { HttpService } from "app/services/httpService";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { User } from "app/models/user.model";

@Injectable()
export class TeamService extends HttpService {

	constructor(
		http: Http
	) {
		super(http, "http://localhost:40000/v1/teams");
	}

	public getTeamMembers(id: string) : Observable<User[]>{
		return this.get("/" + id + "/users");
	}
}
