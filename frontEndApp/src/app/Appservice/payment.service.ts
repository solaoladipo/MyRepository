import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaymentVoucher } from '../ViewModel/AuthModel';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private baseUrl: string = "https://localhost:7252/api/Paymentvoucher/";
 

  constructor(private http:HttpClient) { }


  saveVoucher(signObj: any){

    return this.http.post<any>(`${this.baseUrl}AddPaymentVoucher`, signObj);
  }

  GetPaymentVoucherByCode(dataRecord: any){
   return this.http.get<any>(`${this.baseUrl}GetPaymentVoucher`, {params:{paymentId:dataRecord}});
  }

  saveVoucherEdit(signObj: any){

    return this.http.post<any>(`${this.baseUrl}ModifyPaymentVoucher`, signObj);
  }




}
