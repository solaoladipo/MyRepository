import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {ReactiveFormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import {ToastrModule} from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LayoutComponent } from './layout/layout.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { InvoicingComponent } from './Pages/invoicing/invoicing.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptorInterceptor } from './Interceptor/token-interceptor.interceptor';
import { InvoiceEditComponent } from './Pages/invoice-edit/invoice-edit.component';
import { BeneficiaryComponent } from './Pages/beneficiary/beneficiary.component';
import { BeneficiaryEditComponent } from './Pages/beneficiary-edit/beneficiary-edit.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    LayoutComponent,
    DashboardComponent,
    InvoicingComponent,
    InvoiceEditComponent,
    BeneficiaryComponent,
    BeneficiaryEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()

  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:TokenInterceptorInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
