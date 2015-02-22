using System;
using System.Collections.Generic;
using System.Linq;
using MVC_BathCompareSIte.DAO;
using MVC_BathCompareSIte.DTO;

namespace MVC_BathCompareSIte.Service
{
    public class BrandServiceImpl : BrandService
    {
        private BrandDao _dao = new BrandDaoImpl();

        public BrandDao Dao
        {
            get { return this._dao; }
            set { this._dao = value; }
        }
        public BrandDTO Add(BrandDTO dto)
        {
            var result = new BrandDTO {ErrorList = new List<string>()};

            try
            {
                int nResult = _dao.Add(dto);

                if (nResult <= 0)
                {
                    result.ErrorList = new List<string>();
                    result.ErrorList.Add("Add Failed!");
                }
                else
                {
                    var temp = _dao.GetById(dto.Id);
                    result.Id = temp.Id;
                    result.Name = temp.Name;
                    result.Description = temp.Description;
                }
            }
            catch (Exception e)
            {
                result = new BrandDTO {ErrorList = new List<string> {e.Message}};
            }

            return result;
        }

        public BrandDTO Edit(BrandDTO dto)
        {
            var result = new BrandDTO {ErrorList = new List<string>()};
            try
            {
                int nResult = _dao.Edit(dto);
                if (nResult <= 0)
                {
                    result.ErrorList = new List<string>{"Edit Failed!"};
                }
                else
                {
                    var temp = _dao.GetById(dto.Id);
                    result.Id = temp.Id;
                    result.Name = temp.Name;
                    result.Description = temp.Description;
                }
            }
            catch (Exception e)
            {
                result = new BrandDTO {ErrorList = new List<string> {e.Message}};
            }

            return result;
        }

        public BrandDTO Delete(BrandDTO dto)
        {
            var result = new BrandDTO {ErrorList = new List<string>()};
            try
            {
                int nResult = _dao.Delete(dto);
                if (nResult <= 0)
                {
                    result.ErrorList = new List<string> {"Delete Failed!"};
                }
            }
            catch (Exception e)
            {
                result = new BrandDTO {ErrorList = new List<string> {e.Message}};
            }

            return result;
        }

        public BrandListDTO GetAll()
        {
            BrandListDTO list = new BrandListDTO();
            list.DtoList = new List<BrandDTO>();
            list.ErrorList = new List<string>();

            try
            {
                var tempList = _dao.GetAll();
                list.DtoList = tempList.Select(q => new BrandDTO
                {
                    Id = q.Id,
                    Name = q.Name,
                    Description = q.Description
                }).ToList();
            }
            catch (Exception e)
            {
                list = new BrandListDTO();
                list.ErrorList = new List<string>();
                list.ErrorList.Add(e.Message);
            }

            return list;
        }
    }
}