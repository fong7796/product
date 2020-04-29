using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Service;
using MemberCardManagementV1.Core.Service;
using MemberCardManagementV1.Models;
using Model;

namespace MemberCardManagementV1.Controllers
{
    public class TransactionsController : Controller
    {
        private TransactionService service = new TransactionService();

        // GET: Transactions
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

        // GET: Transactions/Details/5
        public ActionResult Details(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Transaction memberCard = service.Get(id);
                if (memberCard != null)
                {
                    return View(memberCard);
                }
            }
            catch (Exception ex)
            {

            }
            return HttpNotFound();
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            try
            {
                List<SelectListItem> listItems = new List<SelectListItem>();
                var memberCardService = new MemberCardService();
                var memberCards = memberCardService.GetAll();
                if (memberCards != null && memberCards.Count > 0)
                {
                    foreach (var item in memberCards)
                    {
                        listItems.Add(new SelectListItem
                        {
                            Text = item.MemberCardNo,
                            Value = item.MemberCardID.ToString(),
                            Selected = memberCards.IndexOf(item) == 0 ? true : false
                        });
                    }
                }
                ViewBag.MemberCards = listItems;
                return View();
            }
            catch (Exception ex)
            {

            }
            return View("Error");
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,MemberCardID,Point,Revenue,CreateDate,ModifyDate,CreateBy,ModifyBy")] Transaction transaction)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Add(transaction);
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

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(long? id)
        {
            Transaction memberCard = null;
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
                var memberCardService = new MemberCardService();
                var memberCards = memberCardService.GetAll();
                if (memberCards != null && memberCards.Count > 0)
                {
                    foreach (var item in memberCards)
                    {
                        listItems.Add(new SelectListItem
                        {
                            Text = item.MemberCardNo,
                            Value = item.MemberCardID.ToString(),
                            Selected = memberCards.IndexOf(item) == 0 ? true : false
                        });
                    }
                }
                ViewBag.MemberCards = listItems;
            }
            catch (Exception ex)
            {

            }

            return View(memberCard);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,MemberCardID,Point,Revenue,CreateDate,ModifyDate,CreateBy,ModifyBy")] Transaction transaction)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Update(transaction);
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

            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(long? id)
        {
            Transaction memberCardCategory = null;
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

        // POST: Transactions/Delete/5
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
