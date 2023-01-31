import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppserviceService } from '../Appservice/appservice.service';
import { NotificationService } from '../Appservice/notification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private notifyservice: NotificationService, private appserv: AppserviceService){}

  register(form: NgForm){
    
    let credential  = form.value;
    this.appserv.signup(credential).subscribe({
      next: (res) =>{
        this.notifyservice.showSuccess(res.Message,"Successful");
      },
      error : (err)=>{
        this.notifyservice.showError("User creation failed! Please check user details and try again.", "Error");
        console.log(err);
      }
    });

  }

}
