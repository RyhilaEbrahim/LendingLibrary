using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMappingEngine _mappingEngine;
        ApplicationDbContext context;

        public RoleController(IRolesRepository rolesRepository, IMappingEngine mappingEngine)
        {
            _rolesRepository = rolesRepository;
            _mappingEngine = mappingEngine;
        }

        public ActionResult Index()
        {
           
            var roleses = _rolesRepository.GetAllRoles().ToList();

            return View();
        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}