using AspnetRun.Infrastructure.Repository;
using Core.Service.Interface;
using Infrastructure.Repository.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static MemberCardManagementV1.Constant.Enumeration;

namespace Core.Service
{
    public class BaseService<TEntity, TRepository> : IBaseService<TEntity, TRepository>
        where TEntity : BaseEntity
        where TRepository : BaseRepository<TEntity>
    {
        protected IBaseRepository<TEntity> _repository;

        public BaseService ()
        {
            _repository = Activator.CreateInstance<TRepository>();
        }

        public ServiceResponse Add(TEntity entity)
        {
            var res = new ServiceResponse();
            entity.EditMode = EditMode.Add;
            res = ValidateEntityBeforeSave(entity);
            if (res != null && res.IsSuccess)
            {
                CustomEntityBeforeSave(entity);
                res.IsSuccess = _repository.Add(entity);
            }
            if (res.IsSuccess)
            {
                AfterSaveEntity(entity);
            }
            return res;
        }

        public bool Delete(object id)
        {
            return _repository.Delete(id);
        }

        public TEntity Get(object id)
        {
            var entity = _repository.Get(id);
            AfterGetEntity(entity);
            return entity;
        }

        public List<TEntity> GetAll()
        {
            var entities = _repository.GetAll();
            AfterGetListEntity(entities);
            return entities;
        }

        public ServiceResponse Update(TEntity entity)
        {
            var res = new ServiceResponse();
            entity.EditMode = EditMode.Edit;
            res = ValidateEntityBeforeSave(entity);
            if (res != null && res.IsSuccess)
            {
                CustomEntityBeforeSave(entity);
                res.IsSuccess = _repository.Update(entity);
            }
            if (res.IsSuccess)
            {
                AfterSaveEntity(entity);
            }
            return res;
        }

        public virtual ServiceResponse ValidateEntityBeforeSave(TEntity entity)
        {
            return new ServiceResponse();
        }

        public virtual void CustomEntityBeforeSave(TEntity entity)
        {
            if (entity.EditMode == EditMode.Add)
            {
                entity.CreateDate = DateTime.Now;
                entity.ModifyDate = DateTime.Now;
            }
            else if(entity.EditMode == EditMode.Edit)
            {
                entity.ModifyDate = DateTime.Now;
            }
        }

        public virtual void AfterSaveEntity(TEntity entity)
        {
            
        }

        public virtual void AfterGetEntity(TEntity entity)
        {

        }

        public virtual void AfterGetListEntity(List<TEntity> entities)
        {

        }
    }
}
