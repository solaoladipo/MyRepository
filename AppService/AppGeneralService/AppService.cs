using DTOS.AppModel;
using DTOS.Interface;
using DTOS.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.AppGeneralService
{
    public class AppService
    {
        private readonly IUnitOfWork _unitOfwork;

        public AppService(IUnitOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }

        public void AddBeneficiary (Beneficiary beneficiary)
        {
            _unitOfwork.Beneficiary.Add(beneficiary);
        }

        public void RemoveBeneficiary(Beneficiary beneficiary)
        {
            _unitOfwork.Beneficiary.Remove(beneficiary);
        }
        public void AddPaymentVoucher(PaymentModel paymentModel)
        {
            Guid refon = Guid.Parse(GeneralClass.PV_ID());
            Guid HeadId = Guid.NewGuid();
            CodingHead head = new CodingHead()
            {
                AnotherCharges = paymentModel.AnotherCharges,
                approved = paymentModel.approved,
                BeneficiaryCode = paymentModel.BeneficiaryCode,
                ColdingHeadId = HeadId,
                createdby = paymentModel.createdby,
                currency = paymentModel.currency,
                Datecreated = DateTime.Now,
                Documentno = paymentModel.Documentno,
                EarlierAdditionorDeduction = paymentModel.EarlierAdditionorDeduction,
                Refno = GeneralClass.GetRefnumber(refon, "Refno", "CodingHead", true),
                sendforapproval = paymentModel.sendforapproval,
                TotalAmount = paymentModel.TotalAmount,
                TransDate = paymentModel.TransDate,
                VAT = paymentModel.VAT,
                WHT = paymentModel.WHT
            };

            _unitOfwork.CodingHead.Add(head);

            if(paymentModel.CodingDetail != null)
            {
                foreach (CodingDetail detail in paymentModel.CodingDetail)
                {
                    detail.ColdingHeadId = HeadId;
                    detail.ColdingDetailsId = Guid.NewGuid();
                    _unitOfwork.CodingDetail.Add(detail);
                }

            }

        }

        public void ModifyPaymentVoucher(PaymentModel paymentModel)
        {
            if(paymentModel.CodingDetail != null)
            {
                _unitOfwork.CodingDetail.RemoveRange(paymentModel.CodingDetail);
            }
           
            CodingHead? headModify = _unitOfwork.CodingHead.Find(x => x.ColdingHeadId == paymentModel.ColdingHeadId).FirstOrDefault();
            if (headModify != null && paymentModel.CodingDetail != null)
            {
                _unitOfwork.CodingHead.Remove(headModify);

                CodingHead head = new CodingHead()
                {
                    AnotherCharges = paymentModel.AnotherCharges,
                    approved = paymentModel.approved,
                    BeneficiaryCode = paymentModel.BeneficiaryCode,
                    ColdingHeadId = paymentModel.ColdingHeadId,
                    createdby = paymentModel.createdby,
                    currency = paymentModel.currency,
                    Datecreated = DateTime.Now,
                    Documentno = paymentModel.Documentno,
                    EarlierAdditionorDeduction = paymentModel.EarlierAdditionorDeduction,
                    Refno = paymentModel.Refno,
                    sendforapproval = paymentModel.sendforapproval,
                    TotalAmount = paymentModel.TotalAmount,
                    TransDate = paymentModel.TransDate,
                    VAT = paymentModel.VAT,
                    WHT = paymentModel.WHT
                };

                _unitOfwork.CodingDetail.AddRange(paymentModel.CodingDetail);

            }
        }

        public int SaveRecord()
        {
            return _unitOfwork.Save();
        }

    }
}
