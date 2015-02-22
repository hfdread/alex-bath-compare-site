using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public interface TypeDao
    {
        ProdType GetByName(string name);
        int Add(TypeDTO dto);
        int Edit(TypeDTO dto);
        int Delete(TypeDTO dto);
    }
}