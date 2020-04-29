using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interface
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>đối tượng</returns>
        /// created by ntphong 26/4/2020
        bool Add(TEntity entity);

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
        bool Update(TEntity entity);
    }
}
