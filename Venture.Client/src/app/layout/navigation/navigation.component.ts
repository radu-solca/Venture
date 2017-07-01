import { Component, OnInit } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';

@Component({
	selector: 'app-navigation',
	templateUrl: './navigation.component.html',
	styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

	private location: Location;
	private isLoggedIn: boolean;
	private userName: string;

	constructor(location: Location) {
		 this.location = location;
		 this.isLoggedIn = true;
		 this.userName = "John Doe";
	}

	ngOnInit() {
	}

  private isAtPath(path: string) : boolean{
    return this.location.path() == path;
  }

	

}
