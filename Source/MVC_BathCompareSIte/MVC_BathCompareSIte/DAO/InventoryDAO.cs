using System;
using System.Collections.Generic;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.DAO
{
    public interface InventoryDao
    {
        IList<Inventory> SearchInventories(SearchForm form);
        int Add(InventoryDTO dto);
        int Edit(InventoryDTO dto);
        int Delete(InventoryDTO dto);
        Inventory GetByCode(string Id);

        IList<Inventory> SearchItemsByUser(Int32 userId);
        Int32 CategorySearchList(Int32 cateId);

        IList<Inventory> GetFilterByCategory(Int32 cateId);
    }
}