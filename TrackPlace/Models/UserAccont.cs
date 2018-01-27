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
    /// Useraccont entity
    /// </summary>
    public class UserAccont
    {   
       
        public int UserAccontId { get; set; }
        [Required]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        [Required]
        public int PasswordId { get; set; }
        public virtual Password Password { get; set; }
        public int OrderId { get; set; }
        public virtual List<Order> Orders { get; set; }
        public int BillId { get; set; }
        public virtual List<Bill> Bills { get; set; }
    }
}
