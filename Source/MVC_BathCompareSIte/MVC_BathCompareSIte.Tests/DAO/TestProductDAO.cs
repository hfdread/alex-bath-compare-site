using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using MVC_BathCompareSIte.DAO;
using MVC_BathCompareSIte.Models;
using MVC_BathCompareSIte.Forms;

namespace MVC_BathCompareSIte.Tests.DAO
{
    [TestClass]
    public class TestProductDao
    {

         [DataSource("MySql.Data.MySqlClient",
            "server=localhost;database=alex_items_db;User Id=root;password=adm1n",
            "products", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TC0001_Search_Success()
        {
            
        }
    }
}
