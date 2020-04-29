using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MemberCardManagementV1.Models;
using Models;
using Core.Service;
using Model;
using MemberCardManagementV1.Core.Service;

namespace MemberCardManagementV1.Controllers
{
    public class MemberCardsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private MemberCardService service = new MemberCardService();

        // GET: MemberCards
        public ActionResult Index()
        {
            try
            {
                return View(service.GetAll());
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: MemberCards/Details/5
        public ActionResult Details(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MemberCard memberCard = service.Get(id);
                if (memberCard != null)
                {
                    return View(memberCard);
                }
            }
            catch (Exception)
            {

            }
            return HttpNotFound();
        }

        // GET: MemberCards/Create
        public ActionResult Create()
        {
            try
            {
                List<SelectListItem> listItems = new List<SelectListItem>();
                var memberCardCategoryService = new MemberCardCategoryService();
                var memberCardCategories = memberCardCategoryService.GetAll();
                if (memberCardCategories != null && memberCardCategories.Count > 0)
                {
                    foreach(var item in memberCardCategories)
                    {
                        listItems.Add(new SelectListItem
                        {
                            Text = item.MemberCardCategoryName,
                            Value = item.MemberCardCategoryID.ToString(),
                            Selected = memberCardCategories.IndexOf(item) == 0? true: false
                        });
                    }
                }
                ViewBag.MemberCardCategories = listItems;
                return View();
            }
            catch (Exception ex)
            {

            }
            return View("Error");
        }

        // POST: MemberCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberCardID,MemberCardNo,Tel,MemberCardCategoryID,Point,Revenue,StartDate,EndDate,CreateDate,ModifyDate,CreateBy,ModifyBy")] MemberCard memberCard)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Add(memberCard);
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
            return View(memberCard);
        }

        // GET: MemberCards/Edit/5
        public ActionResult Edit(long? id)
        {
            MemberCard memberCard = null;
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
                var memberCardCategoryService = new MemberCardCategoryService();
                var memberCardCategories = memberCardCategoryService.GetAll();
                if (memberCardCategories != null && memberCardCategories.Count > 0)
                {
                    foreach (var item in memberCardCategories)
                    {
                        listItems.Add(new SelectListItem
                        {
                            Text = item.MemberCardCategoryName,
                            Value = item.MemberCardCategoryID.ToString(),
                            Selected = memberCardCategories.IndexOf(item) == 0 ? true : false
                        });
                    }
                }
                ViewBag.MemberCardCategories = listItems;
            }
            catch (Exception ex)
            {

            }
            
            return View(memberCard);
        }

        // POST: MemberCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberCardID,MemberCardNo,Tel,MemberCardCategoryID,Point,Revenue,StartDate,EndDate,CreateDate,ModifyDate,CreateBy,ModifyBy")] MemberCard memberCard)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Update(memberCard);
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
            
            return View(memberCard);
        }

        // GET: MemberCards/Delete/5
        public ActionResult Delete(long? id)
        {
            MemberCard memberCard = null;
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
            }
            catch (Exception ex)
            {

            }
            
            return View(memberCard);
        }

        // POST: MemberCards/Delete/5
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
