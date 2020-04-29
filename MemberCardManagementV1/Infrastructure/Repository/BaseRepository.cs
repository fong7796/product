using Infrastructure.Repository.Interface;
using MemberCardManagementV1.Models;
using Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AspnetRun.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : BaseEntity
    {
        //protected readonly ApplicationDbContext _context;
        public BaseRepository()
        {
            //_context = context;
        }
        public bool Add(TEntity entity)
        {
            bool result = false;
            using (var context = new ApplicationDbContext())
            {
                context.Set<TEntity>().Add(entity);
                result = context.SaveChanges() > 0;
            }
            return result;
        }

        public bool Delete(object id)
        {
            bool result = false;
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Set<TEntity>().Find(id);
                if (entity != null)
                {
                    context.Set<TEntity>().Remove(entity);
                    result = context.SaveChanges() > 0;
                }
            }

            return result;
        }

        public TEntity Get(object id)
        {
            TEntity entity = null;
            using (var context = new ApplicationDbContext())
            {
                entity = context.Set<TEntity>().Find(id);
            }
            return entity;
        }

        public List<TEntity> GetAll()
        {
            List<TEntity> entities = new List<TEntity>();
            using (var context = new ApplicationDbContext())
            {
                entities = context.Set<TEntity>().ToListAsync().Result;
            }
            return entities;
        }

        public bool Update(TEntity entity)
        {
            bool result = false;
            using (var context = new ApplicationDbContext())
            {
                var obj = context.Entry(entity);
                obj.State = EntityState.Modified;
                result = context.SaveChanges() > 0;
            }
            return result;
        }
    }
}
