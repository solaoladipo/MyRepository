

export class loginModel{
    Username!: string; 
    Password!: string;
}

export class registerModel {
    Username!: string;
    Email!: string;
    Password!: string;
}

export class paymentDetails{
    ColdingDetailsId!: string;
    ColdingHeadId!: string;
    Particulars!: string;
    ActualAmount!: number;
    TransactionFee!: number;
    WHT!: number;
    VAT!: number;
    NetAmount!: number;
    Allocate!: string;

}

export class PaymentVoucher {

    constructor(){

        this.Detail = [];

    }

    ColdingHeadId! : string;
    Refno!: string;
    BeneficiaryCode!: string;
    TransDate!: string;
    Documentno!:string;
    VAT!: number;
    WHT!:number;
    TotalAmount!:number;
    sendforapproval!:boolean;
    approved!:boolean;
    createdby!:string;
    Datecreated!:string
    currency!:string;
    EarlierAdditionorDeduction!:number;
    AnotherCharges!:string;
    Detail! : paymentDetails[];
}