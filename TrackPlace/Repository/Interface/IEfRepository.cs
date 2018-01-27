﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackPlace.Repository.Interface
{
    public interface IEFRepository<TEntity>
    {
        //IQueryable<TEntity> All { get; }
        List<TEntity> All { get; }
        TEntity Find(int id);

        void Remove(int id);
        void Remove(TEntity entity);

        TEntity Add(TEntity entity);

        void Update(TEntity entity);

        int SaveChanges();
    }
}
