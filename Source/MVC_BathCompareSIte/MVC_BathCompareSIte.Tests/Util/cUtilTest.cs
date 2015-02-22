using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_BathCompareSIte.Utils;

namespace MVC_BathCompareSIte.Tests.Util
{
    [TestClass]
    public class cUtilTest
    {
        [TestMethod]
        public void TestEncrypt()
        {
            string encrypted = cUtils.Encrypt("200");

            Assert.AreEqual(false, encrypted.Contains("/"));
        }

        [TestMethod]
        public void TestDecrypt()
        {
            string encrypt = cUtils.Encrypt("21");
            string decrypted = cUtils.Decrypt(encrypt);

            Assert.AreEqual(false,encrypt.Contains("/"));
            Assert.AreEqual(decrypted, "21");
        }
    }
}
