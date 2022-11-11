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
    /// Interaction logic for EventsWindow.xaml
    /// </summary>
    public partial class EventsWindow : Window
    {
        // Creates the DataAccess object needed to access the class methods.
        DataAccess data = new DataAccess();
        // Creates a list used to store the Events objects.
        List<Events> EventsList = new List<Events>();
        // A singular Events object used to store the currently selected Events object.
        Events CurrentEvents = new Events();
        // Stores if the current entry being entered will be a new entry or not.
        bool isNewEntry = true;

        // Main EventsWindow constructor.
        public EventsWindow()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        // Method used to populate and refresh the data grid.
        public void UpdateDataGrid()
        {
            EventsList = data.GetEvents();
            dgvEvents.ItemsSource = EventsList;
            dgvEvents.Items.Refresh();
        }

        // Event that exectutes when a row is selected in the data grid.
        private void dgvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvEvents.SelectedIndex < 0)
            {
                return;
            }

            Events selected = (Events)dgvEvents.SelectedItem;
            int id = selected.EventId;

            CurrentEvents = data.GetEventsByID(id);

            txtEventID.Text = CurrentEvents.EventId.ToString();
            txtEventName.Text = CurrentEvents.EventName;
            txtEventLocation.Text = CurrentEvents.EventLocation;
            dtpEventDate.Text = CurrentEvents.EventDate;

            isNewEntry = false;
        }

        // Clears the fields and readies the creation of a new record.
        private void ClearTextBoxes()
        {
            txtEventID.Text = "";
            txtEventName.Text = "";
            txtEventLocation.Text = "";
            dtpEventDate.Text = "";

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
            CurrentEvents.EventName = txtEventName.Text;
            CurrentEvents.EventLocation = txtEventLocation.Text;
            CurrentEvents.EventDate = dtpEventDate.Text;

            if (isNewEntry)
            {
                data.AddEvents(CurrentEvents);
            }
            else
            {
                data.UpdateEvents(CurrentEvents);
            }
            ClearTextBoxes();
            UpdateDataGrid();
        }

        // Event that executes on clicking of the delete button.
        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dgvEvents.SelectedIndex < 0)
            {
                return;
            }

            Events selected = (Events)dgvEvents.SelectedItem;
            int id = selected.EventId;

            MessageBoxResult result = MessageBox.Show("Are you sure you wish to delete this record?",
                                                        "Confimration", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                data.DeleteEvents(id);
                ClearTextBoxes();
                UpdateDataGrid();
            }
        }
    }
}
