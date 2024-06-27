import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TaskerComponent } from './tasker/tasker.component';
import { ManageTaskComponent } from './tasker/manage-task/manage-task.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule, NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { withInterceptors, provideHttpClient } from '@angular/common/http';
import { TokenInterceptor } from './token.interceptor';
import { authGuard } from './auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    TaskerComponent,
    ManageTaskComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [
    provideHttpClient(withInterceptors([TokenInterceptor])),
    authGuard,
   
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
