using System;
using CompanyManagement.Utilities;
using NUnit.Framework;

namespace CompanyManagementTest.UtilitiesTest
{
    [TestFixture]
    public class UtilsTest
    {
        [Test]
        public void Utils_Convert_DateTime_To_String_Test()
        {
            var dt = new DateTime(2000, 1, 1, 7, 0, 0);
            var str = Utils.ToSQLFormat(dt);
            Assert.AreEqual("2000-01-01 07:00:00", str);
        }
    }
}