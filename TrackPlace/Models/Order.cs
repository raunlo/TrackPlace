using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackPlace.Models
{
    /// <summary>
    /// Entity for Order
    /// </summary>
    public  class Order
    {
        public int OrderId { get; set; }
        [Required]
        public string LoadingCounty { get; set; }
        [Required]
        public string LoadingAddress { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? LoadingDateTime { get; set; }
        public string LoadingCity { get; set; }
        [Required]
        public int LoadingHouseNumber { get; set; }     
        [Required]
        public string UnloadingCounty { get; set; }
        [Required]
        public string UnloadingAddress { get; set; }
        public string UnloadingCity { get; set; }       
        [Required]
        public int UnloadingHouseNumber { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? UnloadingDateTime { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int? ProductLength { get; set; }
        public int? ProductWidth { get; set; }
        public int? ProductHeight { get; set; }
        public int ProductWeight { get; set; }
        public int? ProductCubage { get; set; }
        public virtual List<TruckInOrder> TruckInOrders { get; set; }      
        public int? UserAccontId { get; set; }
        public virtual UserAccont UserAccont { get; set; }
        public string Comments { get; set; }

    }
}
