import { Component } from '@angular/core';
import { AbstractControl,
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { AppserviceService } from '../Appservice/appservice.service';
import { NotificationService } from '../Appservice/notification.service';
import Validation from '../utils/Validation';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  form: FormGroup = new FormGroup({
    Username: new FormControl(''),
    Email: new FormControl(''),
    Password: new FormControl(''),
    confirmPassword: new FormControl('')
  });
  submitted = false;

  constructor(private notifyservice: NotificationService, private appserv: AppserviceService, private formBuilder: FormBuilder){}


  ngOnInit(){
    this.form = this.formBuilder.group({
      Username: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      Password:['', [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(20),
      ],
    ],
    confirmPassword:['', Validators.required],
  },

  {
    validators: [Validation.match('Password', 'confirmPassword')],
  
    }
    );
  }

  get f(): {[key:string]: AbstractControl}{
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    let credential  = {Username:this.form.value.Username, Email:this.form.value.Email, Password: this.form.value.Password }
    this.appserv.signup(credential).subscribe({
      next: (res) =>{
        this.notifyservice.showSuccess(res.Message,"Successful");
        this.form.reset();
      },
      error : (err)=>{
        this.notifyservice.showError("User creation failed! Please check user details and try again.", "Error");
        console.log(err);
      }
    });

    
  }



  /* register(form: NgForm){
    
    let credential  = form.value;
    this.appserv.signup(credential).subscribe({
      next: (res) =>{
        this.notifyservice.showSuccess(res.Message,"Successful");
        form.reset();
      },
      error : (err)=>{
        this.notifyservice.showError("User creation failed! Please check user details and try again.", "Error");
        console.log(err);
      }
    });

  } */

}
