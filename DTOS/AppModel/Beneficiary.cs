using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.AppModel
{
    public class Beneficiary
    {
        [Key]
        public Guid BeneficiaryID { get; set; }
        [Required]
        public string? BeneficiaryCode { get; set; }
        [Required]
        public  string? BeneficiaryName { get; set; }
        [Required]
        public string? Contactperson { get; set; }
        public string? RCno { get; set; }
        public string? Address { get; set; }
        [Phone]
        public string? phoneno { get; set; }
        public string? fax { get; set; }
        public string? Tinno { get; set; }
        public string? Vatno { get; set; }
        [EmailAddress]
        public string? email { get; set; }
        public string? Bankname { get; set; }
        public string? Bankacctno { get; set; }
        public string? Sortcode { get; set; }
        public string? SetupOption {get; set; }
        [Required]
        public string? Createdby { get; set; }
        [Required]
        public DateTime Datecreated { get; set; }
                
    }
}
