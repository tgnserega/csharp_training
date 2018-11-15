using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = (new ContactData("Ivanov2", "Ivan2"));
            newData.Title = "Test2";
            newData.Company = "Arl2";
            newData.Address = "Russia2";
            newData.Mobilephone = "8888882";
            newData.Email = "adkl@sdkgfj.com2";

            app.Contacts.Modify(1, newData);
        }
    }
}
