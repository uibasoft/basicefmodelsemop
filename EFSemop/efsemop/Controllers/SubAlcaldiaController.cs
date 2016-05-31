using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using efsemop.Framework.Pepemosca.Data;

namespace efsemop.Controllers
{
    public class SubAlcaldiaController : Controller
    {
        // GET: SubAlcaldia
        public ActionResult Index()
        {
            return View();
        }

        // GET: SubAlcaldia/Crear
        public ActionResult Crear()
        {                        
            return View();
        }

        // POST: SubAlcaldia/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear([Bind(Include = "IdSubAlcaldia,Nombre,Direccion,Zona,Telefono, NombreSubAlcalde")] SubAlcaldia subAlcaldia)
        {
            if (!ModelState.IsValid) return View(subAlcaldia);
            using (var db = new AlcaldiaModelContainer())
            {
                db.SubAlcaldias.Add(subAlcaldia);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}