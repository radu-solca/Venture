import { Component, OnInit } from '@angular/core';

import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';
import { AuthService } from "app/services/auth.service";

@Component({
	selector: 'app-projects',
	templateUrl: './projects.component.html',
	styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

	private projects: Project[];

	constructor(
		private projectService: ProjectService,
		private authService: AuthService
	) { }

	ngOnInit() {
		this.loadProjects();
		setInterval(() =>{
			this.loadProjects()
		}, 2000);
	}

	private loadProjects() {
		this.projectService
			.getAll()
			.subscribe(projects => this.projects = projects)
	}

}
