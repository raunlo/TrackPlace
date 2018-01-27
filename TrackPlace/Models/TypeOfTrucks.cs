using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackPlace.Models
{
    /// <summary>
    /// Enum of trucks
    /// </summary>
   public enum TypeOfTrucks
    {
        PoolHaagis = 1,        
        Täishaagis = 2,
        Megahaagisega=3,
        Tagaluukauto=4,
        keskteikhaagis = 5
    }
}
