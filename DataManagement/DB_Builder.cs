using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using DataManagement.Models;

namespace DataManagement
{
    public class DB_Builder : DataAccess
    {
        #region Database Schema
        // Creates a barebones database used for the application.
        public void CreateDatabase()
        {
            SqlConnection connection = Helper.CreateConnection("KiddEsports");
            try
            {
                string connString = $"Data Source={connection.DataSource}; Integrated Security = True";
                string query = $"IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = '{connection.Database}') " +
                    $"CREATE DATABASE {connection.Database}";

                using (connection = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (connection.State == System.Data.ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        // Checks if the database has any user made tables.
        public bool DoTablesExist()
        {
            var connection = Helper.CreateConnection("KiddEsports");

            string query = $"SELECT COUNT(*) FROM {connection.Database}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            try
            {
                using (connection)
                {
                    int count = connection.QuerySingle<int>(query);

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Framework method used in the creation of new tables.
        public void CreateTable(string name, string structure)
        {
            string query = $"CREATE TABLE {name} ({structure})";
            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    connection.Execute(query);
                }
            }
            catch (Exception)
            {

            }
        }

        // Builds all the required table using other methods.
        public void BuildDatabaseTables()
        {
            BuildPrimaryContactTable();
            CreateTeamDetailsTable();
            CreateEventsTable();
            CreateGamesPlayedTable();
            CreateTeamResultsTable();
        }
        // Builds all the required views using other methods.
        public void BuildDatabaseViews()
        {
            CreateTeamDetailsView();
            CreateTeamResultsView();
        }
        #endregion
        #region Table Builders
        // Builds the primary contact table.
        private void BuildPrimaryContactTable()
        {
            string name = "PrimaryContact";

            string structure = "PrimaryContactID int IDENTITY(1,1) PRIMARY KEY, " +
                                "PrimaryContactName VARCHAR(50) NOT NULL, " +
                                "PrimaryContactPhone VARCHAR(12) NOT NULL, " +
                                "PrimaryContactEmail VARCHAR(50) NOT NULL";
            CreateTable(name, structure);
        }

        // Builds the Team Details table.
        private void CreateTeamDetailsTable()
        {
            string name = "TeamDetails";

            string structure = "TeamID int IDENTITY(1,1) PRIMARY KEY, " +
                                "TeamName VARCHAR(50) NOT NULL, " +
                                "PrimaryContactID int NOT NULL FOREIGN KEY REFERENCES PrimaryContact(PrimaryContactID), " +
                                "CompPoints int NOT NULL";
            CreateTable(name, structure);
        }

        private void CreateEventsTable()
        {
            string name = "Events";

            string structure = "EventID int IDENTITY(1,1) PRIMARY KEY, " +
                                "EventName VARCHAR(50) NOT NULL, " +
                                "EventLocation VARCHAR(100) NOT NULL, " +
                                "EventDate VARCHAR(20) NOT NULL";
            CreateTable(name, structure);
        }

        // Builds the Games Played table.
        private void CreateGamesPlayedTable()
        {
            string name = "GamesPlayed";

            string structure = "GamesPlayedID int IDENTITY(1,1) PRIMARY KEY, " +
                                "GameName VARCHAR(50) NOT NULL, " +
                                "GameType VARCHAR(50) NOT NULL";
            CreateTable(name, structure);
        }

        // Builds the Team Results table.
        private void CreateTeamResultsTable()
        {
            string name = "TeamResults";

            string structure = "TeamResultsID int IDENTITY(1,1) PRIMARY KEY, " +
                                "GamesPlayedID int NOT NULL FOREIGN KEY REFERENCES GamesPlayed(GamesPlayedID), " +
                                "TeamID int NOT NULL FOREIGN KEY REFERENCES TeamDetails(TeamID), " +
                                "EventID int NOT NULL FOREIGN KEY REFERENCES Events(EventID), " +
                                "OpposingTeamID int NOT NULL FOREIGN KEY REFERENCES TeamDetails(TeamID), " +
                                "Results VARCHAR(20) NOT NULL";
            CreateTable(name, structure);
        }

        // Builds the Team Details view.
        private void CreateTeamDetailsView()
        {
            string query = "CREATE VIEW TeamDetails_View AS " +
                "SELECT dbo.TeamDetails.TeamID, dbo.TeamDetails.TeamName, dbo.TeamDetails.PrimaryContactID, " +
                "dbo.TeamDetails.CompPoints, dbo.PrimaryContact.PrimaryContactName " +
                "FROM dbo.PrimaryContact " +
                "INNER JOIN dbo.TeamDetails ON dbo.PrimaryContact.PrimaryContactID = dbo.TeamDetails.PrimaryContactID";

            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    connection.Execute(query);
                }
            }
            catch (Exception)
            {

            }
        }
        // Builds the Team Result view.
        private void CreateTeamResultsView()
        {
            string query = "CREATE VIEW TeamResults_View AS " +
                            "SELECT dbo.TeamResults.TeamResultsID, dbo.TeamResults.GamesPlayedID, dbo.TeamResults.TeamID, dbo.TeamResults.EventID, " +
                            "dbo.TeamResults.OpposingTeamID, dbo.TeamResults.Results, dbo.TeamDetails.TeamName, dbo.GamesPlayed.GameName, " +
                            "dbo.Events.EventName, OppTeam.TeamName AS OpposingTeam " +
                            "FROM dbo.TeamResults " +
                            "INNER JOIN dbo.TeamDetails ON dbo.TeamResults.TeamID = dbo.TeamDetails.TeamID " +
                            "INNER JOIN dbo.GamesPlayed ON dbo.TeamResults.GamesPlayedID = dbo.GamesPlayed.GamesPlayedID " +
                            "INNER JOIN dbo.Events ON dbo.TeamResults.EventID = dbo.Events.EventID " +
                            "INNER JOIN dbo.TeamDetails AS OppTeam ON dbo.TeamResults.OpposingTeamID = OppTeam.TeamID";

            try
            {
                using (var connection = Helper.CreateConnection("KiddEsports"))
                {
                    connection.Execute(query);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region Populate Tables
        // Populates the tables with data.
        public void PopulateTables()
        {
            PopulatePrimaryContactTable();
            PopulateTeamDetailsTable();
            PopulateGamesPlayedTable();
            PopulateEventsTable();
            PopulateTeamResultsTable();
        }

        // Adds the data to the PrimaryContact table.
        private void PopulatePrimaryContactTable()
        {
            List<PrimaryContact> primaryContacts = new List<PrimaryContact>();

            primaryContacts.Add(new PrimaryContact("Steve", "0423985627", "Steve@email.com"));
            primaryContacts.Add(new PrimaryContact("James", "0423985627", "James@email.com"));
            primaryContacts.Add(new PrimaryContact("Steph", "0423985627", "Steph@email.com"));
            primaryContacts.Add(new PrimaryContact("Lucy", "0423985627", "Lucy@email.com"));

            foreach (var item in primaryContacts)
            {
                AddPrimaryContact(item);
            }
        }
        // Adds the data to the TeamDetails table.
        private void PopulateTeamDetailsTable()
        {
            List<TeamDetails> teamDetails = new List<TeamDetails>();

            teamDetails.Add(new TeamDetails("The Elite", 4, 1));
            teamDetails.Add(new TeamDetails("Cat Attack", 2, 2));
            teamDetails.Add(new TeamDetails("Keter", 0, 3));
            teamDetails.Add(new TeamDetails("Jingle Jangle", 2, 4));

            foreach (var item in teamDetails)
            {
                AddTeamDetails(item);
            }
        }
        // Adds the data to the GamesPlayed table.
        private void PopulateGamesPlayedTable()
        {
            List<GamesPlayed> gamesPlayed = new List<GamesPlayed>();

            gamesPlayed.Add(new GamesPlayed("Halo", "Team"));
            gamesPlayed.Add(new GamesPlayed("Street Fighter", "Solo"));

            foreach (var item in gamesPlayed)
            {
                AddGamesPlayed(item);
            }
        }
        // Adds the data to the Events table.
        private void PopulateEventsTable()
        {
            List<Events> events = new List<Events>();

            events.Add(new Events("Worlds", "New York", "2025/10/31"));
            events.Add(new Events("World Cup", "Earth", "2055/12/25"));

            foreach (var item in events)
            {
                AddEvents(item);
            }
        }
        // Adds the data to the TeamResults table.
        private void PopulateTeamResultsTable()
        {
            List<TeamResults> teamResults = new List<TeamResults>();

            teamResults.Add(new TeamResults(1, 1, 1, 2, "Win"));
            teamResults.Add(new TeamResults(1, 1, 2, 1, "Win"));
            teamResults.Add(new TeamResults(2, 2, 3, 4, "Lose"));
            teamResults.Add(new TeamResults(2, 1, 4, 1, "Lose"));

            foreach (var item in teamResults)
            {
                AddTeamResults(item);
            }
        }
        #endregion
    }
}
