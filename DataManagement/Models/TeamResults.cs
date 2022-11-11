using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Models
{
    public class TeamResults
    {
        public int TeamResultsID { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int GamesPlayedID { get; set; }
        public string GameName { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int OpposingTeamID { get; set; }
        public string OpposingTeam { get; set; }
        public string Results { get; set; }

        public TeamResults()
        {

        }

        public TeamResults(int eventID, int gamesPlayedID, int teamID, int opposingTeamID, string results)
        {
            EventID = eventID;
            GamesPlayedID = gamesPlayedID;
            TeamID = teamID;
            OpposingTeamID = opposingTeamID;
            Results = results;
        }

        public TeamResults(int teamResultsID, int eventID, int gamesPlayedID, int teamID, int opposingTeamID, string results)
        {
            TeamResultsID = teamResultsID;
            EventID = eventID;
            GamesPlayedID = gamesPlayedID;
            TeamID = teamID;
            OpposingTeamID = opposingTeamID;
            Results = results;
        }
    }
}
