import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppserviceService } from '../Appservice/appservice.service';


@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {
[x: string]: any|string;

  public fullname : string = "";
constructor(private router: Router, private appservice: AppserviceService){}
  ngOnInit()
  {
    this.fullname = this.appservice.getUsername()!;
  }

signout(){
this.appservice.Logout();
this.router.navigate(['login']);
}

}
