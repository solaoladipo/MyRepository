import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  private fullName$ = new BehaviorSubject<string>("");

  constructor() { }

  public getFullNamefromstore(){
    return this.fullName$.asObservable();
  }

  public setFullNameforStore(fullname:string){
    this.fullName$.next(fullname);

  }
}
