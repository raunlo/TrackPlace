using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackPlace.Models;

namespace TrackPlace.Repository.Interface
{
    interface ITruckInOrderRepository
    {
        int IsFree(TruckInOrder currentOrder);
    }
}
