using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackPlace.Models
{
    /// <summary>
    /// Entity to list, which truck has done what
    /// </summary>
   public  class TruckInOrder
    {
        public int TruckInOrderId { get; set; }
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required]
        public int TruckId { get; set; }
        public virtual Truck Truck { get; set; }
        [Required]
        public string TypeOfTrucks { get; set; }
    }
}
