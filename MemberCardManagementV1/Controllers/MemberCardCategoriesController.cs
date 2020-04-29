using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MemberCardManagementV1.Core.Service;
using MemberCardManagementV1.Models;
using Model;

namespace MemberCardManagementV1.Controllers
{
    public class MemberCardCategoriesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private MemberCardCategoryService service = new MemberCardCategoryService();

        // GET: MemberCardCategories
        public ActionResult Index()
        {
            try
            {
                return View(service.GetListMemberCardCategory());
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: MemberCardCategories/Details/5
        public ActionResult Details(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MemberCardCategory memberCardCategory = service.Get(id);
                if (memberCardCategory != null)
                {
                    return View(memberCardCategory);
                }
            }
            catch (Exception ex)
            {

            }
            return HttpNotFound();
        }

        // GET: MemberCardCategories/Create
        public ActionResult Create()
        {
            try
            {
                List<SelectListItem> listItems = new List<SelectListItem>();
                var configService = new ConfigService();
                var configs = configService.GetAll();
                if (configs != null && configs.Count > 0)
                {
                    foreach (var item in configs)
                    {
                        listItems.Add(new SelectListItem
                        {
                            Text = item.ConfigName,
                            Value = item.ConfigID.ToString(),
                            Selected = configs.IndexOf(item) == 0 ? true : false
                        });
                    }
                }
                ViewBag.Configs = listItems;
            }
            catch (Exception ex)
            {
        
            }
            
            return View();
        }

        // POST: MemberCardCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberCardCategoryID,MemberCardCategoryName,PromotionRevenue,Duration,DiscountRate,ConfigID,CreateDate,ModifyDate,CreateBy,ModifyBy")] MemberCardCategory memberCardCategory)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Add(memberCardCategory);
                    if (res != null && !res.IsSuccess)
                    {
                        ViewBag.Error = res.Message;
                    }
                }
                else
                {
                    res.IsSuccess = false;
                }
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                        
            }
            
            return View(memberCardCategory);
        }

        // GET: MemberCardCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            MemberCardCategory memberCard = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                memberCard = service.Get(id);
                if (memberCard == null)
                {
                    return HttpNotFound();
                }

                List<SelectListItem> listItems = new List<SelectListItem>();
                var configService = new ConfigService();
                var configs = configService.GetAll();
                if (configs != null && configs.Count > 0)
                {
                    foreach (var item in configs)
                    {
                        listItems.Add(new SelectListItem
                        {
                            Text = item.ConfigName,
                            Value = item.ConfigID.ToString(),
                            Selected = configs.IndexOf(item) == 0 ? true : false
                        });
                    }
                }
                ViewBag.Configs = listItems;
            }
            catch (Exception ex)
            {

            }
            
            return View(memberCard);
        }

        // POST: MemberCardCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberCardCategoryID,MemberCardCategoryName,PromotionRevenue,Duration,DiscountRate,ConfigID,CreateDate,ModifyDate,CreateBy,ModifyBy")] MemberCardCategory memberCardCategory)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Update(memberCardCategory);
                    if (res != null && !res.IsSuccess)
                    {
                        ViewBag.Error = res.Message;
                    }
                }
                else
                {
                    res.IsSuccess = false;
                }
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
            
            return View(memberCardCategory);
        }

        // GET: MemberCardCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            MemberCardCategory memberCardCategory = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                memberCardCategory = service.Get(id);
                if (memberCardCategory == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {

            }
            
            return View(memberCardCategory);
        }

        // POST: MemberCardCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                bool result = false;
                result = service.Delete(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
            
            return View("Error");
        }
    }
}
