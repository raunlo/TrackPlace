using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackPlace.Models
{
    /// <summary>
    /// Bill entity
    /// </summary>
    public class Bill
    {
        public int BillId { get; set; }
        [Required]
        public int BillNumber { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime? BillDateTime { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int UserAccontId { get; set; }
        public virtual UserAccont UserAccont { get; set; }
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }       
    }
}
