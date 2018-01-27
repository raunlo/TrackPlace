using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackPlace.Models;

namespace TrackPlace.Repository
{
    /// <summary>
    /// Bill  repository for adding bills to database
    /// </summary>
   public class BillRepository:EFRepository<Bill>
    {
        public BillRepository(IContext dbContext) : base(dbContext)
        {
        }
     
    }
}
