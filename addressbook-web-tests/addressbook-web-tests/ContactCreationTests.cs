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
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToAddContactsPage();
            ContactData contact = (new ContactData("Ivanov", "Ivan"));
            contact.Title = "Test";
            contact.Company = "Arl";
            contact.Address = "Russia";
            contact.Mobilephone = "888888";
            contact.Email = "adkl@sdkgfj.com";
            FillContactForm(contact);
            SubmitContactCreation();
            Logout();
        }
    }
}
