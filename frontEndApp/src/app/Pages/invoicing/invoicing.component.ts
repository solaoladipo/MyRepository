import { getLocaleDateFormat } from '@angular/common';
import { Component } from '@angular/core';
import { 
  FormBuilder,
  FormGroup,
  FormControl,
  Validators
} from '@angular/forms';
import { NotificationService } from 'src/app/Appservice/notification.service';
import { PaymentService } from 'src/app/Appservice/payment.service';
import { paymentDetails, PaymentVoucher } from 'src/app/ViewModel/AuthModel';

@Component({
  selector: 'app-invoicing',
  templateUrl: './invoicing.component.html',
  styleUrls: ['./invoicing.component.css']
})
export class InvoicingComponent {

  formHead: FormGroup = new FormGroup({
    BeneficiaryCode:new FormControl(''),
    Documentno:new FormControl(''),
    AnotherCharges:new FormControl(''),
    EarlierAdditionorDeduction:new FormControl(''),
    TransDate:new FormControl(''),
    currency:new FormControl(''),
    TotalAmount:new FormControl(''),

  });

  form: FormGroup = new FormGroup({
    Particulars: new FormControl(''),
    ActualAmount: new FormControl(''),
    TransactionFee: new FormControl(''),
    WHT: new FormControl(''),
    VAT: new FormControl(''),
    NetAmount: new FormControl(''),
    Allocate: new FormControl(''),
    whtPercent: new FormControl(''),
    vatPercent: new FormControl(''),
    grossup: new FormControl(''),
  });
 
  details : any[] = [];
  //formDetails: paymentDetails[] = [];
  


  
  
  constructor(private notifyservice: NotificationService, private formBuilder: FormBuilder, private payService: PaymentService){}

  ngOnInit(){

    this.form = this.formBuilder.group({
      Particulars: ['', [Validators.required]],
      ActualAmount:['', [Validators.required]],
      TransactionFee:['', [Validators.required]],
      WHT:['', [Validators.required]],
      VAT:['', [Validators.required]],
      NetAmount:['', [Validators.required]],
      Allocate:['', [Validators.required]],
      whtPercent:[''],
      vatPercent: [''],
      grossup:['']
    });

    this.formHead = this.formBuilder.group({

      BeneficiaryCode:[''],
      Documentno: [''],
      AnotherCharges:[''],
      EarlierAdditionorDeduction:[''],
      TransDate:[''],
      currency:[''],
      TotalAmount:['']

    });

    this.formHead.controls['EarlierAdditionorDeduction'].setValue(0);
    this.formHead.controls['TotalAmount'].setValue(0);

    this.form.controls['VAT'].setValue(0);
    this.form.controls['WHT'].setValue(0);
    this.form.controls['ActualAmount'].setValue(0);
    this.form.controls['TransactionFee'].setValue(0);
    this.form.controls['NetAmount'].setValue(0);
  }
  
  changeWHT(): void{

    if(this.form.value.ActualAmount == null || this.form.value.ActualAmount < 1){
      return this.notifyservice.showError("Kindly key in the Actual Amount", "Error");
    }else {

      let whtAmount = this.getCalculate(Number.parseFloat(this.form.value.whtPercent), Number.parseFloat(this.form.value.ActualAmount));
      this.form.controls['WHT'].setValue(whtAmount);
      let netamount = Number.parseFloat(this.form.value.ActualAmount) 
      - Number.parseFloat(this.form.value.WHT) + Number.parseFloat(this.form.value.VAT) + Number.parseFloat(this.form.value.TransactionFee);
      this.form.controls['NetAmount'].setValue(netamount);
  
    }


    

  }

  changeVAT(): void{

    if(this.form.value.ActualAmount == null || this.form.value.ActualAmount < 1){
      return this.notifyservice.showError("Kindly key in the Actual Amount", "Error");
    }else {

      let vatAmount = this.getCalculate(Number.parseFloat(this.form.value.vatPercent), Number.parseFloat(this.form.value.ActualAmount));
      this.form.controls['VAT'].setValue(vatAmount);
      let netamount = Number.parseFloat(this.form.value.ActualAmount) 
      - Number.parseFloat(this.form.value.WHT) + Number.parseFloat(this.form.value.VAT) + Number.parseFloat(this.form.value.TransactionFee);
      this.form.controls['NetAmount'].setValue(netamount);
    }

    
  }

  changeGrossup(): void{

    let grosscent = Number.parseFloat(this.form.value.grossup);
    let grossAmount = Number.parseFloat(this.form.value.ActualAmount);
    let realGrossup = 0;

    if(grosscent == 0.05){
      
       realGrossup = Number.parseFloat(((grossAmount /95) * 100).toFixed(2));

    }else if(grosscent == 0.075){

      realGrossup = Number.parseFloat(((grossAmount /92.5) * 100).toFixed(2));


    }else if(grosscent == 0.10){

      realGrossup = Number.parseFloat(((grossAmount /90) * 100).toFixed(2));

    }else if(grosscent == 0.0){

      this.form.controls['ActualAmount'].setValue(0);

    }



    
    //let grossupAmount = Number.parseFloat(this.form.value.ActualAmount) - grossAmount;
    this.form.controls['ActualAmount'].setValue(realGrossup);

  }

