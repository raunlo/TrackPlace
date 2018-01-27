using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TrackPlace.Models;
using TrackPlace.Repository.Interface;

namespace TrackPlace.Repository
{
    /// <summary>
    /// All logic for manipulatiing with truckinorder entity
    /// </summary>
    public class TruckInOrderRepository : EFRepository<TruckInOrder>, ITruckInOrderRepository
    {
        public TruckInOrderRepository(IContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Finds Truck driver for order
        /// </summary>
        /// <param name="currentOrder">
        /// Takes in TruckInOrder entity, which will be added.
        /// </param>
        /// <returns> 
        /// Returns Truck Id
        /// </returns>
        public int IsFree(TruckInOrder currentOrder)
        {
            using (var db = new TrackPlaceDbContext())
            {
                //ˇget driver who hasent make a single ride yet
                var truck = from b in db.Trucks
                    where !db.TruckInOrders.Any(id => id.TruckId == b.TruckId)
                    where currentOrder.Order.ProductCubage <=
                          b.TrailerCubage
                    where b.WeigthCapacity >= currentOrder.Order.ProductWeight &&
                          currentOrder.TypeOfTrucks == b.TypeOfTrucks
                    select b;
                if (truck.Any())
                {
                    return truck.First().TruckId;
                }

                // search form all drivers
                var datetime = DateTime.Now.AddHours(3);
                var truckId = from b in db.Trucks
                    where currentOrder.Order.ProductCubage <= b.TrailerCubage &&
                          b.WeigthCapacity >= currentOrder.Order.ProductWeight &&
                          currentOrder.TypeOfTrucks == b.TypeOfTrucks
                    select b.TruckId;

                if (truckId.Any())
                {
                    return truckId.First();
                }

                return 0;
            }
        }

        /// <summary>
        /// Get all acitve orders
        /// </summary>
        /// <param name="userId">Useraccont id</param>
        /// <returns> list of orders</returns>
        public List<TruckInOrder> GetAllTruckInOrders(int userId)
        {
            return RepositoryDbSet
                .Where(o => o.Order.UnloadingDateTime > DateTime.Now && o.Order.UserAccontId == userId || o.Truck.UserAccontId == userId).ToList();
        }

        /// <summary>
        /// Find if these DateTimes are correct
        /// </summary>
        /// <param name="currentOrder">Takes in TruInOrder </param>
        /// <returns> returns boolean if these DateTime is correct</returns>
        public bool AreDatesCorrect(TruckInOrder currentOrder)
        {
            var date = RepositoryDbSet.Where(d =>
                d.Order.LoadingDateTime > currentOrder.Order.LoadingDateTime ||
                d.Order.UnloadingDateTime < currentOrder.Order.UnloadingDateTime);
            if (date.Any() && Find(1) != null)
            {
                return true;
            }
            return false;
        }
    }
}