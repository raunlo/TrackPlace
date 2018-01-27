using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackPlace.Models;
using TrackPlace.Repository.Interface;

namespace TrackPlace.Repository
{
    /// <summary>
    /// UserAccont Entity operations class
    /// </summary>
    public class UserAccontRepository : EFRepository<UserAccont>, IUserAccontRepository
    {
        public UserAccontRepository(IContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Checks if login iputs are corrext
        /// </summary>
        /// <param name="password">
        /// Accont Password
        /// </param>
        /// <param name="username"></param>
        /// <returns>
        /// Username for login
        /// </returns>
        public int Login(string password, string username)
        {
            using (TrackPlaceDbContext db = new TrackPlaceDbContext())
            {
                var user = from p in db.UserAcconts
                    where p.Password.PasswordName == password && p.Password.PasswordId == p.UserAccontId
                    where p.Person.EMailAddress == username && p.Person.PersonId == p.UserAccontId
                    select p.UserAccontId;

                if (user.Any())
                {
                    return user.First();
                }
                return 0;
            }
        }
        /// <summary>
        /// Finds user In database
        /// </summary>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public int find(string password, string username)
        {
            using (TrackPlaceDbContext db = new TrackPlaceDbContext())
            {
                var user = from p in db.UserAcconts
                    where p.Password.PasswordName == password && p.Password.PasswordId == p.UserAccontId
                    where p.Person.EMailAddress == username && p.Person.PersonId == p.UserAccontId
                    select p.UserAccontId;
                return user.First();
            }
           
        }
       
        /// <summary>
        /// Checks if user exsists already in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IfExsists(UserAccont user)
        {
            using (TrackPlaceDbContext db = new TrackPlaceDbContext())
            {
                
                var accont = from p in db.UserAcconts
                    where user.Person.FirstName == p.Person.FirstName
                    where user.Person.LastName == p.Person.LastName
                    where user.Person.EMailAddress == p.Person.EMailAddress
                    select p;
                if (accont.Any())
                {
                    return true;
                }
                return false;
            }
        }
    }
}