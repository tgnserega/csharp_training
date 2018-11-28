using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            var a = new Random().Next(1, 13000);
            ContactData newData = (new ContactData("Ivanov" + a, "Ivan" + a));
            newData.Title = null;
            newData.Company = null;
            newData.Address = null;
            newData.Mobilephone = null;
            newData.Email = null;

            app.Contacts.Modify(newData);
        }
    }
}
