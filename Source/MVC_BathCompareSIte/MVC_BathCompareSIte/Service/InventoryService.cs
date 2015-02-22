using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MVC_BathCompareSIte.DTO;
using MVC_BathCompareSIte.Forms;
using MVC_BathCompareSIte.Models;

namespace MVC_BathCompareSIte.Service
{
    public interface InventoryService
    {
        InventoryListDTO Search(SearchForm form);
        InventoryDTO ImportFile(Stream input);

        InventoryDTO Add(InventoryDTO dto);
        InventoryDTO Edit(InventoryDTO dto);
        InventoryDTO Delete(InventoryDTO dto);
        InventoryDTO GetByCode(string code);
        string RowValidation(string[] rowVal, int nRow);
        InventoryListDTO SearchItemsByUser(Int32 userId);

        IList<ColorTreeDto> GetColorsByCategory(Int32 Id);
       IList<SizeTreeDto> GetSizesByCategory(Int32 Id);
    }
}