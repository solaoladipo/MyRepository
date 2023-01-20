using AppService.AppGeneralService;
using DataAccessLayer.unitOfwork;
using DTOS.AppModel;
using DTOS.Interface;
using DTOS.SharedModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace InvoicingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentvoucherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfwork;
        public PaymentvoucherController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }
        #region Beneficiary
        [HttpPost]
        [Route("SaveBeneficiary")]
        public async Task<IActionResult> SaveBeneficiary(Beneficiary beneficiary)
        {
            beneficiary.BeneficiaryID = Guid.NewGuid();
            beneficiary.Datecreated = DateTime.Now;
            //beneficiary.Createdby = User.Identity.Name;
            _unitOfwork.Beneficiary.Add(beneficiary);
             int record = await Task.Run(()=> _unitOfwork.Save());
            if(record != 0) 
            {
                return Ok(new Response { Status = "Success", Message = "Beneficiary Records Saved Successfully." });
       
            }
            else 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Beneficiary Record failed, Pls Kindly check the Input" });
            }
            
        }

        [HttpGet]
        [Route("GetBeneficiaryByCode")]
        public async Task<IActionResult> GetBeneficiaryByCode (string BenefCode)
        {
            Beneficiary? beneficiary = await Task.Run(()=>  _unitOfwork.Beneficiary.Find(x => x.BeneficiaryCode == BenefCode).FirstOrDefault());
            if (beneficiary != null)
            {
                return Ok(beneficiary);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("ModifiedBeneficiary")]
        public async Task<IActionResult> ModifiedBeneficiary(Beneficiary beneficiary)
        {
            var oldData = _unitOfwork.Beneficiary.Find(x => x.BeneficiaryCode == beneficiary.BeneficiaryCode).FirstOrDefault();
            if (oldData != null)
            {
                _unitOfwork.Beneficiary.Remove(oldData);
                _unitOfwork.Beneficiary.Add(beneficiary);
            }

            int saveRecord = await Task.Run(() => _unitOfwork.Save());
            if (saveRecord != 0)
            {
                return Ok(new Response { Status = "Success", Message = "Beneficiary Records Successfully Updated" });

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Beneficiary Record failed, Pls Kindly check the Input" });
            }
        }

        #endregion



        [HttpPost]
        [Route("AddPaymentVoucher")]
        public async Task<IActionResult> AddPaymentVoucher (PaymentModel paymentModel)
        {
            Guid refon =  Guid.Parse(GeneralClass.PV_ID());
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
                Refno = await Task.Run(()=> GeneralClass.GetRefnumber(refon, "Refno", "CodingHead", true)),
                sendforapproval = paymentModel.sendforapproval,
                TotalAmount = paymentModel.TotalAmount,
                TransDate = paymentModel.TransDate,
                VAT = paymentModel.VAT,
                WHT = paymentModel.WHT
            };

            _unitOfwork.CodingHead.Add(head);

            if (paymentModel.CodingDetail != null)
            {
                foreach (CodingDetail detail in paymentModel.CodingDetail)
                {
                    detail.ColdingHeadId = HeadId;
                    detail.ColdingDetailsId = Guid.NewGuid();
                    _unitOfwork.CodingDetail.Add(detail);
                }

            }

            int record = await Task.Run(() => _unitOfwork.Save());

            if (record != 0)
            {
                return Ok(new Response { Status = "Success", Message = "Payment Voucher Records Saved Successfully." });

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Payment Voucher Record failed, Pls Kindly check the Input" });
            }

        }

        [HttpPost]
        [Route("ModifyPaymentVoucher")]
        public async Task<IActionResult> ModifyPaymentVoucher(PaymentModel paymentModel)
        {
            if (paymentModel.CodingDetail != null)
            {
                var details = await Task.Run(() => _unitOfwork.CodingDetail.Find(x => x.ColdingHeadId == paymentModel.ColdingHeadId).ToList());
                _unitOfwork.CodingDetail.RemoveRange(details);
            }

            CodingHead? headModify = await Task.Run(()=> _unitOfwork.CodingHead.Find(x => x.ColdingHeadId == paymentModel.ColdingHeadId).FirstOrDefault());
            
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
                _unitOfwork.CodingHead.Add(head);
                _unitOfwork.CodingDetail.AddRange(paymentModel.CodingDetail);

            }

            int record = await Task.Run(() => _unitOfwork.Save());

            if (record != 0)
            {
                return Ok(new Response { Status = "Success", Message = "Payment Voucher Records Updated Successfully." });

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Payment Voucher Record failed, Pls Kindly check the Input" });
            }


        }

        [HttpGet]
        [Route("GetPaymentVoucher")]
        public async Task<IActionResult> GetPaymentVoucher(string paymentId)
        {
            PaymentModel DataModel = new PaymentModel();
            Guid RealId = Guid.Parse(paymentId);
            var Head = await Task.Run(()=> _unitOfwork.CodingHead.Find(x => x.ColdingHeadId == RealId).FirstOrDefault());
            var Details = await Task.Run(()=> _unitOfwork.CodingDetail.Find(f => f.ColdingHeadId == RealId).ToList());
            if (Head != null && Details != null)
            {
                DataModel.WHT = Head.WHT;
                DataModel.sendforapproval = Head.sendforapproval;
                DataModel.AnotherCharges = Head.AnotherCharges;
                DataModel.Datecreated = Head.Datecreated;
                DataModel.Refno = Head.Refno;
                DataModel.createdby = Head.createdby;
                DataModel.BeneficiaryCode = Head.BeneficiaryCode;
                DataModel.approved = Head.approved;
                DataModel.ColdingHeadId = Head.ColdingHeadId;
                DataModel.currency = Head.currency;
                DataModel.Documentno = Head.Documentno;
                DataModel.EarlierAdditionorDeduction = Head.EarlierAdditionorDeduction;
                DataModel.TotalAmount = Head.TotalAmount;
                DataModel.TransDate = Head.TransDate;
                DataModel.VAT = Head.VAT;

                foreach (CodingDetail det in Details)
                {
                    DataModel.CodingDetail.Add(det);
                }

            }

            if(DataModel != null)
            {
               return Ok(DataModel);
            }
            else
            {
               return null;
            }
            



        }

    }
}
