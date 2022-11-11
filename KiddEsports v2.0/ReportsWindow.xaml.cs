using Dapper;
using DataManagement;
using DataManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : Window
    {
        // Creates the DataAccess object needed to access the class methods.
        DataAccess data = new DataAccess();
        // Creates a list used to store the Team Results List.
        List<TeamResults> TeamResultsList = new List<TeamResults>();
        // Creates a list used to store the Team Details object.
        List<TeamDetails> TeamDetailsList = new List<TeamDetails>();

        // Main ReportsWindow constructor.
        public ReportsWindow()
        {
            InitializeComponent();
            UpdateComboBoxes();
        }

        // Method used to populate and update the combo boxes with data.
        public void UpdateComboBoxes()
        {
            TeamDetailsList = data.GetTeamDetailsNameAndID();

            foreach (TeamDetails team in TeamDetailsList)
            {
                cboTeamName.Items.Add($"ID: {team.TeamID}" +
                                       $" Name: {team.TeamName}");
            }
        }

        // Event that executes on clicking of the export button.
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            bool teamNameListHasAny= TeamDetailsList.Any();
            bool teamResultsListHasAny = TeamResultsList.Any();

            if (teamNameListHasAny)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter("Report.txt"))
                    {
                        foreach (var team in TeamDetailsList)
                        {
                            writer.WriteLine($"Team ID: {team.TeamID}, Team Name: {team.TeamName}, Primary Contact ID: {team.PrimaryContactId}, " +
                                $"Primary Contact Name: {team.PrimaryContactName}, Comp Points: {team.CompPoints}");
                        }
                    }
                }
                catch (Exception)
                {

                }
                MessageBox.Show("Report successfully exported.");
            }
            else if (teamResultsListHasAny)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter("Report.txt"))
                    {
                        foreach (var results in TeamResultsList)
                        {
                            writer.WriteLine($"Team Results ID: {results.TeamResultsID}, Event ID: {results.EventID}, " +
                                $"Event Name: {results.EventName}, Games Played ID: {results.GamesPlayedID}, " +
                                $"Game Name: {results.GameName}, Team ID: {results.TeamID}, Team Name: {results.TeamName}, " +
                                $"Opposing Team ID: {results.OpposingTeamID}, Opposing Team Name: {results.OpposingTeam}, Result: {results.Results}");
                        }
                    }
                }
                catch (Exception)
                {

                }
                MessageBox.Show("Report successfully exported.");
            }
        }

        private void cboReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgvReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Event that executes on clicking of the generate report button.
        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            string cboTeamNameContents = cboTeamName.SelectedItem.ToString();

            string teamNameString = String.Join("",
                    Regex.Matches(cboTeamNameContents,
                    @"(?<=.ComboBoxItem: )(.*$)").Cast<Match>().Select(m => m.Groups[1].Value));

            string teamNameStringRegex = String.Join("",
                    Regex.Matches(cboTeamNameContents,
                    @"(?<=Name: )(.*$)").Cast<Match>().Select(m => m.Groups[1].Value));

            try
            {
                if (teamNameString != "No Filter")
                {
                    switch (cboReportType.SelectedValue.ToString())
                    {
                        case string str when str.Equals("Team Details: Sorted by Comp Points"):
                            TeamDetailsList = data.GetTeamDetailsReportByNameCompPoints(teamNameStringRegex);
                            TeamResultsList.Clear();
                            dgvReport.ItemsSource = TeamDetailsList;
                            dgvReport.Items.Refresh();
                            break;

                        case string str when str.Equals("Team Results: Sorted by Event"):
                            TeamResultsList = data.GetTeamResultsReportByNameEvent(teamNameStringRegex);
                            TeamDetailsList.Clear();
                            dgvReport.ItemsSource = TeamResultsList;
                            dgvReport.Items.Refresh();
                            break;
                        case String str when str.Equals("Team Results: Sorted by Team Name"):
                            TeamResultsList = data.GetTeamResultsReportByNameTeam(teamNameStringRegex);
                            TeamDetailsList.Clear();
                            dgvReport.ItemsSource = TeamResultsList;
                            dgvReport.Items.Refresh();
                            break;
                    }
                }
                else
                {
                    switch (cboReportType.SelectedValue.ToString())
                    {
                        case string str when str.Equals("Team Details: Sorted by Comp Points"):
                            TeamDetailsList = data.GetTeamDetailsReportCompPoints();
                            TeamResultsList.Clear();
                            dgvReport.ItemsSource = TeamDetailsList;
                            dgvReport.Items.Refresh();
                            break;

                        case string str when str.Equals("Team Results: Sorted by Event"):
                            TeamResultsList = data.GetTeamResultsReportEvent();
                            TeamDetailsList.Clear();
                            dgvReport.ItemsSource = TeamResultsList;
                            dgvReport.Items.Refresh();
                            break;
                        case String str when str.Equals("Team Results: Sorted by Team Name"):
                            TeamResultsList = data.GetTeamResultsReportTeam();
                            TeamDetailsList.Clear();
                            dgvReport.ItemsSource = TeamResultsList;
                            dgvReport.Items.Refresh();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboTeamName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
