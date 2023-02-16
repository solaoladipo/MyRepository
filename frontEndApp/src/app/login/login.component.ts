import { Component } from '@angular/core';
import { NgForm,
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AppserviceService } from '../Appservice/appservice.service';
import { NotificationService } from '../Appservice/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  form: FormGroup = new FormGroup({
    Username: new FormControl(''),
    Password: new FormControl('')
  });
  submitted = false;

  constructor(private notifyservice: NotificationService, private appserv: AppserviceService, private router: Router, private formBuilder: FormBuilder){}

  ngOnInit(){
    this.form = this.formBuilder.group({
      Username: ['', [Validators.required, Validators.email]],
      Password:['', [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(20),
      ],
    ],
    });
  }
  
  get f(): {[key:string]: AbstractControl}{
    return this.form.controls;
  }

  onSubmit(): void{
    this.submitted = true;
    if(this.form.invalid){
      return;
    }

    let credential = this.form.value;
    this.appserv.signIn(credential).subscribe({
      next: (res) =>{
        this.appserv.storeToken(res.token);
        this.appserv.storeUsername(res.username);
        this.notifyservice.showSuccess("You have successfully login","Successful");
        this.router.navigate(['layout']);
      },
      error : (err)=>{
        this.notifyservice.showError("An error has occured, kindly check your login details", "Error");
        console.log(err);
      }
    });
  }
  
/*   login(form: NgForm){
    
    let credential = form.value;
    this.appserv.signIn(credential).subscribe({
      next: (res) =>{
        this.appserv.storeToken(res.token);
        this.appserv.storeUsername(res.username);
        this.notifyservice.showSuccess("You have successfully login","Successful");
        this.router.navigate(['layout']);
      },
      error : (err)=>{
        this.notifyservice.showError("An error has occured, kindly check your login details", "Error");
        console.log(err);
      }
    });

  } */


  

}
