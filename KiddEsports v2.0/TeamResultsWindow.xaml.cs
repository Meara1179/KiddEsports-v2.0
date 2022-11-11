using DataManagement;
using DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KiddEsports_v2._0
{
    /// <summary>
    /// Interaction logic for TeamResultsWindow.xaml
    /// </summary>
    public partial class TeamResultsWindow : Window
    {
        // Creates the DataAccess object needed to access the class methods.
        DataAccess data = new DataAccess();
        // Creates a list used to store the TeamResults objects.
        List<TeamResults> TeamResultsList = new List<TeamResults>();
        // Creates a list used to store the TeamDetails objects.
        List<TeamDetails> TeamNameList = new List<TeamDetails>();
        // Creates a list used to store the Events objects.
        List<Events> EventList = new List<Events>();
        // Creates a list used to store the GamesPlayed objects.
        List<GamesPlayed> GameList = new List<GamesPlayed>();
        // A singular TeamResults object used to store the currently selected Events object.
        TeamResults currentTeamResults = new TeamResults();
        // A singular TeamDetails object used to store the team in the first team slot.
        TeamDetails team1 = new TeamDetails();
        // A singular TeamDetails object used to store the team in the opposing team slot.
        TeamDetails team2 = new TeamDetails();
        // Stores the old result to be used for comp points calculations
        string oldResults;
        // Stores if the current entry being entered will be a new entry or not.
        bool isNewEntry = true;

        int eventID;
        int gamePlayedID;
        int teamID;
        int opposingTeamID;
        string results;

        // Main TeamResultsWndow constructor.
        public TeamResultsWindow()
        {
            InitializeComponent();
            UpdateComboBoxes();
            UpdateDataGrid();
        }

        // Method used to populate and refresh the data grid.
        public void UpdateDataGrid()
        {
            TeamResultsList = data.GetTeamResults();
            dgvTeamResults.ItemsSource = TeamResultsList;
            dgvTeamResults.Items.Refresh();
        }

        // Method used to populate and update the combo boxes with data.
        public void UpdateComboBoxes()
        {
            TeamNameList = data.GetTeamDetailsNameAndID();
            EventList = data.GetEventsNameAndID();
            GameList = data.GetGamesPlayedNameAndID();

            foreach (TeamDetails team in TeamNameList)
            {
                cboTeamName.Items.Add($"ID: {team.TeamID}" +
                                       $" Name: {team.TeamName}");
            }

            foreach (TeamDetails team in TeamNameList)
            {
                cboOpposingTeam.Items.Add($"ID: {team.TeamID}" +
                                       $" Name: {team.TeamName}");
            }

            foreach (Events events in EventList)
            {
                cboEventName.Items.Add($"ID: {events.EventId}" +
                                       $" Name: {events.EventName}");
            }

            foreach (GamesPlayed gameList in GameList)
            {
                cboGameName.Items.Add($"ID: {gameList.GamesPlayedID}" +
                                       $" Name: {gameList.GameName}");
            }
        }

        // Clears the fields and readies the creation of a new record.
        private void ClearTextBoxes()
        {
            txtTeamResultsID.Text = "";
            eventID = -1;
            gamePlayedID = -1;
            teamID = -1;
            opposingTeamID = -1;
            results = "";

            isNewEntry = true;
        }

        // Event that exectutes when a row is selected in the data grid.
        private void dgvTeamResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvTeamResults.SelectedIndex < 0)
            {
                return;
            }

            TeamResults selected = (TeamResults)dgvTeamResults.SelectedItem;
            int id = selected.TeamResultsID;

            currentTeamResults = data.GetTeamResultsByID(id);

            txtTeamResultsID.Text = currentTeamResults.TeamResultsID.ToString();
            eventID = currentTeamResults.EventID;
            gamePlayedID = currentTeamResults.GamesPlayedID;
            teamID = currentTeamResults.TeamID;
            opposingTeamID = currentTeamResults.OpposingTeamID;

            team1 = data.GetTeamDetailsByID(currentTeamResults.TeamID);
            team2 = data.GetTeamDetailsByID(currentTeamResults.OpposingTeamID);
            oldResults = currentTeamResults.Results;

            isNewEntry = false;
        }

        // Saves the currently selected combo box option to a variable.
        private void cboEventName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboEventName.SelectedItem != null)
            {
                string cbiString = cboEventName.SelectedItem.ToString();
                string eventIDString = String.Join("",
                    Regex.Matches(cbiString,
                    @"(?<=ID: )(.*?)(?= Name:)").Cast<Match>().Select(m => m.Groups[1].Value));
                eventID = int.Parse(eventIDString);
            }
        }

        // Saves the currently selected combo box option to a variable.
        private void cboGameName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboGameName.SelectedItem != null)
            {
                string cbiString = cboGameName.SelectedItem.ToString();
                string gamesPlayedIDString = String.Join("",
                    Regex.Matches(cbiString,
                    @"(?<=ID: )(.*?)(?= Name:)").Cast<Match>().Select(m => m.Groups[1].Value));
                gamePlayedID = int.Parse(gamesPlayedIDString);
            }
        }

        // Saves the currently selected combo box option to a variable.
        private void cboTeamName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTeamName.SelectedItem != null)
            {
                string cbiString = cboTeamName.SelectedItem.ToString();
                string TeamIDString = String.Join("",
                    Regex.Matches(cbiString,
                    @"(?<=ID: )(.*?)(?= Name:)").Cast<Match>().Select(m => m.Groups[1].Value));
                teamID = int.Parse(TeamIDString);
            }
        }

        // Saves the currently selected combo box option to a variable.
        private void cboOpposingTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboOpposingTeam.SelectedItem != null)
            {
                string cbiString = cboOpposingTeam.SelectedItem.ToString();
                string opposingTeamIDString = String.Join("",
                    Regex.Matches(cbiString,
                    @"(?<=ID: )(.*?)(?= Name:)").Cast<Match>().Select(m => m.Groups[1].Value));
                opposingTeamID = int.Parse(opposingTeamIDString);
            }
        }

        // Saves the currently selected combo box option to a variable.
        private void cboResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cboResultsString = cboResults.SelectedItem.ToString();
            if (cboResultsString.Contains("Team 1"))
            {
                results = "Win";
            }
            else if (cboResultsString.Contains("Team 2"))
            {
                results = "Lose";
            }
            else if (cboResultsString.Contains("Draw"))
            {
                results = "Draw";
            }

        }

        // Event that executes on clicking of the new button.
        private void btnNewRecord_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        // Event that executes on clicking of the save button.
        private void btnSaveRecord_Click(object sender, RoutedEventArgs e)
        {
            currentTeamResults.GamesPlayedID = gamePlayedID;
            currentTeamResults.TeamID = teamID;
            currentTeamResults.EventID = eventID;
            currentTeamResults.OpposingTeamID = opposingTeamID;
            currentTeamResults.Results = results;

            team1 = data.GetTeamDetailsByID(teamID);
            team2 = data.GetTeamDetailsByID(opposingTeamID);


            if (isNewEntry)
            {
                data.AddTeamResults(currentTeamResults);

                switch (results)
                {
                    case string str when str.Equals("Win"):
                        team1.CompPoints = team1.CompPoints + 2;
                        break;
                    case string str when str.Equals("Lose"):
                        team2.CompPoints = team2.CompPoints + 2;
                        break;
                    case string str when str.Equals("Draw"):
                        team1.CompPoints = team1.CompPoints + 1;
                        team2.CompPoints = team2.CompPoints + 1;
                        break;
                }
                data.UpdateTeamDetails(team1);
                data.UpdateTeamDetails(team2);
            }
            else
            {
                data.UpdateTeamResults(currentTeamResults);

                switch (oldResults)
                {
                    case string str when str.Equals("Win"):
                        switch (results)
                        {
                            case string str2 when str2.Equals("Win"):
                                break;
                            case string str2 when str2.Equals("Lose"):
                                team1.CompPoints = team1.CompPoints - 2;
                                team2.CompPoints = team2.CompPoints + 2;
                                break;
                            case string str2 when str2.Equals("Draw"):
                                team1.CompPoints = team1.CompPoints - 1;
                                team2.CompPoints = team2.CompPoints + 1;
                                break;
                        }
                        break;
                    case string str when str.Equals("Lose"):
                        switch (results)
                        {
                            case string str2 when str2.Equals("Win"):
                                team1.CompPoints = team1.CompPoints + 2;
                                team2.CompPoints = team2.CompPoints - 2;
                                break;
                            case string str2 when str2.Equals("Lose"):
                                break;
                            case string str2 when str2.Equals("Draw"):
                                team1.CompPoints = team1.CompPoints + 1;
                                team2.CompPoints = team2.CompPoints - 1;
                                break;
                        }
                        break;
                    case string str when str.Equals("Draw"):
                        switch (results)
                        {
                            case string str2 when str2.Equals("Win"):
                                team1.CompPoints = team1.CompPoints + 1;
                                team2.CompPoints = team2.CompPoints - 1;
                                break;
                            case string str2 when str2.Equals("Lose"):
                                team1.CompPoints = team1.CompPoints - 1;
                                team2.CompPoints = team2.CompPoints + 1;
                                break;
                            case string str2 when str2.Equals("Draw"):
                                break;
                        }
                        break;
                }
                data.UpdateTeamDetails(team1);
                data.UpdateTeamDetails(team2);
            }
            ClearTextBoxes();
            UpdateDataGrid();
        }

        // Event that executes on clicking of the delete button.
        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTeamResults.SelectedIndex < 0)
            {
                return;
            }

            TeamResults selected = (TeamResults)dgvTeamResults.SelectedItem;
            int id = selected.TeamResultsID;

            MessageBoxResult mBoxResult = MessageBox.Show("Are you sure you wish to delete this record?",
                                "Confimration", MessageBoxButton.YesNo);
            if (mBoxResult == MessageBoxResult.Yes)
            {
                data.DeleteTeamResults(id);
                ClearTextBoxes();
                UpdateDataGrid();
            }
        }
    }
}
