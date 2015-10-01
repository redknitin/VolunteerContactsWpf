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
using VolunteerContact.Model;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace VolunteerContact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Volunteer> lstVolunteers = null;
        private const string Filename = "volunteers.dat";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveData() {
            var serializer = new XmlSerializer(typeof(List<Volunteer>));
            //TextWriter writer = new StringWriter();
            var writer = new System.IO.StreamWriter(Filename);
            serializer.Serialize(writer, lstVolunteers);
            //writer.Flush(); //will close cause a Flush implicitly?
            writer.Close();
            //File.WriteAllText(Filename, writer.ToString());
        }

        private void LoadData() {
            if (!File.Exists(Filename)) return;
            var serializer = new XmlSerializer(typeof(List<Volunteer>));
            var instream = File.Open(Filename, FileMode.Open, FileAccess.Read);
            lstVolunteers = (List<Volunteer>) serializer.Deserialize(instream);
            instream.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            if (lstVolunteers == null)
            {
                //This is to load sample data
                lstVolunteers = new List<Volunteer>() {
                    new Volunteer() { /*Id=1,*/ Name="Nitin", Email="redknitin@gmail.com", Location="Dubai", Notes="App Dev" }
                };
            }

            dataGrid.ItemsSource = lstVolunteers;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to save", "Save", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                SaveData();
            }
            Environment.Exit(0);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }
    }
}
