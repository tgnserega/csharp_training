using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];
            string dataType = args[0];

            if (args[0] == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });
                }
                if (format == "excel")
                {
                    writeGroupsToExcelFile(groups, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        writeGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        writeGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        writeGroupsToJsonFile(groups, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format " + format);
                    }

                    writer.Close();
                }
            }

            else if (args[0] == "contact")
            {
                List<ContactData> contacts = new List<ContactData>();

                for (int i = 0; i < count; i++)

                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10))
                    {
                        Mobilephone = TestBase.GenerateRandomString(10),
                        Homephone = TestBase.GenerateRandomString(10),
                        Workphone = TestBase.GenerateRandomString(10),
                        Email = TestBase.GenerateRandomString(10),
                        Email2 = TestBase.GenerateRandomString(10),
                        Email3 = TestBase.GenerateRandomString(10)
                    });

                }

                if (format == "excel")
                {
                    writeContactsToExcelFile(contacts, filename);
                }

                else
                {
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        writeContactsToCsvFile(contacts, writer);
                    }

                    else if (format == "xml")
                    {
                        writeContactsToXmlFile(contacts, writer);
                    }

                    else if (format == "json")
                    {
                        writeContactsToJsonFile(contacts, writer);
                    }

                    else
                    {
                        System.Console.Out.Write("Unrecognized format: " + format);
                    }
                    writer.Close();
                }
            }

            else
            {
                System.Console.Out.Write("Unrecognized data type: " + dataType);
            }
        }
        static void writeGroupsToExcelFile (List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }
        
        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void writeContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1},${2},${3},${4},${5},${6},${7}",
                    contact.Firstname, contact.Lastname, contact.Mobilephone, contact.Homephone, contact.Workphone, contact.Email, contact.Email2, contact.Email3));
            }
        }

        static void writeContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;

            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Firstname;
                sheet.Cells[row, 2] = contact.Lastname;
                sheet.Cells[row, 3] = contact.Mobilephone;
                sheet.Cells[row, 4] = contact.Homephone;
                sheet.Cells[row, 5] = contact.Workphone;
                sheet.Cells[row, 6] = contact.Email;
                sheet.Cells[row, 7] = contact.Email2;
                sheet.Cells[row, 8] = contact.Email3;
                row++;

            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
