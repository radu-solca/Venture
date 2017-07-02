import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Observable } from 'rxjs';

import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';
import { Comment } from '../../models/comment.model';
import { TeamService } from "app/services/team.service";
import { User } from "app/models/user.model";

@Component({
	selector: 'app-project',
	templateUrl: './project.component.html',
	styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

	//Temp stuff:
	private isOwner: boolean;
	private isTeamMember: boolean;
	private ownerId: string = "173c3bfa-0000-0000-8488-54e618afd0ef";

	private project: Project;
	private chat: Comment[];
	private team: User[];

	private commentText: string;

	constructor(
		private route: ActivatedRoute,
		private projectService: ProjectService,
		private teamService: TeamService,
		private router: Router
	) { }

	ngOnInit() {
		this.isOwner = false;
		this.isTeamMember = false;

		this.route.params.subscribe(params => {
			let id = params['id'];
			this.loadData(id);
		})
	}

	private loadData(id: string): void {
		this.projectService
			.getById(id)
			.subscribe(project => this.project = project);

		setInterval(() => {
			this.projectService
				.getChat(id)
				.subscribe(chat => this.chat = chat);
		}, 2000);
		

		this.teamService
			.getTeamMembers(id)
			.subscribe(team => this.team = team);
	}

	private postInChat(): void {
		let comment = new Comment({authorId : this.ownerId, content : this.commentText});
		this.projectService.postInChat(this.project.id, comment)
			.subscribe(
				res => console.log(res),
				err => console.log(err));
		this.commentText = "";
	}
}
