using DataManagement;
using DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for GamesPlayedWindow.xaml
    /// </summary>
    public partial class GamesPlayedWindow : Window
    {
        // Creates the DataAccess object needed to access the class methods.
        DataAccess data = new DataAccess();
        // Creates a list used to store the Games Played objects.
        List<GamesPlayed> GamesPlayedList = new List<GamesPlayed>();
        // A singular Events object used to store the currently selected Games Played object.
        GamesPlayed currentGamesPlayed = new GamesPlayed();
        // Stores if the current entry being entered will be a new entry or not.
        bool isNewEntry = true;

        // Main GamesPlayedWindow constructor.
        public GamesPlayedWindow()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        // Method used to populate and refresh the data grid.
        public void UpdateDataGrid()
        {
            GamesPlayedList = data.GetGamesPlayed();
            dgvGamesPlayed.ItemsSource = GamesPlayedList;
            dgvGamesPlayed.Items.Refresh();
        }

        // Event that exectutes when a row is selected in the data grid.
        private void dgvGamesPlayed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvGamesPlayed.SelectedIndex < 0)
            {
                return;
            }

            GamesPlayed selected = (GamesPlayed)dgvGamesPlayed.SelectedItem;
            int id = selected.GamesPlayedID;

            currentGamesPlayed = data.GetGamesPlayedByID(id);

            txtGameID.Text = currentGamesPlayed.GamesPlayedID.ToString();
            txtGameName.Text = currentGamesPlayed.GameName;
            txtGameType.Text = currentGamesPlayed.GameType;

            isNewEntry = false;
        }

        // Clears the fields and readies the creation of a new record.
        private void ClearTextBoxes()
        {
            txtGameID.Text = "";
            txtGameName.Text = "";
            txtGameType.Text = "";
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
            currentGamesPlayed.GameName = txtGameName.Text;
            currentGamesPlayed.GameType = txtGameType.Text;

            if (isNewEntry)
            {
                data.AddGamesPlayed(currentGamesPlayed);
            }
            else
            {
                data.UpdateGamesPlayed(currentGamesPlayed);
            }
            ClearTextBoxes();
            UpdateDataGrid();
        }

        // Event that executes on clicking of the delete button.
        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dgvGamesPlayed.SelectedIndex < 0)
            {
                return;
            }

            GamesPlayed selected = (GamesPlayed)dgvGamesPlayed.SelectedItem;
            int id = selected.GamesPlayedID;

            MessageBoxResult result = MessageBox.Show("Are you sure you wish to delete this record?",
                                            "Confimration", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                data.DeleteGamesPlayed(id);
                ClearTextBoxes();
                UpdateDataGrid();
            }
        }
    }
}
