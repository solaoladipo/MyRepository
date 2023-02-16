import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AppserviceService {

  private baseUrl: string = "https://localhost:7252/api/Authenticate/";
 

  constructor(private http: HttpClient) { 

   
  }

  signup(signObj: any){

    return this.http.post<any>(`${this.baseUrl}Register`, signObj);
  }
  
  signIn(signIn: any){
    return this.http.post<any>(`${this.baseUrl}Login`, signIn);
  }

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue);
  }

  getStoredToken(){
    return localStorage.getItem('token');
  }

  storeUsername(username: string){
    localStorage.setItem('UserName', username );
  }

  getUsername(){
    return localStorage.getItem('UserName');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  Logout(){
    localStorage.clear();
  }

  decodeToken(){
    const jwthelper = new JwtHelperService();
    const token = this.getStoredToken()!;
    return jwthelper.decodeToken(token)
  }

  /* getfullnamefromToken(){
    if(this.userpayload){
      return this.userpayload.http;
    }
  } */

  


}
