using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using efsemop.Framework.Pepemosca.Data;
using efsemop.Models.ViewModels.SubAlcaldia;
using PagedList;

namespace efsemop.Controllers
{
    public class SubAlcaldiaController : Controller
    {
        // GET: SubAlcaldia
        public ActionResult Index(string texto, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = string.IsNullOrEmpty(sortOrder) ? "nombre_desc" : string.Empty;
            ViewBag.ZonaSortParm = sortOrder == "Zona" ? "zona_desc" : "Zona";

            if (!string.IsNullOrWhiteSpace(texto))
            {
                page = 1;
            }
            else
            {
                texto = currentFilter;
            }

            ViewBag.CurrentFilter = texto;

            using (var db = new AlcaldiaModelContainer())
            {
                var list = from ele in db.SubAlcaldias
                           select ele;

                if (!string.IsNullOrEmpty(texto))
                {
                    list = list.Where(s => s.Nombre.Contains(texto) || s.Direccion.Contains(texto));
                }
                switch (sortOrder)
                {
                    case "nombre_desc":
                        list = list.OrderByDescending(s => s.Nombre);
                        break;
                    case "Zona":
                        list = list.OrderBy(s => s.Zona);
                        break;
                    case "zona_desc":
                        list = list.OrderByDescending(s => s.Zona);
                        break;
                    default:
                        list = list.OrderBy(s => s.Nombre);
                        break;
                }
                var pageSize = 3;
                var pageNumber = (page ?? 1);
                return View(list.ToPagedList(pageNumber, pageSize));

            }         
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