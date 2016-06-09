using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using efsemop.Framework.Pepemosca.Data;
using PagedList;

namespace efsemop.Framework.Pepemosca.Domain
{
    public interface IRepoSubAlcaldia : IDisposable
    {
        IPagedList<SubAlcaldia> Busqueda(string texto, string sortOrder, int pageIndex, int pageSize);
        Task<int> Crear(SubAlcaldia dto);
        Task<int> Eliminar(int id);
        Task<SubAlcaldia> Obtener(int id);
    }
}
