using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public decimal Balance { get; set; }
        [StringLength(25)]
        public string Password { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
