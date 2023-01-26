import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { InvoicingComponent } from './Pages/invoicing/invoicing.component';
import { RegisterComponent } from './register/register.component';


const routes: Routes = [
  {path:"", component: LoginComponent},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},
  {path: "layout", component: LayoutComponent, 
  children:[
    {path: "",component: DashboardComponent},
    {path: "Dashboard", component:DashboardComponent},
    {path: "Invoicing", component: InvoicingComponent}

  ]}

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }