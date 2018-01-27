using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackPlace.Models;

namespace TrackPlace.Repository.Interface
{
    interface IUserAccontRepository
    {
        int Login(string password, string username);
        int find(string password, string username);
        bool IfExsists(UserAccont user);
    }
}
