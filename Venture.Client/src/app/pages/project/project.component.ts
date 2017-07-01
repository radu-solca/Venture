import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/switchMap';

import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  private isOwner : boolean;
  private isTeamMember : boolean;
  private project : Project;

  constructor(
    private route : ActivatedRoute,
    private projectService : ProjectService,
    private router : Router
  ) {}

  ngOnInit() {
    this.isOwner = false;
    this.isTeamMember = false;

    this.route.paramMap
      .switchMap((params: ParamMap) => this.projectService.getById(params.get('id')))
      .subscribe((project: Project) => {
        if(project === undefined){
          this.router.navigate(["notfound"]);
        }
        
        this.project = project;
      });
  }
}
