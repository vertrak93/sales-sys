using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefreshTokenId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public string Token { get; set; }

        [Required, DefaultValue(true)]
        public bool Active { get; set; }

        public virtual User User { get; set; }
    }
}
