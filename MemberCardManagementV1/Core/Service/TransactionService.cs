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
    public class TransactionService : BaseService<Transaction, TransactionRepository>, ITransactionService
    {
        public override ServiceResponse ValidateEntityBeforeSave(Transaction entity)
        {
            var res = base.ValidateEntityBeforeSave(entity);

            if (entity != null)
            {
                if (res != null && res.IsSuccess)
                {
                    var memberCardService = new MemberCardService();
                    var memberCard = memberCardService.Get(entity.MemberCardID);
                    if (memberCard == null)
                    {
                        res.IsSuccess = false;
                        res.Message = "Thẻ tích điểm không tồn tại. Vui lòng kiểm tra lại";
                    }
                }
            }

            return res;
        }

        public override void CustomEntityBeforeSave(Transaction entity)
        {
            base.CustomEntityBeforeSave(entity);

            /*
             * custom giao dịch trước khi lưu
             * => lấy ra thông tin thẻ
             *  => lấy ra hạng thẻ
             *      => nếu hạng thẻ đó có config riêng => lấy ra config riêng
             *      => nếu không có config riêng thì lấy config mặc định (config mặc định là config IsDefault = true hoặc config đầu tiên)
             *  => lấy được config thì quy đổi ra số điểm của giao dịch đó
             */

            if (entity != null)
            {
                var transactions = GetAll();
                if (transactions == null)
                {
                    transactions = new List<Transaction>();
                }
                if (transactions != null)
                {
                    if (entity.EditMode == MemberCardManagementV1.Constant.Enumeration.EditMode.Add)
                    {
                        entity.TransactionID = transactions.Count + 1;
                    }
                }

                var memberCardService = new MemberCardService();
                var memberCard = memberCardService.Get(entity.MemberCardID);
                if (memberCard != null)
                {
                    Config config = null;
                    var configService = new ConfigService();
                    if (memberCard.MemberCardCategoryID != null && memberCard.MemberCardCategoryID > 0)
                    {
                        var memberCardCategoryService = new MemberCardCategoryService();
                        var memberCardCategory = memberCardCategoryService.Get(memberCard.MemberCardCategoryID);
                        if (memberCardCategory != null)
                        {
                            if (memberCardCategory.ConfigID != null && memberCardCategory.ConfigID > 0)
                            {
                                config = configService.Get(memberCardCategory.ConfigID);
                            }

                            if (memberCardCategory.DiscountRate.HasValue)
                            {
                                entity.Revenue = entity.Revenue - (entity.Revenue * (memberCardCategory.DiscountRate.Value/100));
                            }
                        }
                    }
                    if (config == null)
                    {
                        var configs = configService.GetAll();
                        if (configs != null && configs.Count > 0)
                        {
                            config = configs.FirstOrDefault(x => x.IsDefault.HasValue && x.IsDefault.Value);
                            if (config == null)
                            {
                                config = configs.FirstOrDefault();
                            }
                        }
                    }

                    if (config != null && config.ConfigValue.HasValue && config.ConfigValue.Value != 0)
                    {
                        entity.Point = entity.Revenue / config.ConfigValue.Value;
                    }
                    else
                    {
                        entity.Point = 0;
                    }
                }
            }
        }

        public override void AfterSaveEntity(Transaction entity)
        {
            base.AfterSaveEntity(entity);

            //xử lý tích điểm cho khách hàng
            if (entity != null)
            {
                var memberCardService = new MemberCardService();
                var memberCard = memberCardService.Get(entity.MemberCardID);
                if (memberCard != null && memberCard.EndDate.HasValue && memberCard.EndDate.Value <= DateTime.Now)
                {
                    memberCard.Point += entity.Point;
                    memberCard.Revenue += entity.Revenue;

                    var memberCardCategoryService = new MemberCardCategoryService();
                    var memberCardCategories = memberCardCategoryService.GetAll();
                    if (memberCardCategories != null && memberCardCategories.Count > 0)
                    {
                        var lstCategory = memberCardCategories.Where(x => x.PromotionRevenue <= memberCard.Revenue && x.MemberCardCategoryID != memberCard.MemberCardCategoryID).OrderByDescending(x => x.PromotionRevenue).ToList();
                        if (lstCategory != null && lstCategory.Count > 0)
                        {
                            var cate = lstCategory.FirstOrDefault();
                            if (cate != null)
                            {
                                memberCard.MemberCardCategoryID = cate.MemberCardCategoryID;
                                memberCard.StartDate = DateTime.Now;
                                if (cate.Duration.HasValue)
                                {
                                    memberCard.EndDate = memberCard.StartDate.Value.AddDays(cate.Duration.Value);
                                }
                            }
                        }
                    }

                    memberCardService.Update(memberCard);
                }
            }
        }

        public override void AfterGetListEntity(List<Transaction> entities)
        {
            base.AfterGetListEntity(entities);

            if (entities != null && entities.Count > 0)
            {
                var memberCardService = new MemberCardService();
                var memberCards = memberCardService.GetAll();

                if (memberCards != null && memberCards.Count > 0)
                {
                    foreach (var item in entities)
                    {
                        if (item != null && item.MemberCardID > 0)
                        {
                            var memberCard = memberCards.FirstOrDefault(x => x.MemberCardID == item.MemberCardID);
                            if (memberCard != null)
                            {
                                item.MemberCardNo = memberCard.MemberCardNo;
                            }
                        }
                    }
                }
            }
        }

        public override void AfterGetEntity(Transaction entity)
        {
            base.AfterGetEntity(entity);

            if (entity != null && entity.MemberCardID > 0)
            {
                var memberCardService = new MemberCardService();
                var memberCard = memberCardService.Get(entity.MemberCardID);
                if (memberCard != null)
                {
                    entity.MemberCardNo = memberCard.MemberCardNo;
                }
            }
        }
    }
}