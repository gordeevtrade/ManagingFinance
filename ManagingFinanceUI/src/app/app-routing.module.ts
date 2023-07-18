import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DailyReportComponent } from './components/daily-report/daily-report.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './AuthGuard';
import { DashboardLayoutComponent } from './components/dashboard-layout/dashboard-layout.component';
import { LogoutComponent } from './components/logout/logout/logout.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CallbackComponent } from './components/google-login/callback/callback.component';



const routes: Routes = [
  { 
    path: 'dashboard-layout', 
    component: DashboardLayoutComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'report', component: DailyReportComponent },
      { path: 'logout', component: LogoutComponent },
    ]
  },
  
  { path: 'login', 
  component: LoginComponent,
},

{ path: 'register', component: RegistrationComponent },
{ path: 'callback', component: CallbackComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
