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
using efsemop.Framework.Pepemosca.Domain;
using efsemop.Models.ViewModels.SubAlcaldia;
using PagedList;

namespace efsemop.Controllers
{
    public class SubAlcaldiaController : Controller
    {
        protected readonly IRepoSubAlcaldia RepoSubAlcaldia;
        public SubAlcaldiaController(IRepoSubAlcaldia pRepoSubAlcaldia)
        {
            if (pRepoSubAlcaldia == null)
                throw new ArgumentNullException(nameof(pRepoSubAlcaldia));
            RepoSubAlcaldia = pRepoSubAlcaldia;
        }

        // GET: SubAlcaldia
        public ActionResult Index(string texto, string sortOrder, string currentFilter, int? page)
        {
            var pageSize = 5;
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
            var pageIndex = (page ?? 1);
            var result = RepoSubAlcaldia.Busqueda(texto, sortOrder, pageIndex, pageSize);
            return View(result);
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
            var subAlcaldia = new SubAlcaldia()
            {
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                Zona = model.Zona,
                Telefono = model.Telefono,
                NombreSubAlcalde = model.NombreSubAlcalde
            };
            var result = await RepoSubAlcaldia.Crear(subAlcaldia);
            return RedirectToAction("Index");
        }

        // GET: SubAlcaldia/Eliminar/5
        public async Task<ActionResult> Eliminar(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subAlcaldia = await RepoSubAlcaldia.Obtener(id.Value);
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

        // POST: SubAlcaldia/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(SubAlcaldia subAlcaldia)
        {
            try
            {
                var result = await RepoSubAlcaldia.Eliminar(subAlcaldia.IdSubAlcaldia);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Eliminar", new { concurrencyError = true, id = subAlcaldia.IdSubAlcaldia });
            }
            catch (DataException)
            {
                //Log the error
                ModelState.AddModelError(string.Empty, "No se puede eliminar. Inténtelo de nuevo, y si el problema persiste pongase con el administrador del sistema.");
                return View(subAlcaldia);
            }
        }

        // GET: SubAlcaldia/Editar/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subAlcaldia = await RepoSubAlcaldia.Obtener(id.Value);
            if (subAlcaldia == null)
            {
                return HttpNotFound();
            }
            return View(subAlcaldia);
        }

        // POST: SubAlcaldia/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int? id, byte[] rowVersion)
        {
            var fieldsToBind = new[] { "Nombre", "Direccion", "Zona", "Telefono", "NombreSubAlcalde" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subAlcaldiaToUpdate = await RepoSubAlcaldia.Obtener(id.Value);
            if (subAlcaldiaToUpdate == null)
            {
                var deletedSubAlcaldia = new SubAlcaldia();
                TryUpdateModel(deletedSubAlcaldia, fieldsToBind);
                ModelState.AddModelError(string.Empty, "Imposible guardar los cambios. El elemento fué eliminado por otro usuario.");
                return View(deletedSubAlcaldia);
            }
            if (!TryUpdateModel(subAlcaldiaToUpdate, fieldsToBind)) return View(subAlcaldiaToUpdate);
            try
            {
                var res = await RepoSubAlcaldia.Editar(subAlcaldiaToUpdate);
                //subAlcaldiaToUpdate = res;
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (SubAlcaldia)entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty, "Imposible guardar los cambios. El elemento fué eliminado por otro usuario.");
                }
                else
                {
                    var databaseValues = (SubAlcaldia)databaseEntry.ToObject();

                    if (databaseValues.Nombre != clientValues.Nombre)
                        ModelState.AddModelError("Nombre", "Nombre valor: "
                                                           + databaseValues.Nombre);
                    if (databaseValues.Direccion != clientValues.Direccion)
                        ModelState.AddModelError("Direccion", "Direccion valor: "
                                                              + databaseValues.Nombre);
                    if (databaseValues.Zona != clientValues.Zona)
                        ModelState.AddModelError("Zona", "Zona valor: "
                                                         + databaseValues.Zona);
                    if (databaseValues.Telefono != clientValues.Telefono)
                        ModelState.AddModelError("Telefono", "Telefono valor: "
                                                             + databaseValues.Telefono);
                    if (databaseValues.NombreSubAlcalde != clientValues.NombreSubAlcalde)
                        ModelState.AddModelError("NombreSubAlcalde", "NombreSubAlcalde valor: "
                                                                     + databaseValues.NombreSubAlcalde);
                    ModelState.AddModelError(string.Empty, "El registro que está intentado modificar, fue eliminado por otro usuario después de consultar " +
                                                      "los valores originales. La operación de edición fue cancelada.");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError(string.Empty, "No se puede editar. Inténtelo de nuevo, y si el problema persiste pongase con el administrador del sistema.");
            }

            return View(subAlcaldiaToUpdate);
        }

    }
}