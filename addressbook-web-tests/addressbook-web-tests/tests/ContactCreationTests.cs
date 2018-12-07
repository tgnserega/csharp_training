using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Ivan", "Ivanov")
            {
                Mobilephone = "8-(999)-12312-12",
                Homephone = "8-(456)-567-345",
                Workphone = "8-(862)-4657-6",
                Email = "adkl@sdkgfj.com",
                Email2 = "12@08=0=0--.ru",
                Email3 = "o0o0@bk.com"
            };

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);
            app.Navigator.GoToContactsPage();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EmptyContactCreationTest()

        {
            ContactData contact = new ContactData("", "")
            {
                Title = "",
                Company = "",
                Address = "",
                Mobilephone = "",
                Homephone = "",
                Workphone = "",
                Email = "",
                Email2 = "",
                Email3 = ""
            };

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);
            app.Navigator.GoToContactsPage();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
