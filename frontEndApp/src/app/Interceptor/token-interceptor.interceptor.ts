import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AppserviceService } from '../Appservice/appservice.service';
import { NotificationService } from '../Appservice/notification.service';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptorInterceptor implements HttpInterceptor {

  constructor(private authService: AppserviceService, private notifyservice: NotificationService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const myToken = this.authService.getStoredToken();

    if(myToken){
      request = request.clone({ setHeaders: {Authorization : `Bearer ${myToken}`}})
    }

    return next.handle(request).pipe(
      catchError((err : any) => {
        if(err instanceof HttpErrorResponse){
          if(err.status === 401){
            this.notifyservice.showWarning('Token is expired, Kindly login again.', 'Warning')
            this.router.navigate(['login']);
          }
        }
        return throwError(()=> new Error("some other error occured"))
      })

    );
 
}

}

