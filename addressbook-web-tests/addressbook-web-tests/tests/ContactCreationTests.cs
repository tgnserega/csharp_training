using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Mobilephone = GenerateRandomString(100),
                    Homephone = GenerateRandomString(100),
                    Workphone = GenerateRandomString(100),
                    Email = GenerateRandomString(100),
                    Email2 = GenerateRandomString(100),
                    Email3 = GenerateRandomString(100)
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            //   string[] lines = File.ReadAllLines(@"contact.csv");
            string[] lines = File.ReadAllLines(@"C:\Users\sergey.pashkov\Source\Repos\tgnserega\csharp_training\addressbook-web-tests\addressbook-web-tests\bin\Debug\contact.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData(parts[0], parts[1])
                {
                    Mobilephone = parts[2],
                    Homephone = parts[3],
                    Workphone = parts[4],
                    Email = parts[5],
                    Email2 = parts[6],
                    Email3 = parts[7]
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            //string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"contact.xml");
            //return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(fullPath));
            return (List<ContactData>) new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"C:\Users\sergey.pashkov\Source\Repos\tgnserega\csharp_training\addressbook-web-tests\addressbook-web-tests\bin\Debug\contact.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            //return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"C:\Users\sergey.pashkov\Source\Repos\tgnserega\csharp_training\addressbook-web-tests\addressbook-web-tests\bin\Debug\contact.json"));
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contact.json"));
        }

        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contact.xlsx"));
            //Excel.Workbook wb = app.Workbooks.Open(@"C:\Users\sergey.pashkov\Source\Repos\tgnserega\csharp_training\addressbook-web-tests\addressbook-web-tests\bin\Debug\contact.xlsx");
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = range.Cells[i, 1].Value,
                    Lastname = range.Cells[i, 2].Value,
                    Mobilephone = range.Cells[i, 3].Value,
                    Homephone = range.Cells[i, 4].Value,
                    Workphone = range.Cells[i, 5].Value,
                    Email = range.Cells[i, 6].Value,
                    Email2 = range.Cells[i, 7].Value,
                    Email3 = range.Cells[i, 8].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return contacts;

        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {
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
