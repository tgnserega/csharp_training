﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInformationFromEditForm;

        public ContactData(string lastname, string firstname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData()
        {
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Lastname == other.Lastname && Firstname == other.Firstname;
        }

        public override int GetHashCode()
        {
            string Fio = Lastname + Firstname;
            return Fio.GetHashCode();
        }

        public override string ToString()
        {
            return "Lastname=" + Lastname + "\nFirstname=" + Firstname + "\nAllPhones=" + AllPhones + "\nAllEmails=" + AllEmails;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(Firstname, other.Firstname))
            {
                return Lastname.CompareTo(other.Lastname);
            }

            return Firstname.CompareTo(other.Firstname);
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "mobile")]
        public string Mobilephone { get; set; }

        [Column(Name = "home")]
        public string Homephone { get; set; }

        [Column(Name = "work")]
        public string Workphone { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones !=null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(Homephone) + CleanUp(Mobilephone) + CleanUp(Workphone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
          //         return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
            return Regex.Replace(phone, "[ -/g()\r\n]", "");
        }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3));
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllInformationFromEditForm
        {
            get
            {
                if (allInformationFromEditForm != null)
                {
                    return allInformationFromEditForm;
                }
                else
                {
                    string a = "";

                    if (Firstname != "")
                    {
                        a += Firstname;
                    }
                    if (Lastname != "")
                    {
                        a += Lastname;
                    }
                    if (Homephone != "")
                    {
                        a += "H:" + Homephone;
                    }
                    if (Mobilephone != "")
                    {
                        a += "M:" + Mobilephone;
                    }
                    if (Workphone != "")
                    {
                        a += "H:" + Workphone;
                    }
                    if (Email != "")
                    {
                        a += Email;
                    }
                    if (Email2 != "")
                    {
                        a += Email2;
                    }
                    if (Email3 != "")
                    {
                        a += Email3;
                    }
                    return a;
                }
            }
            set
            {
                allInformationFromEditForm = value;
            }
        }

        public string ContactPropertiesPageText { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                //return (from c in db.Contacts where c.Deprecated == "0000-00-00 00:00:00" select c).ToList();
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();

            }
        }
    }
}