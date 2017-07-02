import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { User } from '../models/user.model';
import { HttpService } from './httpService';

@Injectable()
export class AuthService extends HttpService {

	constructor(http: Http) {
		super(http, "http://localhost:40001/v1/auth");
	}

	private token: string = null;
	public user: User = null;

	public login(username: string, password: string) : void{
		let payload = "grant_type=password";
		payload += "&username=" + username;
		payload += "&password=" + password;

		let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });

		this.post("/token", payload, headers)
			.subscribe(
				response => {
					this.token = response["access_token"];
					this.updateProfile();
				},
				error => {
					alert("login failed");
					console.log(error);
				}
			);
	}

	public logout() : void{
		this.token = null;
		this.user = null;
	}

	public updateProfile() : void{

		if(!this.token){
			return;
		}

		let headers = new Headers({ 'Authorization': 'Bearer ' + this.token });
		this.get("/profile", headers)
			.subscribe(response => {
				this.user = new User({id: response["subject"], name: response["name"]});
			});
	}

	public isLoggedIn() : boolean{
		return this.user != null && this.token != null;
	}

	public register(user: User) : void{
		this.post("/register", user)
			.subscribe(
				response => {
					this.login(user.name, user.password);
				},
				error => {
					alert("register failed");
					console.log(error);
				}
			);
	}

}
