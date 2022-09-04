import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './authentication/login/login.component';
import { RecoverPasswordComponent } from './authentication/recover-password/recover-password.component';
import { RegisterComponent } from './authentication/register/register/register.component';
import { SendEmailComponent } from './authentication/send-email/send-email.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'login', component: LoginComponent},
  {path: 'dashboard', component: DashboardComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'recoverPassword', component: RecoverPasswordComponent},
  {path: 'sendEmail', component: SendEmailComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
