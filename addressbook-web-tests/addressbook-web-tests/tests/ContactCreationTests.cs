using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Ivanov", "Ivan")
            {
                Title = "Test",
                Company = "Arl",
                Address = "Russia",
                Mobilephone = "888888",
                Email = "adkl@sdkgfj.com"
            };

            app.Contacts.Create(contact);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = (new ContactData("", ""));
            contact.Title = "";
            contact.Company = "";
            contact.Address = "";
            contact.Mobilephone = "";
            contact.Email = "";

            app.Contacts.Create(contact);
        }
    }
}
