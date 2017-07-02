import { Component, OnInit } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';

import { AuthService } from "app/services/auth.service";

@Component({
	selector: 'app-navigation',
	templateUrl: './navigation.component.html',
	styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

	constructor(
		private location: Location,
		private authService: AuthService
	) {}

	ngOnInit() {
	}

	private usernameText: string;
	private passwordText: string;

	private isAtPath(path: string): boolean {
		return this.location.path() == path;
	}
}
