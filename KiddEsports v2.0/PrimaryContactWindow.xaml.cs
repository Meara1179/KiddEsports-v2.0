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
using DataManagement;
using DataManagement.Models;


namespace KiddEsports_v2._0
{
    /// <summary>
    /// Interaction logic for PrimaryContactWindow.xaml
    /// </summary>
    public partial class PrimaryContactWindow : Window
    {
        DataAccess data = new DataAccess();
        List<PrimaryContact> PrimaryContactList = new List<PrimaryContact>();
        PrimaryContact currentPrimaryContact = new PrimaryContact();
        bool isNewEntry = true;
        // PrimaryContactWindow constructor that executes on window open.
        public PrimaryContactWindow()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        // Method to handle populating and updating the data grid.
        public void UpdateDataGrid()
        {
            PrimaryContactList = data.GetPrimaryContacts();
            dgvPrimaryContact.ItemsSource = PrimaryContactList;
            dgvPrimaryContact.Items.Refresh();
        }

        // Event that exectutes when a row is selected in the data grid.
        private void dgvPrimaryContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvPrimaryContact.SelectedIndex < 0)
            {
                return;
            }

            PrimaryContact selected = (PrimaryContact)dgvPrimaryContact.SelectedItem;
            int id = selected.PrimaryContactID;

            currentPrimaryContact = data.GetPrimaryContactsByID(id);

            txtPrimaryContactId.Text = currentPrimaryContact.PrimaryContactID.ToString();
            txtPrimaryContactName.Text = currentPrimaryContact.PrimaryContactName;
            txtPrimaryContactPhone.Text = currentPrimaryContact.PrimaryContactPhone;
            txtPrimaryContactEmail.Text = currentPrimaryContact.PrimaryContactEmail;

            isNewEntry = false;
        }

        // Clears the fields and readies the creation of a new record.
        private void ClearTextBoxes()
        {
            txtPrimaryContactId.Text = "";
            txtPrimaryContactName.Text = "";
            txtPrimaryContactPhone.Text = "";
            txtPrimaryContactEmail.Text = "";
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
            currentPrimaryContact.PrimaryContactName = txtPrimaryContactName.Text;
            currentPrimaryContact.PrimaryContactPhone = txtPrimaryContactPhone.Text;
            currentPrimaryContact.PrimaryContactEmail = txtPrimaryContactEmail.Text;

            if (isNewEntry)
            {
                data.AddPrimaryContact(currentPrimaryContact);
            }
            else
            {
                data.UpdatePrimaryContact(currentPrimaryContact);
            }
            ClearTextBoxes();
            UpdateDataGrid();
        }

        // Event that executes on clicking of the delete button.
        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dgvPrimaryContact.SelectedIndex < 0)
            {
                return;
            }

            PrimaryContact selected = (PrimaryContact)dgvPrimaryContact.SelectedItem;
            int id = selected.PrimaryContactID;

            MessageBoxResult result = MessageBox.Show("Are you sure you wish to delete this record?", 
                                                        "Confimration", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                data.DeletePrimaryContact(id);
                ClearTextBoxes();
                UpdateDataGrid();
            }
        }
    }
}
