using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db; // Her får vi adgang til vores database, så vi kan bruge den i vores controller
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expenses; // Vi laver en liste af Item som vi kalder objList som henter alle vores Expenses i databasen.

            foreach (var obj in objList)
            {
                obj.ExpenseType = _db.ExpenseTypes.FirstOrDefault(u => u.Id == obj.ExpenseTypeId); //Den gør at vi får alle objekterne i vores database
            }

            return View(objList); // Listen bliver retuneret i vores view. Kan kan man lege med objekternes propperties (navn, id, type osv alt efter hvad vi gerne vil vise omkring objekterne) i View delen. 
        }

        // GET-Create
        public IActionResult Create()
        {
            ExpenseViewModel expenseViewModel = new ExpenseViewModel()
            {
                Expense = new Expense(),
                ExpenseTypeDropDown = _db.ExpenseTypes.Select(item => new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.Id.ToString()
                })
            };
            return View(expenseViewModel);
        }



        //Post-Create den sender data
        [HttpPost] //The HTTP POST method is used to create or add a resource on the server
        [ValidateAntiForgeryToken] //Sikkerheds token
        public IActionResult Create(ExpenseViewModel obj) //vi tilføjer et object af typen Item som parameter
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(obj.Expense); //Tilføjer objektet til databasen
                _db.SaveChanges(); //Gemmer ændringer i databasen
                return RedirectToAction("Index");  //Når vi har tilføjet et Item bliver vi Redirected til Index. Fordi det er i samme Controller klasse behøver vi ikke skrive mere end bare "Index"

            }
            return View(obj);
                         
        }

        //Get-Delete den sender data
        public IActionResult Delete(int? id) //Vi får id fra obj som vi trykker delete på
        {
           if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id); // Vi får hele objektet ved find(id)

           if(obj == null)
            {
                return NotFound();
            }
            return View(obj); //Vi retunere deleteView sammen med det enkelte objekt
        }

        //Post-Delete den sender data
        [HttpPost] //The HTTP POST method is used to create or add a resource on the server
        [ValidateAntiForgeryToken] //Sikkerheds token
        public IActionResult DeletePost(int? id) //vi tilføjer et object af typen Item som parameter
        {
            var obj = _db.Expenses.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            else
            {
                _db.Expenses.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //Get-Update den sender data
        public IActionResult Update(int? id) //Vi får id fra obj som vi trykker update på
        {
            ExpenseViewModel expenseViewModel = new ExpenseViewModel() //Laver en instans af vore viewmodel klasse
            {
                Expense = new Expense(), //i viewModel klassen laver vi en instans af Expense klassen
                ExpenseTypeDropDown = _db.ExpenseTypes.Select(type => new SelectListItem //udover en instans af expenseklassen får vi vores type dropdown fra databasen
                {
                    Text = type.TypeName,
                    Value = type.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            expenseViewModel.Expense = _db.Expenses.Find(id);

            if (expenseViewModel.Expense == null)
            {
                return NotFound();
            }
            return View(expenseViewModel); //Vi retunere updateView sammen med det enkelte objekt
        }

        //Post-Update den sender data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
    }
}