  getCalculate(percent:any, amount:any):number{
    
    let deucent = Number.parseFloat(percent);
    let actual = Number.parseFloat(amount);
    let deuAmount = deucent * actual;

    return deuAmount;
  }

  Reset(){
    this.form.controls['VAT'].setValue(0);
    this.form.controls['WHT'].setValue(0);
    this.form.controls['ActualAmount'].setValue(0);
    this.form.controls['TransactionFee'].setValue(0);
    this.form.controls['NetAmount'].setValue(0);

  }

  removeDetail(dataIndex : number){

    this.details.splice(dataIndex);
    let sumNetAmount : number = 0;
    this.details.forEach((x)=> {
      sumNetAmount = sumNetAmount + Number.parseFloat(x.NetAmount) ;
    });
    
    this.formHead.controls['TotalAmount'].setValue(sumNetAmount);

  }

  AddToList(){
    if(this.form.invalid){
      return
    }

    this.details.push({"Particulars":this.form.value.Particulars, "ActualAmount":this.form.value.ActualAmount, "TransactionFee": this.form.value.TransactionFee, "WHT":this.form.value.WHT, "VAT":this.form.value.VAT,"NetAmount": this.form.value.NetAmount, "Allocate":this.form.value.Allocate});

    this.formHead.controls['TotalAmount'].setValue(Number.parseFloat(this.formHead.value.TotalAmount) + Number.parseFloat(this.form.value.NetAmount));
    
    this.Reset();
    
  }


  saveRecord(): void{

    let TotalVat = 0;
    let Totalwht = 0;
    let TotalnetAmount = 0;
    this.details.forEach((x)=> {
      TotalVat = TotalVat + x.VAT;
      Totalwht = Totalwht + x.WHT;
    });

   if(this.formHead.value.AnotherCharges == "Earlier Addition"){

    TotalnetAmount = Number.parseFloat(this.formHead.value.TotalAmount) + Number.parseFloat(this.formHead.value.EarlierAdditionorDeduction);
   }else{

    TotalnetAmount = Number.parseFloat(this.formHead.value.TotalAmount) - Number.parseFloat(this.formHead.value.EarlierAdditionorDeduction);

   }

    this.formHead.controls['TotalAmount'].setValue(TotalnetAmount);
    let newPaymentVoucher: PaymentVoucher = new PaymentVoucher();
    newPaymentVoucher.AnotherCharges = this.formHead.value.AnotherCharges;
    newPaymentVoucher.BeneficiaryCode = this.formHead.value.BeneficiaryCode;
    newPaymentVoucher.ColdingHeadId = "5F53B788-81A5-434A-851A-BF9724BB3BDD";
    newPaymentVoucher.Datecreated = "10/02/2023";
    newPaymentVoucher.Documentno = this.formHead.value.Documentno;
    newPaymentVoucher.EarlierAdditionorDeduction = this.formHead.value.EarlierAdditionorDeduction;
    newPaymentVoucher.Refno = "refno";
    newPaymentVoucher.TotalAmount = TotalnetAmount;
    newPaymentVoucher.TransDate = this.formHead.value.TransDate;
    newPaymentVoucher.VAT = TotalVat;
    newPaymentVoucher.WHT = Totalwht;
    newPaymentVoucher.approved = false;
    newPaymentVoucher.createdby = "sola";
    newPaymentVoucher.currency = this.formHead.value.currency;
    newPaymentVoucher.sendforapproval = false;
    
    this.details.forEach((x)=>{
     let newDetails: paymentDetails = new paymentDetails();
     newDetails.ActualAmount = x.ActualAmount;
     newDetails.Allocate = x.Allocate;
     newDetails.NetAmount = x.NetAmount;
     newDetails.Particulars = x.Particulars;
     newDetails.TransactionFee = x.TransactionFee;
     newDetails.VAT= x.VAT;
     newDetails.WHT = x.WHT;
     newDetails.ColdingDetailsId = "02D82D95-1ACF-4B12-B156-65386316E8F8";
     newDetails.ColdingHeadId = "5F53B788-81A5-434A-851A-BF9724BB3BDD";
     newPaymentVoucher.Detail.push(newDetails);
    });

    this.payService.saveVoucher(newPaymentVoucher).subscribe({
      next: (res)=>{

        this.notifyservice.showSuccess(res.message, res.status);
        console.log(res);
        this.form.reset();
        this.formHead.reset();
        this.details.splice(0, this.details.length);
      },
      error : (error)=>{
        this.notifyservice.showError(error, "Error")

      }
    })

 

  }

  

}
