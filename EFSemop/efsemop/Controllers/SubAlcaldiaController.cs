using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using efsemop.Framework.Pepemosca.Data;
using efsemop.Models.ViewModels.SubAlcaldia;

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
        public async Task<ActionResult> Crear([Bind(Include = "Nombre,Direccion,Zona,Telefono, NombreSubAlcalde")] CreateSubAlcaldiaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (var db = new AlcaldiaModelContainer())
            {
                var subAlcaldia = new SubAlcaldia()
                {
                    Nombre = model.Nombre,
                    Direccion = model.Direccion,
                    Zona = model.Zona,
                    Telefono = model.Telefono,
                    NombreSubAlcalde = model.NombreSubAlcalde
                };
                db.SubAlcaldias.Add(subAlcaldia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }               
                        
        }
    }
}