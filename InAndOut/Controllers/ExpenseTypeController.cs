using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseTypeController(ApplicationDbContext db)
        {
            _db = db; // Her får vi adgang til vores databse, så vi kan bruge den i vores controller
        }

        public IActionResult Index()
        {
            IEnumerable<ExpenseType> objList = _db.ExpenseTypes; 
            return View(objList); 
        }
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create den sender data
        [HttpPost] //The HTTP POST method is used to create or add a resource on the server
        [ValidateAntiForgeryToken] //Sikkerheds token
        public IActionResult Create(ExpenseType obj) 
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Add(obj); 
                _db.SaveChanges();
                return RedirectToAction("Index"); 

            }
            return View(obj);
                         
        }

        //Get-Delete den sender data
        public IActionResult Delete(int? id) 
        {
           if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExpenseTypes.Find(id); 

           if(obj == null)
            {
                return NotFound();
            }
            return View(obj); 
        }

        //Post-Delete den sender data
        [HttpPost] //The HTTP POST method is used to create or add a resource on the server
        [ValidateAntiForgeryToken] //Sikkerheds token
        public IActionResult DeletePost(int? id) 
        {
            var obj = _db.ExpenseTypes.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            else
            {
                _db.ExpenseTypes.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //Get-Update den sender data
        public IActionResult Update(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExpenseTypes.Find(id); 

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post-Update den sender data
        [HttpPost] //The HTTP POST method is used to create or add a resource on the server
        [ValidateAntiForgeryToken] //Sikkerheds token
        public IActionResult Update(ExpenseType obj) 
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Update(obj); 
                _db.SaveChanges(); 
                return RedirectToAction("Index"); 

            }
            return View(obj);

        }
    }
}
