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
    /// Interaction logic for TeamDetailsWindow.xaml
    /// </summary>
    public partial class TeamDetailsWindow : Window
    {
        // Creates the DataAccess object needed to access the class methods.
        DataAccess data = new DataAccess();
        // Creates a list used to store the TeamDetails objects.
        List<TeamDetails> TeamDetailsList = new List<TeamDetails>();
        // Creates a list used to store the PrimaryContact objects.
        List<PrimaryContact> PrimaryContactNameList = new List<PrimaryContact>();
        // A singular TeamDetails object used to store the currently selected Events object.
        TeamDetails currentTeamDetails = new TeamDetails();
        // Stores if the current entry being entered will be a new entry or not.
        bool isNewEntry = true;

        // Main TeamDetailsWindow constructor.
        public TeamDetailsWindow()
        {
            InitializeComponent();
            UpdateComboBox();
            UpdateDataGrid();
        }

        // Method used to populate and refresh the data grid.
        public void UpdateDataGrid()
        {
            TeamDetailsList = data.GetTeamDetails();
            dgvTeamDetails.ItemsSource = TeamDetailsList;
            dgvTeamDetails.Items.Refresh();
        }

        // Method used to populate and update the combo boxes with data.
        public void UpdateComboBox()
        {
            PrimaryContactNameList = data.GetPrimaryContactsNameAndID();

            foreach (PrimaryContact contact in PrimaryContactNameList)
            {
                cboPrimaryContactName.Items.Add($"ID: {contact.PrimaryContactID}" +
                                               $" Name: {contact.PrimaryContactName}");
            }
        }

        // Clears the fields and readies the creation of a new record.
        private void ClearTextBoxes()
        {
            txtTeamName.Text = "";
            txtTeamID.Text = "";
            txtPrimaryContactID.Text = "";
            txtCompPoints.Text = "";
            isNewEntry = true;
        }

        // Event that executes on clicking of the new button.
        private void btnNewRecord_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        // Event that executes on clicking of the save button.
        private void btnSaveRecord_Click(object sender, RoutedEventArgs e)
        {
            currentTeamDetails.TeamName = txtTeamName.Text;
            currentTeamDetails.PrimaryContactId = int.Parse(txtPrimaryContactID.Text);
            currentTeamDetails.CompPoints = int.Parse(txtCompPoints.Text);

            if (isNewEntry)
            {
                data.AddTeamDetails(currentTeamDetails);
            }
            else
            {
                data.UpdateTeamDetails(currentTeamDetails);
            }
            ClearTextBoxes();
            UpdateDataGrid();
        }

        // Event that exectutes when a row is selected in the data grid.
        private void dgvTeamDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvTeamDetails.SelectedIndex < 0)
            {
                return;
            }

            TeamDetails selected = (TeamDetails)dgvTeamDetails.SelectedItem;
            int id = selected.TeamID;

            currentTeamDetails = data.GetTeamDetailsByID(id);

            txtTeamID.Text = currentTeamDetails.TeamID.ToString();
            txtTeamName.Text = currentTeamDetails.TeamName;
            txtPrimaryContactID.Text = currentTeamDetails.PrimaryContactId.ToString();
            txtCompPoints.Text = currentTeamDetails.CompPoints.ToString();

            isNewEntry = false;
        }

        // Saves the currently selected option in the combo box to a text field.
        private void cboPrimaryContactName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboPrimaryContactName.SelectedItem != null)
            {
                string cbiString = cboPrimaryContactName.SelectedItem.ToString();
                string primaryContactID = String.Join("",
                    Regex.Matches(cbiString,
                    @"(?<=ID: )(.*?)(?= Name:)").Cast<Match>().Select(m => m.Groups[1].Value));
                txtPrimaryContactID.Text = primaryContactID.ToString();
            }
        }

        // Event that executes on clicking of the delete button.
        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTeamDetails.SelectedIndex < 0)
            {
                return;
            }

            TeamDetails selected = (TeamDetails)dgvTeamDetails.SelectedItem;
            int id = selected.TeamID;

            MessageBoxResult result = MessageBox.Show("Are you sure you wish to delete this record?",
                                            "Confimration", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                data.DeleteTeamDetails(id);
                ClearTextBoxes();
                UpdateDataGrid();
            }
        }
    }
}
