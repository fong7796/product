using Core.Service;
using MemberCardManagementV1.Core.Service.Interface;
using MemberCardManagementV1.Infrastructure.Repository;
using MemberCardManagementV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberCardManagementV1.Core.Service
{
    public class ConfigService : BaseService<Config, ConfigRepository>, IConfigService
    {
        public override void CustomEntityBeforeSave(Config entity)
        {
            base.CustomEntityBeforeSave(entity);

            if (entity != null)
            {
                var memberCards = GetAll();
                if (memberCards == null)
                {
                    memberCards = new List<Config>();
                }
                if (memberCards != null)
                {
                    if (entity.EditMode == MemberCardManagementV1.Constant.Enumeration.EditMode.Add)
                    {
                        entity.ConfigID = memberCards.Count + 1;
                    }
                }
            }
        }

        public override void AfterSaveEntity(Config entity)
        {
            base.AfterSaveEntity(entity);

            if (entity != null && entity.IsDefault.HasValue && entity.IsDefault.Value)
            {
                var configs = GetAll();
                if (configs != null && configs.Count > 0)
                {
                    var defaultConfig = configs.FirstOrDefault(x => x.IsDefault.HasValue && x.IsDefault.Value && x.ConfigID != entity.ConfigID);
                    if (defaultConfig != null)
                    {
                        defaultConfig.IsDefault = false;
                        Update(defaultConfig);
                    }
                }
            }
        }
    }
}