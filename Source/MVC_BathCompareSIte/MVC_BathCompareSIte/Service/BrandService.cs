using System;
using MVC_BathCompareSIte.DTO;

namespace MVC_BathCompareSIte.Service
{
    public interface BrandService
    {
        BrandDTO Add(BrandDTO dto);
        BrandDTO Edit(BrandDTO dto);
        BrandDTO Delete(BrandDTO dto);
        BrandListDTO GetAll();
    }
}