using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELearning.Attributes;
using ELearning.ViewModels;
using ELearning.Services;

namespace ELearning.Controllers
{
    [ShowName(ControllerShowName = "系统管理")]
    public class SysAdminController : ControllerBase
    {
        public IAccountService AccountService { get; set; }

        // GET: SysAdmin
        public ActionResult Index()
        {
            return View();
        }

        [ShowName(ControllerShowName = "权限管理", ActionShowName = "用户管理")]
        [HttpGet]
        public ActionResult Users()
        {
            return View();
        }

        /// <summary>
        /// 接受Ajax请求返回JSON格式用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfos(QueryParam queryParam)
        {
            var userInfos = AccountService.GetUsersByParam(queryParam);
            throw new Exception("bad request");
            var jsonResult = new { total = userInfos.Total, rows = userInfos.ViewModels };
            return Json(jsonResult);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserEdit(string userName)
        {
            return PartialView();
        }


        [ShowName(ControllerShowName = "权限管理", ActionShowName = "角色管理")]
        public ActionResult Roles()
        {
            return View();
        }

        // GET: SysAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SysAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SysAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SysAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SysAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SysAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
