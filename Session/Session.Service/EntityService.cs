using Session.Model;
using Session.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace Session.Service
{

    /// <summary>
    /// generic 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks></remarks>
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        IUnitOfWork _unitOfWork;
        IGenericRepository<T> _repository;

        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        /// <summary>
        /// create new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks></remarks>
        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _unitOfWork.Commit();
        }


        /// <summary>
        /// update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks></remarks>
        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _repository.Edit(entity);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks></remarks>
        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// get all entities
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }

}
