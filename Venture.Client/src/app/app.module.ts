import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule, Http } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

/* START bootstrap */
import { BsDropdownModule } from 'ngx-bootstrap';
/* END bootstrap */

import { AppComponent } from './app.component';
import { NavigationComponent } from './layout/navigation/navigation.component';
import { ProjectsComponent } from './pages/projects/projects.component';
import { NotFoundComponent } from './pages/notfound/notfound.component';
import { ProjectComponent } from './pages/project/project.component';

import { ProjectService } from './services/project.service';
import { AuthService } from "app/services/auth.service";
import { ProjectCreatorComponent } from './pages/project-creator/project-creator.component';
import { TeamService } from "app/services/team.service";

/* routing */
const appRoutes: Routes = [
  { path: '', component: ProjectsComponent },
  { path: 'projects', component: ProjectsComponent },
  { path: 'projects/:id', component: ProjectComponent },
  { path: 'project-creator', component: ProjectCreatorComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    ProjectsComponent,
    NotFoundComponent,
    ProjectComponent,
    ProjectCreatorComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BsDropdownModule.forRoot(),
    BrowserModule,
    HttpModule,
    FormsModule
  ],
  providers: [ProjectService, AuthService, TeamService],
  bootstrap: [AppComponent]
})
export class AppModule { }
