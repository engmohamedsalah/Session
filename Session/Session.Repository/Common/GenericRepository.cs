using Session.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Session.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        protected GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.AsEnumerable<T>();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {

            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        /// <summary>
        /// Add new record in entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        /// <summary>
        /// delete records
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        /// <summary>
        /// make the state as modified 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }


        /// <summary>
        /// commit to database
        /// </summary>
        public virtual void Save()
        {
            _entities.SaveChanges();
        }
        /// <summary>
        /// remove all records
        /// </summary>
        public virtual void RemoveAll()
        {
            foreach (var entity in _dbset)
                _dbset.Remove(entity);
            _entities.SaveChanges();
        }
    }
}
