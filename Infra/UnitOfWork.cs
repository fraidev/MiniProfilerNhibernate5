﻿using System;
using System.Linq;
using NHibernate;

namespace MiniProfilerNhibernate5.Infra
{
    public interface IUnitOfWork
    {
        void Save<TEntity>(TEntity entity) where TEntity : class;
        void Update(object entity);
        void Delete(object entity);
        void Flush();
        T GetById<T>(Guid id);
        IQueryable<T> Query<T>();
        ISession GetSession();
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;

        public UnitOfWork(ISession session)
        {
            _session = session;
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            _session.BeginTransaction();
            _session.Save(entity);
            _session.Transaction.Commit();
        }

        public void Update(object entity)
        {
            _session.BeginTransaction();
            _session.Update(entity);
            _session.Transaction.Commit();
        }

        public void Delete(object entity)
        {
            _session.BeginTransaction();
            _session.Delete(entity);
            _session.Transaction.Commit();
        }

        public void Flush()
        {
            _session.Flush();
        }

        public T GetById<T>(Guid id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public ISession GetSession()
        {
            return _session;
        }
    }
}