import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGurdGuard } from './AuthGurd/auth-gurd.guard';
import { LayoutComponent } from './layout/layout.component';
import { LoginComponent } from './login/login.component';
import { BeneficiaryEditComponent } from './Pages/beneficiary-edit/beneficiary-edit.component';
import { BeneficiaryComponent } from './Pages/beneficiary/beneficiary.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { InvoiceEditComponent } from './Pages/invoice-edit/invoice-edit.component';
import { InvoicingComponent } from './Pages/invoicing/invoicing.component';
import { RegisterComponent } from './register/register.component';

//canActivate:[AuthGurdGuard], canActivate:[AuthGurdGuard]
const routes: Routes = [
  {path:"", redirectTo: '/login', pathMatch: 'full'},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},
  {path: "layout", component: LayoutComponent, canActivate:[AuthGurdGuard],
  children:[
    {path: "",component: DashboardComponent, canActivate:[AuthGurdGuard]},
    {path: "Dashboard", component:DashboardComponent, canActivate:[AuthGurdGuard]},
    {path: "Invoicing", component: InvoicingComponent, canActivate:[AuthGurdGuard]},
    {path: "InvoiceEdit", component: InvoiceEditComponent, canActivate:[AuthGurdGuard]},
    {path: "Beneficiary", component: BeneficiaryComponent},
    {path: "BeneficiaryEdit", component: BeneficiaryEditComponent},

  ]}

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }