﻿using System;
using System.Collections.Generic;
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

            app.Contacts.GreateContactIfNotExist();

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Modify(newData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }
    }
}
