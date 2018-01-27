using System.Linq;
using TrackPlace.Models;
using TrackPlace.Repository.Interface;

namespace TrackPlace.Repository
{
    /// <summary>
    /// Logic for manipulating with Truck entity.
    /// </summary>
   public class TruckRepository:EFRepository<Truck>, ITruckRepository
    {
        public TruckRepository(IContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Finds if the driver exsits or not
        /// </summary>
        /// <param name="id">
        /// Takes in UserAccont id and checks if this user has already registred truck
        /// </param>
        /// <returns>
        /// Returns boolean, if user has registred truck.
        /// </returns>
        public bool IfExsists( int id)
       {
           var truck = RepositoryDbSet.FirstOrDefault(p => p.UserAccontId == id);
           return truck != null;
       }
    }
}
