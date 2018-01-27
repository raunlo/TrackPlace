using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackPlace.Models;

namespace TrackPlace
{
    /// <summary>
    /// DBContext for entities
    /// </summary>
    public class TrackPlaceDbContext:DbContext, IContext
    {
        public DbSet<Bill> Bills { get; set; }    
        public DbSet<Order> Orders { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Person> Persons { get; set; }     
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckInOrder> TruckInOrders { get; set; }
        public DbSet<UserAccont> UserAcconts { get; set; }

        public TrackPlaceDbContext() : base("name=TrackPlaceSql")
        {
            
        }
    }
}
