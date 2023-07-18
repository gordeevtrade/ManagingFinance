import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { AddCategoryDialogComponent } from './dialog-components/category/add-category-dialog/add-category-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { UpdateCategoryDialogComponent } from './dialog-components/category/update-category-dialog/update-category.component';
import { DailyReportComponent } from './components/daily-report/daily-report.component';
import { UpdateTransactionDialogComponent } from './dialog-components/transaction/update-transaction-dialog/update-transaction-dialog.component';
import { RemoveCategoryDialogComponent } from './dialog-components/category/remove-category-dialog/remove-category-dialog.component';
import {MatCardModule} from '@angular/material/card';
import { AddTransactionDialogComponent } from './dialog-components/transaction/add-transaction-dialog/add-transaction-dialog.component';
import { MatTableModule } from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RemoveTransactionDialogComponent } from './dialog-components/transaction/remove-transaction-dialog/remove-transaction-dialog.component';
import { LoginComponent } from './components/login/login.component';
import { AuthInterceptor } from './components/login/AuthInterceptor';
import { DashboardLayoutComponent } from './components/dashboard-layout/dashboard-layout.component';
import { LogoutComponent } from './components/logout/logout/logout.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CallbackComponent } from './components/google-login/callback/callback.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ShowTransactionsComponent } from './dialog-components/transaction/show-transactions/show-transactions.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';




@NgModule({
  declarations: [
    AppComponent,
    AddCategoryDialogComponent,
    UpdateCategoryDialogComponent,
    DailyReportComponent,
    UpdateTransactionDialogComponent,
    RemoveCategoryDialogComponent,
    AddTransactionDialogComponent,
    DashboardComponent,
    RemoveTransactionDialogComponent,
    LoginComponent,
    DashboardLayoutComponent,
    LogoutComponent,
    RegistrationComponent,
    CallbackComponent,
    ShowTransactionsComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatButtonModule,
    FormsModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    ReactiveFormsModule,
    MatSnackBarModule

  ],
  providers: [ { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
