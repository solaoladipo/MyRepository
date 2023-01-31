import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AppserviceService {

  private baseUrl: string = "https://localhost:7252/api/Authenticate/";

  constructor(private http: HttpClient) { }

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

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }


}
