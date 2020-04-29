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
    public class ConfigsController : Controller
    {
        private ConfigService service = new ConfigService();

        // GET: Configs
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

        // GET: Configs/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Config memberCard = service.Get(id);
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

        // GET: Configs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfigID,ConfigName,ConfigValue,IsDefault")] Config config)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Add(config);
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

            return View(config);
        }

        // GET: Configs/Edit/5
        public ActionResult Edit(int? id)
        {
            Config memberCard = null;
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

        // POST: Configs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConfigID,ConfigName,ConfigValue,IsDefault")] Config config)
        {
            try
            {
                var res = new ServiceResponse();
                ViewBag.Error = string.Empty;
                if (ModelState.IsValid)
                {
                    res = service.Update(config);
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

            return View(config);
        }

        // GET: Configs/Delete/5
        public ActionResult Delete(int? id)
        {
            Config memberCardCategory = null;
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

        // POST: Configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
