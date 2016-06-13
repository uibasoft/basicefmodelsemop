using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using efsemop.Framework.Pepemosca.Domain;
using PagedList;

namespace efsemop.Framework.Pepemosca.Data
{
    public class RepoSubAlcaldia : IRepoSubAlcaldia, IDisposable
    {
        private AlcaldiaModelContainer _db;

        public RepoSubAlcaldia()
        {
            _db = new AlcaldiaModelContainer();
        }
        public IPagedList<SubAlcaldia> Busqueda(string texto, string sortOrder, int pageIndex, int pageSize)
        {
            IPagedList<SubAlcaldia> result = null;
            try
            {
                var list = from ele in _db.SubAlcaldias
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
                result = list.ToPagedList(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                // Logger
            }
            return result;
        }

        public async Task<int> Crear(SubAlcaldia dto)
        {
            var result = -1;
            try
            {
                _db.SubAlcaldias.Add(dto);
                result = await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Logger
            }
            return result;
        }

        public async Task<int> Eliminar(int id)
        {
            var result = -1;
            try
            {
                var entity = await _db.SubAlcaldias.FindAsync(id);
                if (entity != null)
                {
                    _db.Entry(entity).State = EntityState.Deleted;
                    result = await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Logger
            }
            return result;
        }

        public async Task<SubAlcaldia> Obtener(int id)
        {
            SubAlcaldia result = null;
            try
            {
                var subAlcaldia = await _db.SubAlcaldias.FindAsync(id);
                result = subAlcaldia;
            }
            catch (Exception ex)
            {
                // Logger
            }
            return result;
        }

        public async Task<SubAlcaldia> Editar(SubAlcaldia dto)
        {
            SubAlcaldia result = null;
            try
            {
                if (dto == null)
                    return result;
                var entity = await _db.SubAlcaldias.FindAsync(dto.IdSubAlcaldia);
                entity.Nombre = dto.Nombre;
                entity.Direccion = dto.Direccion;
                entity.Zona = dto.Zona;
                entity.Telefono = dto.Telefono;
                entity.NombreSubAlcalde = dto.NombreSubAlcalde;
                var rest = await _db.SaveChangesAsync();
                result = entity;
            }
            catch (Exception ex)
            {
                // Logger
            }
            return result;
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_db == null) return;
            _db.Dispose();
            _db = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}