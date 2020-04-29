using AspnetRun.Infrastructure.Repository;
using MemberCardManagementV1.Infrastructure.Repository.Interface;
using MemberCardManagementV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberCardManagementV1.Infrastructure.Repository
{
    public class MemberCardCategoryRepository : BaseRepository<MemberCardCategory>, IMemberCardCategoryRepository
    {
        public List<MemberCardCategory> GetListMemberCardCategory()
        {
            List<MemberCardCategory> entities = new List<MemberCardCategory>();
            List<Config> configs = new List<Config>();
            using (var context = new ApplicationDbContext())
            {
                entities = context.MemberCardCategories.ToList();
                configs = context.Configs.ToList();
            }
            if (entities != null && entities.Count > 0 && configs != null && configs.Count > 0)
            {
                foreach (var item in entities)
                {
                    if (item != null && item.ConfigID.HasValue && item.ConfigID.Value > 0)
                    {
                        var config = configs.FirstOrDefault(x => x.ConfigID == item.ConfigID.Value);
                        if (config != null)
                        {
                            item.ConfigName = config.ConfigName;
                        }
                    }
                }
            }
            return entities;
        }
    }
}