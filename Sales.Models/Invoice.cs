using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace Sales.Models
{
    public class Invoice : BaseEntityModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        [Required, MaxLength(100)]
        public string AuthorizationNumber { get; set; }
        [Required, MaxLength(100)]
        public string Series { get; set; }

        [Required, MaxLength(100)]
        public string ElectronicTaxDocuments { get; set; } //Documentos Tributarios Electrónicos - DTE

        [Required]
        public DateTime BroadcastDate { get; set; }

        [Required]
        public DateTime CertificationDate { get; set; }

        public ICollection<Purchase>? Purchases { get; set;}
    }
}
