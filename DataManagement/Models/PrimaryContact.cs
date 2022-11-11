using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Models
{
    public class PrimaryContact
    {
        public int PrimaryContactID { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public string PrimaryContactEmail { get; set; }

        public PrimaryContact()
        {

        }

        public PrimaryContact(int primaryContactID, string primaryContactName)
        {
            PrimaryContactID = primaryContactID;
            PrimaryContactName = primaryContactName;
        }

        public PrimaryContact(string primaryContactName, string primaryContactPhone, string primaryContactEmail)
        {
            PrimaryContactName = primaryContactName;
            PrimaryContactPhone = primaryContactPhone;
            PrimaryContactEmail = primaryContactEmail;
        }

        public PrimaryContact(int primaryContactID, string primaryContactName, string primaryContactPhone, string primaryContactEmail)
        {
            PrimaryContactID = primaryContactID;
            PrimaryContactName = primaryContactName;
            PrimaryContactPhone = primaryContactPhone;
            PrimaryContactEmail = primaryContactEmail;
        }
    }
}
