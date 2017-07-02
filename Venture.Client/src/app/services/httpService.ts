import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

export class HttpService{

	constructor(
		private http: Http, 
		private apiEndpoint: string
	){}

	protected post(
		path: string, 
		payload: any,
		headers: Headers = new Headers({ 'Content-Type': 'application/json' })
	): Observable<Response> 
	{

    	let options = new RequestOptions({ headers: headers });
		return this.http.post(
			this.apiEndpoint + path,
			payload,
			options)
			.map(response => response.json());
	}

	protected get(
		path: string,
		headers: Headers = new Headers()
		): Observable<any> {

		let options = new RequestOptions({ headers: headers });

		return this.http.get(
			this.apiEndpoint + path,
			options)
			.map(response => response.json());
	}
}