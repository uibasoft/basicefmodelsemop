using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
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

        // GET: SubAlcaldia/Eliminar/5
        public async Task<ActionResult> Eliminar(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new AlcaldiaModelContainer())
            {
                var subAlcaldia = await db.SubAlcaldias.FindAsync(id);
                if (subAlcaldia == null)
                {
                    if (concurrencyError.GetValueOrDefault())
                    {
                        return RedirectToAction("Index");
                    }
                    return HttpNotFound();
                }

                if (concurrencyError.GetValueOrDefault())
                {
                    ViewBag.ConcurrencyErrorMessage = "El registro que intentó eliminar fue modificado por otro usuario después de consultar " +
                                                      "los valores originales. " +
                                                      "La operación de eliminación fue cancelada y los valores actuales se han vuelto a " +
                                                      "cargar. Si aún desea eliminar este registro, haga clic en el botón " +
                                                      "Eliminar de nuevo. De lo contrario Haga clic en el hipervínculo Cancelar.";
                }
                return View(subAlcaldia);
            }          
        }

        // POST: SubAlcaldia/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(SubAlcaldia subAlcaldia)
        {
            try
            {
                using (var db = new AlcaldiaModelContainer())
                {
                    db.Entry(subAlcaldia).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
             
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Eliminar", new { concurrencyError = true, id = subAlcaldia.IdSubAlcaldia });
            }
            catch (DataException )
            {
                //Log the error
                ModelState.AddModelError(string.Empty, "No se puede eliminar. Inténtelo de nuevo, y si el problema " +
                                                       "persiste pongase con el administrador del sistema.");
                return View(subAlcaldia);
            }
        }
    }
}