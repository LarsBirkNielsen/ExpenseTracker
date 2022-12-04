using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db; // Vi laver et object af vores DbContext som vi kalder _db


        //Contructor => Hver gang denne controller bliver lavet får den database information fra db.
        public ItemController(ApplicationDbContext db)
        {
            _db = db; // Her får vi adgang til vores databse, så vi kan bruge den i vores controller

        }
        public IActionResult Index()
        {
            IEnumerable<Item> objList = _db.Items; // Vi laver en liste af Item som vi kalder objList som henter alle vores Items i databasen.
            return View(objList); // Listen bliver retuneret i vores view. Kan kan man lege med listen i View delen. 
        }

        //Get-Create action metode- Retunere viewet hvor man kan oprette en ny item
        public IActionResult CreateNewItem()
        {
            return View();  
        }

        //Post-Create den sender data
        [HttpPost] //The HTTP POST method is used to create or add a resource on the server
        [ValidateAntiForgeryToken] //Sikkerheds token
        public IActionResult CreateNewItem(Item obj) //vi tilføjer et object af typen Item som parameter
        {
            _db.Items.Add(obj); //Tilføjer objektet til databasen
            _db.SaveChanges(); //Gemmer ændringer i databasen
            return RedirectToAction("Index");  //Når vi har tilføjet et Item bliver vi Redirected til Index. Fordi det er i samme Controller klasse behøver vi ikke skrive mere end bare "Index"             
        }


    }
}
