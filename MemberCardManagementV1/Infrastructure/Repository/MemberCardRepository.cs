using Infrastructure.Repository.Interface;
using MemberCardManagementV1.Models;
using Model;
using Models;
using System.Data.Entity;

namespace AspnetRun.Infrastructure.Repository
{
    public class MemberCardRepository : BaseRepository<MemberCard>, IMemberCardRepository
    {
        //public override bool Update(MemberCard entity)
        //{
        //    bool result = false;
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var obj = context.MemberCards.Find(entity.MemberCardID);
        //        context.Entry(obj).State = EntityState.Modified;
        //        //obj.State = EntityState.Modified;
        //        result = context.SaveChanges() > 0;
        //    }
        //    return result;
        //}
    }
}
