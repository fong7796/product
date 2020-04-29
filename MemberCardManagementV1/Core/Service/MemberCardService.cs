using AspnetRun.Infrastructure.Repository;
using Core.Service.Interface;
using MemberCardManagementV1.Core.Service;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Service
{
    public class MemberCardService : BaseService<MemberCard, MemberCardRepository>, IMemberCardService
    {
        public override ServiceResponse ValidateEntityBeforeSave(MemberCard entity)
        {
            var res = base.ValidateEntityBeforeSave(entity);

            if (entity != null) {
                if (res != null && res.IsSuccess)
                {
                    var memberCards = GetAll();
                    if (memberCards != null && memberCards.Count > 0)
                    {
                        var existCardNo = memberCards.FirstOrDefault(x => string.Equals(x.MemberCardNo, entity.MemberCardNo, StringComparison.OrdinalIgnoreCase) && x.MemberCardID != entity.MemberCardID);
                        if (existCardNo != null)
                        {
                            res.IsSuccess = false;
                            res.Message = "Mã thẻ đã tồn tại. Vui lòng kiểm tra lại";
                        }
                        if (res.IsSuccess)
                        {
                            var existTel = memberCards.FirstOrDefault(x => string.Equals(x.Tel, entity.Tel, StringComparison.OrdinalIgnoreCase) && x.MemberCardID != entity.MemberCardID);
                            if (existTel != null)
                            {
                                res.IsSuccess = false;
                                res.Message = "Số điện thoại đã tồn tại. Vui lòng kiểm tra lại";
                            }
                        }
                    }

                    if (res != null && res.IsSuccess)
                    {
                        if (entity.MemberCardCategoryID != null && entity.MemberCardCategoryID > 0)
                        {
                            var memberCardCategoryService = new MemberCardCategoryService();
                            var memberCardCategory = memberCardCategoryService.Get(entity.MemberCardCategoryID);
                            if (memberCardCategory == null)
                            {
                                res.IsSuccess = false;
                                res.Message = "Hạng thẻ không tồn tại. Vui lòng kiểm tra lại";
                            }
                        }
                    }
                }
            }

            return res;
        }

        public override void CustomEntityBeforeSave(MemberCard entity)
        {
            base.CustomEntityBeforeSave(entity);

            var dafaultPrefixCardNo = "LT";

            if (entity != null)
            {
                var memberCards = GetAll();
                if (memberCards == null)
                {
                    memberCards = new List<MemberCard>();
                }
                if (memberCards != null)
                {
                    if (entity.EditMode == MemberCardManagementV1.Constant.Enumeration.EditMode.Add)
                    {
                        entity.MemberCardID = memberCards.Count + 1;
                    }
                    var existCardNo = memberCards.FirstOrDefault(x => string.Equals(x.MemberCardNo, entity.MemberCardNo, StringComparison.OrdinalIgnoreCase) && x.MemberCardID != entity.MemberCardID);
                    if (existCardNo != null)
                    {
                        entity.MemberCardNo = string.Empty;
                    }
                    if (string.IsNullOrWhiteSpace(entity.MemberCardNo))
                    {
                        entity.MemberCardNo = dafaultPrefixCardNo + entity.MemberCardID.ToString().PadLeft(4, '0');
                    }
                }

                if (entity.MemberCardCategoryID.HasValue && entity.MemberCardCategoryID.Value > 0)
                {
                    var memberCardCategoryService = new MemberCardCategoryService();
                    var memberCardCategory = memberCardCategoryService.Get(entity.MemberCardCategoryID.Value);
                    if (memberCardCategory != null && memberCardCategory.Duration.HasValue)
                    {
                        //thêm mới 
                        if (entity.EditMode == MemberCardManagementV1.Constant.Enumeration.EditMode.Add)
                        {
                            entity.StartDate = DateTime.Now;
                            entity.EndDate = entity.StartDate.Value.AddDays(memberCardCategory.Duration.Value);
                        }

                        //sửa
                        else if (entity.EditMode == MemberCardManagementV1.Constant.Enumeration.EditMode.Edit)
                        {
                            var oldMemberCard = Get(entity.MemberCardID);
                            if (oldMemberCard != null && oldMemberCard.MemberCardCategoryID != entity.MemberCardCategoryID)
                            {
                                entity.StartDate = DateTime.Now;
                                entity.EndDate = entity.StartDate.Value.AddDays(memberCardCategory.Duration.Value);
                            }
                        }
                    }
                }
            }
        }

        public override void AfterGetListEntity(List<MemberCard> entities)
        {
            base.AfterGetListEntity(entities);

            if (entities != null && entities.Count > 0)
            {
                var memberCardCategoryService = new MemberCardCategoryService();
                var memberCardCategories = memberCardCategoryService.GetAll();
                
                if (memberCardCategories != null && memberCardCategories.Count > 0)
                {
                    foreach (var item in entities)
                    {
                        if (item != null && item.MemberCardCategoryID.HasValue && item.MemberCardCategoryID.Value > 0)
                        {
                            var memberCardCategory = memberCardCategories.FirstOrDefault(x => x.MemberCardCategoryID == item.MemberCardCategoryID.Value);
                            if (memberCardCategory != null)
                            {
                                item.MemberCardCategoryName = memberCardCategory.MemberCardCategoryName;
                            }
                        }
                    }
                }
            }
        }

        public override void AfterGetEntity(MemberCard entity)
        {
            base.AfterGetEntity(entity);

            if (entity != null && entity.MemberCardCategoryID.HasValue && entity.MemberCardCategoryID.Value > 0)
            {
                var memberCardCategoryService = new MemberCardCategoryService();
                var memberCardCategory = memberCardCategoryService.Get(entity.MemberCardCategoryID.Value);
                if (memberCardCategory != null)
                {
                    entity.MemberCardCategoryName = memberCardCategory.MemberCardCategoryName;
                }
            }
        }
    }
}
