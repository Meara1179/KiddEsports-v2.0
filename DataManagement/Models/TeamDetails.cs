using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Models
{
    public class TeamDetails
    {

        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int PrimaryContactId { get; set; }
        public string PrimaryContactName { get; set; }
        public int CompPoints { get; set; }


        public TeamDetails()
        {

        }

        public TeamDetails(int teamID, string teamName)
        {
            TeamID = teamID;
            TeamName = teamName;
        }

        public TeamDetails(int teamID, string teamName, int compPoints, int primaryContactId, string primaryContactName)
        {
            TeamID = teamID;
            TeamName = teamName;
            CompPoints = compPoints;
            PrimaryContactId = primaryContactId;
            PrimaryContactName = primaryContactName;    
        }

    }
}
