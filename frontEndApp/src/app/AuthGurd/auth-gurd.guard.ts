import { Injectable } from '@angular/core';
import { CanActivate, Router,  UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AppserviceService } from '../Appservice/appservice.service';
import { NotificationService } from '../Appservice/notification.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGurdGuard implements CanActivate {

  constructor(private auth:AppserviceService, private router: Router, private notifyService:NotificationService){}

  canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if(this.auth.isLoggedIn()){
      return true
    }else {
      this.notifyService.showError("Please Login First", "ERROR");
      this.router.navigate(['login']);
      return false
    }

  }
  
}
