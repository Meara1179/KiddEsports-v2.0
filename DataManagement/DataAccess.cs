using DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace DataManagement
{
    public class DataAccess
    {
        #region Primary Contact
        // Grabs all the rows from the Primary Contact table and populate objects with the data.
        public List<PrimaryContact> GetPrimaryContacts()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT * FROM PrimaryContact";
                    return connection.Query<PrimaryContact>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<PrimaryContact>();
            }
        }
        // Grabs the Name and ID rows from the Primary Contact table and populate objects with the data.
        public List<PrimaryContact> GetPrimaryContactsNameAndID()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT PrimaryContactID, PrimaryContactName FROM PrimaryContact";
                    return connection.Query<PrimaryContact>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<PrimaryContact>();
            }
        }
        // Grabs a specific row from the Primary Contact table based on the supplied ID and populate an object with the data.
        public PrimaryContact GetPrimaryContactsByID(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT * FROM PrimaryContact WHERE PrimaryContactID = {id}";
                    return connection.QuerySingle<PrimaryContact>(query);
                }
            }
            catch (Exception e)
            {
                return new PrimaryContact();
            }
        }
        // Creates a new row in the Primary Contact table using the supplied object.
        public void AddPrimaryContact(PrimaryContact primaryContact)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "INSERT INTO PrimaryContact (PrimaryContactName, PrimaryContactPhone, PrimaryContactEmail) " + 
                                    "VALUES (@PrimaryContactName,@PrimaryContactPhone,@PrimaryContactEmail)";
                    connection.Execute(query, primaryContact);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Update the row in the Priamry Contact table with new data.
        public void UpdatePrimaryContact(PrimaryContact primaryContact)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "UPDATE PrimaryContact SET " +
                                    "PrimaryContactName = @PrimaryContactName, PrimaryContactPhone = @PrimaryContactPhone, " +
                                    "PrimaryContactEmail = @PrimaryContactEmail " +
                                    "WHERE PrimaryContactID = @PrimaryContactID";
                    connection.Execute(query, primaryContact);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Delete the row from the Primary Contact table.
        public void DeletePrimaryContact(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"DELETE FROM PrimaryContact WHERE PrimaryContactID = {id}";
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        #region Team Details
        // Grabs all the rows from the Team Details table and populate objects with the data.
        public List<TeamDetails> GetTeamDetails()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT TeamDetails_View.* FROM TeamDetails_View";
                    return connection.Query<TeamDetails>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamDetails>();
            }
        }
        // Grabs the Name and ID rows from the Team Details table and populate objects with the data.
        public List<TeamDetails> GetTeamDetailsNameAndID()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT TeamID, TeamName FROM TeamDetails";
                    return connection.Query<TeamDetails>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamDetails>();
            }
        }
        // Grabs a specific row from the Team Details table based on the supplied ID and populate an object with the data.
        public TeamDetails GetTeamDetailsByID(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT TeamDetails_View.* FROM TeamDetails_View WHERE TeamID = {id}";
                    return connection.QuerySingle<TeamDetails>(query);
                }
            }
            catch (Exception e)
            {
                return new TeamDetails();
            }
        }
        // Creates a new row in the Team Details table using the supplied object.
        public void AddTeamDetails(TeamDetails teamDetails)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "INSERT INTO TeamDetails (TeamName, PrimaryContactId, CompPoints) " +
                                    "VALUES (@TeamName,@PrimaryContactId,@CompPoints)";
                    connection.Execute(query, teamDetails);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Update the row in the Team Details table with new data.
        public void UpdateTeamDetails(TeamDetails teamDetails)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "UPDATE TeamDetails SET " +
                                    "TeamName = @TeamName, PrimaryContactId = @PrimaryContactId, " +
                                    "CompPoints = @CompPoints " +
                                    "WHERE TeamId = @TeamID";
                    connection.Execute(query, teamDetails);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Delete the row from the Team Details table.
        public void DeleteTeamDetails(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"DELETE FROM TeamDetails WHERE TeamId = {id}";
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        #region Events
        // Grabs all the rows from the Events table and populate objects with the data.
        public List<Events> GetEvents()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT * FROM Events";
                    return connection.Query<Events>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Events>();
            }
        }
        // Grabs the Name and ID rows from the Events table and populate objects with the data.
        public List<Events> GetEventsNameAndID()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT EventID, EventName FROM Events";
                    return connection.Query<Events>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Events>();
            }
        }
        // Grabs a specific row from the Events table based on the supplied ID and populate an object with the data.
        public Events GetEventsByID(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT * FROM Events WHERE EventID = {id}";
                    return connection.QuerySingle<Events>(query);
                }
            }
            catch (Exception e)
            {
                return new Events();
            }
        }
        // Creates a new row in the Events table using the supplied object.
        public void AddEvents(Events events)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "INSERT INTO Events (EventName, EventLocation, EventDate) " +
                                    "VALUES (@EventName,@EventLocation,@EventDate)";
                    connection.Execute(query, events);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Update the row in the Events table with new data.
        public void UpdateEvents(Events events)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "UPDATE Events SET " +
                                    "EventName = @EventName, EventLocation = @EventLocation, " +
                                    "EventDate = @EventDate " +
                                    "WHERE EventId = @EventId";
                    connection.Execute(query, events);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Delete the row from the Events table.
        public void DeleteEvents(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"DELETE FROM Events WHERE EventId = {id}";
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        #region Games Played
        // Grabs all the rows from the Games Played table and populate objects with the data.
        public List<GamesPlayed> GetGamesPlayed()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT * FROM GamesPlayed";
                    return connection.Query<GamesPlayed>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<GamesPlayed>();
            }
        }
        // Grabs the Name and ID rows from the Games Played table and populate objects with the data.
        public List<GamesPlayed> GetGamesPlayedNameAndID()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT GamesPlayedID, GameName FROM GamesPlayed";
                    return connection.Query<GamesPlayed>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<GamesPlayed>();
            }
        }
        // Grabs a specific row from the Games Played table based on the supplied ID and populate an object with the data.
        public GamesPlayed GetGamesPlayedByID(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT * FROM GamesPlayed WHERE GamesPlayedID = {id}";
                    return connection.QuerySingle<GamesPlayed>(query);
                }
            }
            catch (Exception e)
            {
                return new GamesPlayed();
            }
        }
        // Creates a new row in the Games Played table using the supplied object.
        public void AddGamesPlayed(GamesPlayed gamesPlayed)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "INSERT INTO GamesPlayed (GameName, GameType) " +
                                    "VALUES (@GameName,@GameType)";
                    connection.Execute(query, gamesPlayed);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Update the row in the Games Played table with new data.
        public void UpdateGamesPlayed(GamesPlayed gamesPlayed)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "UPDATE GamesPlayed SET " +
                                    "GameName = @GameName, GameType = @GameType, " +
                                    "WHERE GamesPlayedID = @GamesPlayedID";
                    connection.Execute(query, gamesPlayed);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Delete the row from the Games Played table.
        public void DeleteGamesPlayed(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"DELETE FROM GamesPlayed WHERE GamesPlayedID = {id}";
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        #region Team Results
        // Grabs all the rows from the Team Results table and populate objects with the data.
        public List<TeamResults> GetTeamResults()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT * FROM TeamResults_View";
                    return connection.Query<TeamResults>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamResults>();
            }
        }
        // Grabs a specific row from the Team Results table based on the supplied ID and populate an object with the data.
        public TeamResults GetTeamResultsByID(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT * FROM TeamResults_View WHERE TeamResultsID = {id}";
                    return connection.QuerySingle<TeamResults>(query);
                }
            }
            catch (Exception e)
            {
                return new TeamResults();
            }
        }
        // Creates a new row in the Team Results table using the supplied object.
        public void AddTeamResults(TeamResults teamResults)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "INSERT INTO TeamResults (GamesPlayedID, TeamID, EventID, OpposingTeamID, Results) " +
                                    "VALUES (@GamesPlayedID,@TeamID,@EventID,@OpposingTeamID,@Results)";
                    connection.Execute(query, teamResults);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Update the row in the Team Results table with new data.
        public void UpdateTeamResults(TeamResults teamResults)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "UPDATE TeamResults SET " +
                                    "TeamID = @TeamID, EventID = @EventID, " +
                                    "OpposingTeamID = @OpposingTeamID, Results = @Results " +
                                    "WHERE TeamResultsID = @TeamResultsID";
                    connection.Execute(query, teamResults);
                }
            }
            catch (Exception e)
            {

            }
        }
        // Delete the row from the Team Results table.
        public void DeleteTeamResults(int id)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"DELETE FROM TeamResults WHERE TeamResultsID = {id}";
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
        #region Reports
        // Grabs all the rows from the Team Detais table and populate objects with the data, ordered by CompPoints.
        public List<TeamDetails> GetTeamDetailsReportCompPoints()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT TeamID, TeamName, PrimaryContactID, CompPoints, PrimaryContactName " +
                        "FROM TeamDetails_View " +
                        "ORDER BY CompPoints DESC";
                    return connection.Query<TeamDetails>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamDetails>();
            }
        }
        // Grabs all the rows from the Team Detais table that match the supplied Team Name and populate objects with the data, ordered by CompPoints.
        public List<TeamDetails> GetTeamDetailsReportByNameCompPoints(string name)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT TeamID, TeamName, PrimaryContactID, CompPoints, PrimaryContactName " +
                        $"FROM TeamDetails_View " +
                        $"WHERE (TeamName = '{name}') " +
                        $"ORDER BY CompPoints DESC";
                    return connection.Query<TeamDetails>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamDetails>();
            }
        }
        // Grabs all the rows from the Team Results table and populate objects with the data, ordered by Event Name.
        public List<TeamResults> GetTeamResultsReportEvent()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT TeamResultsID, GamesPlayedID, TeamID, EventID, Results, OpposingTeamID, TeamName, GameName, EventName, OpposingTeam " +
                        "FROM TeamResults_View " +
                        "ORDER BY EventName";
                    return connection.Query<TeamResults>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamResults>();
            }
        }
        // Grabs all the rows from the Team Results table and populate objects with the data, ordered by Team Name.
        public List<TeamResults> GetTeamResultsReportTeam()
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = "SELECT TeamResultsID, GamesPlayedID, TeamID, EventID, Results, OpposingTeamID, TeamName, GameName, EventName, OpposingTeam " +
                        "FROM TeamResults_View " +
                        "ORDER BY TeamName";
                    return connection.Query<TeamResults>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamResults>();
            }
        }
        // Grabs all the rows from the Team Results table that match the supplied Team Name and populate objects with the data, ordered by Event Name.
        public List<TeamResults> GetTeamResultsReportByNameEvent(string name)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT TeamResultsID, GamesPlayedID, TeamID, EventID, Results, OpposingTeamID, TeamName, GameName, EventName, OpposingTeam " +
                        $"FROM TeamResults_View " +
                        $"WHERE (TeamName = '{name}') OR (OpposingTeam = '{name}') " +
                        $"ORDER BY EventName";
                    return connection.Query<TeamResults>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamResults>();
            }
        }
        // Grabs all the rows from the Team Results table that match the supplied Team Name and populate objects with the data, ordered by Team Name.
        public List<TeamResults> GetTeamResultsReportByNameTeam(string name)
        {
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    string query = $"SELECT TeamResultsID, GamesPlayedID, TeamID, EventID, Results, OpposingTeamID, TeamName, GameName, EventName, OpposingTeam " +
                        $"FROM TeamResults_View " +
                        $"WHERE (TeamName = '{name}') OR (OpposingTeam = '{name}') " +
                        $"ORDER BY TeamName";
                    return connection.Query<TeamResults>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeamResults>();
            }
        }
        #endregion
    }
}
