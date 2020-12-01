import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './memes/member-detail/member-detail.component';
import { MemeCommentsComponent } from './memes/meme-comments/meme-comments.component';
import { MemeDetailComponent } from './memes/meme-detail/meme-detail.component';
import { MemeListComponent } from './memes/meme-list/meme-list.component';
import { MessagesComponent } from './messages/messages.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'memes/:username', component: MemeDetailComponent},
      {path: 'detail/:username', component: MemberDetailComponent},
      {path: 'messages', component: MessagesComponent}
    ]
  },
  {path: 'memes', component: MemeListComponent},
  {path: 'lists', component: ListsComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
