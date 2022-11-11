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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KiddEsports_v2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Main MainWindow constructor.
        public MainWindow()
        {
            InitializeComponent();
        }

        // Opens up the TeamDetailsWindow when the Teams button is clicked.
        private void btnTeams_Click(object sender, RoutedEventArgs e)
        {
            TeamDetailsWindow window = new TeamDetailsWindow();
            window.ShowDialog();
        }

        // Opens up the TeamResultsWindow when the Results button is clicked.
        private void btnResults_Click(object sender, RoutedEventArgs e)
        {
            TeamResultsWindow window = new TeamResultsWindow();
            window.ShowDialog();
        }

        // Opens up the GamesPlayedWindow when the Games button is clicked.
        private void btnGames_Click(object sender, RoutedEventArgs e)
        {
            GamesPlayedWindow window = new GamesPlayedWindow();
            window.ShowDialog();
        }

        // Opens up the EventsWindow when the Events button is clicked.
        private void btnEventLocations_Click(object sender, RoutedEventArgs e)
        {
            EventsWindow window = new EventsWindow();
            window.ShowDialog();
        }

        // Opens up the ReportsWindow when the Reports button is clicked.
        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            ReportsWindow window = new ReportsWindow();
            window.ShowDialog();
        }

        // Opens up the PrimaryContactWindow when the Primary Contact button is clicked.
        private void btnPrimaryContacts_Click(object sender, RoutedEventArgs e)
        {
            PrimaryContactWindow winodw = new PrimaryContactWindow();
            winodw.ShowDialog();
        }
    }
}
