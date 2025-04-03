using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryEstDans
    {
        Task<ActionResult<IEnumerable<EstDans>>>GetIdCategorieByIdTypeProduit(int id);
    }
}
