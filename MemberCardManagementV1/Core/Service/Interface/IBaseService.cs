using AspnetRun.Infrastructure.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Interface
{
    public interface IBaseService<TEntity, TRepository>
        where TEntity : BaseEntity
        where TRepository : BaseRepository<TEntity>
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>đối tượng</returns>
        /// created by ntphong 26/4/2020
        ServiceResponse Add(TEntity entity);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>đối tượng</returns>
        /// created by ntphong 26/4/2020
        bool Delete(object id);

        /// <summary>
        /// Lấy 1 đối tượng theo id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>đối tượng</returns>
        /// created by ntphong 26/4/2020
        TEntity Get(object id);

        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns>list đối tượng</returns>
        /// created by ntphong 26/4/2020
        List<TEntity> GetAll();

        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>đối tượng</returns>
        /// created by ntphong 26/4/2020
        ServiceResponse Update(TEntity entity);

        /// <summary>
        /// Custom entity trước khi lưu
        /// </summary>
        /// <param name="entity"></param>
        void CustomEntityBeforeSave(TEntity entity);

        ServiceResponse ValidateEntityBeforeSave(TEntity entity);

        void AfterSaveEntity(TEntity entity);

        void AfterGetEntity(TEntity entity);

        void AfterGetListEntity(List<TEntity> entities);
    }
}
