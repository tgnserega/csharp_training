using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = (new ContactData("Ivanov", "Ivan"));
            contact.Title = "Test";
            contact.Company = "Arl";
            contact.Address = "Russia";
            contact.Mobilephone = "888888";
            contact.Email = "adkl@sdkgfj.com";

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
