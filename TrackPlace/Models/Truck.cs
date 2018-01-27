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
    /// Entiy for list of trucks
    /// </summary>
   public  class Truck
    {        
        public int TruckId { get; set; }
        [Required]
        public string BodyType { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? FirstRegDateTime { get; set; }
        [Required]
        public int? UnladenWeigth { get; set; }
        [Required]
        public int? MaximumWeight { get; set; }
        [Required]
        public int? WeigthCapacity { get; set; }
        [Required]
        public int? TrailerLength { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public int? TrailerWidth { get; set; }
        [Required]
        public int? TrailerHeight { get; set; }
        [Required]
        public string Fuel { get; set; }
        [Required]
        public string VinCode { get; set; }
        [Required]
        public int? TrailerCubage { get; set; }
        [Required]
        public int? UserAccontId { get; set; }
        
        public virtual UserAccont UserAccont { get; set; }
        [Required]
    
        public string TypeOfTrucks { get; set; }
       
        public virtual List<TruckInOrder> TruckInOrders { get; set; }

        
    }
}
