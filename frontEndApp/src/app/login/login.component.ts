import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AppserviceService } from '../Appservice/appservice.service';
import { NotificationService } from '../Appservice/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private notifyservice: NotificationService, private appserv: AppserviceService, private router: Router){}

  login(form: NgForm){
    
    let credential = form.value;
    this.appserv.signIn(credential).subscribe({
      next: (res) =>{
        this.appserv.storeToken(res.token);
        this.notifyservice.showSuccess("You have successfully login","Successful");
        this.router.navigate(['layout']);
      },
      error : (err)=>{
        this.notifyservice.showError("An error has occured, kindly check your login details", "Error");
        console.log(err);
      }
    });

  }


  

}
