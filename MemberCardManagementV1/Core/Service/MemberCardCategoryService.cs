using AspnetRun.Infrastructure.Repository;
using Core.Service;
using MemberCardManagementV1.Core.Service.Interface;
using MemberCardManagementV1.Infrastructure.Repository;
using MemberCardManagementV1.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberCardManagementV1.Core.Service
{
    public class MemberCardCategoryService : BaseService<MemberCardCategory, MemberCardCategoryRepository>, IMemberCardCategoryService
    {
        public override void CustomEntityBeforeSave(MemberCardCategory entity)
        {
            base.CustomEntityBeforeSave(entity);

            if (entity != null)
            {
                var memberCards = GetAll();
                if (memberCards == null)
                {
                    memberCards = new List<MemberCardCategory>();
                }
                if (memberCards != null)
                {
                    if (entity.EditMode == MemberCardManagementV1.Constant.Enumeration.EditMode.Add)
                    {
                        entity.MemberCardCategoryID = memberCards.Count + 1;
                    }
                }
            }
        }

        public override void AfterGetEntity(MemberCardCategory entity)
        {
            base.AfterGetEntity(entity);

            if (entity != null && entity.ConfigID.HasValue && entity.ConfigID.Value > 0)
            {
                var configService = new ConfigService();
                var config = configService.Get(entity.ConfigID.Value);
                if (config != null)
                {
                    entity.ConfigName = config.ConfigName;
                }
            }
        }

        public List<MemberCardCategory> GetListMemberCardCategory()
        {
            return ((MemberCardCategoryRepository)_repository).GetListMemberCardCategory();
        }
    }
}