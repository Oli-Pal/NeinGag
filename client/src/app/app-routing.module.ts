import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './memes/member-detail/member-detail.component';
import { MemberEditComponent } from './memes/member-edit/member-edit.component';
import { MemeCommentsComponent } from './memes/meme-comments/meme-comments.component';
import { MemeListComponent } from './memes/meme-list/meme-list.component';
import { PhotoEditorComponent } from './memes/photo-editor/photo-editor.component';
import { PopularListComponent } from './memes/popular-list/popular-list.component';
import { MessagesComponent } from './messages/messages.component';
import { PaymentComponent } from './payment/payment.component';
import { RegisterComponent } from './register/register.component';
import { AdminGuard } from './_guards/admin.guard';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: PopularListComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'detail/:username', component: MemberDetailComponent},
      {path: 'edit', component: MemberEditComponent},
      {path: 'messages', component: MessagesComponent},
      {path: 'add-photo', component: PhotoEditorComponent},
      {path: 'comments/:id', component: MemeCommentsComponent},
      {path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard]}
    ]
  },
  {path: 'memes', component: MemeListComponent},
  {path: 'popular', component: PopularListComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'payment', component: PaymentComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
