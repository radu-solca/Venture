import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ProjectService } from "app/services/project.service";

@Component({
	selector: 'app-project-creator',
	templateUrl: './project-creator.component.html',
	styleUrls: ['./project-creator.component.scss']
})
export class ProjectCreatorComponent implements OnInit {

	constructor(
		private projectService: ProjectService,
		private router: Router
	) { }

	ngOnInit() {
	}

	private titleText: string;
	private descriptionText: string;

	private createProject() {
		console.log(this.titleText, this.descriptionText)
		this.projectService.create(this.titleText, this.descriptionText)
		.subscribe(
			response => { this.router.navigate["projects"] },
			error => { this.router.navigate["projects"] }
		);
	}

}
