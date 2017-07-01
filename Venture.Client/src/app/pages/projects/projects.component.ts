import { Component, OnInit } from '@angular/core';

import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';

@Component({
	selector: 'app-projects',
	templateUrl: './projects.component.html',
	styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

	private projects: Project[];

	constructor(
		private projectService: ProjectService) { }

	ngOnInit() {
		this.loadProjects();
	}

	private loadProjects() {
		this.projectService
			.getAll()
			.subscribe(projects => this.projects = projects)
	}

}
