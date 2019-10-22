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
using klassen_oef1.Lib;

namespace klassen_oef1.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Student> studenten = new List<Student>();
        List<Klas> klassen = new List<Klas>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoeStandaardWeergave();
            maakWatStudenten();
            maakWatKlassen();
            vulDeStudenten();
            vulDeKlassen();
        }
        void DoeStandaardWeergave()
        {
            lblID.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
            txtNaam.IsEnabled = false;
            txtVoornaam.IsEnabled = false;
            dtpGeboortedatum.IsEnabled = false;
            lstStudenten.IsEnabled = true;
        }
        void DoeBewerkingsWeergave()
        {
            lblID.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            txtNaam.IsEnabled = true;
            txtVoornaam.IsEnabled = true;
            dtpGeboortedatum.IsEnabled = true;
            lstStudenten.IsEnabled = false;

        }
        void maakWatStudenten()
        {
            Student leerling;
            leerling = new Student("Bibber", "Bert", new DateTime(2004, 12, 25));
            studenten.Add(leerling);
            leerling = new Student("Pienter", "Piet", new DateTime(2005, 4, 25));
            studenten.Add(leerling);
            leerling = new Student("Antigoon", "Suus", new DateTime(1874, 1, 1));
            studenten.Add(leerling);

        }
        void maakWatKlassen()
        {
            Klas klas;
            klas = new Klas("PRB G1");
            klassen.Add(klas);
            klas = new Klas("PRB G2");
            klassen.Add(klas);
            klas = new Klas("PRB G3");
            klassen.Add(klas);
        }
        void vulDeStudenten()
        {
            btnEdit.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

            lstStudenten.Items.Clear();
            foreach(Student student in studenten)
            {
                lstStudenten.Items.Add(student);

            }
            if(lstStudenten.Items.Count > 0)
            {
                lstStudenten.SelectedIndex = 0;
            }
            lstStudenten_SelectionChanged(lstStudenten, null);

        }
        void vulDeKlassen()
        {
            lstDeKlassen.Items.Clear();
            foreach(Klas klas in klassen)
            {
                lstDeKlassen.Items.Add(klas);
            }
            if(lstDeKlassen.Items.Count > 0)
            {
                lstDeKlassen.SelectedIndex = 0;
            }
            lstDeKlassen_SelectionChanged(lstDeKlassen, null);
            
        }

        private void lstStudenten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if(lstStudenten.SelectedValue != null)
            {
                Student zoekStudent = ZoekStudent(lstStudenten.SelectedValue.ToString());
                if (zoekStudent == null)
                    onverwachteSituatie();
                else
                {
                    lblID.Content = zoekStudent.ID;
                    txtNaam.Text = zoekStudent.Naam;
                    txtVoornaam.Text = zoekStudent.Voornaam;
                    dtpGeboortedatum.SelectedDate = zoekStudent.Geboortedatum;
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
            }
            else
            {
                onverwachteSituatie();
            }
        }
        private void onverwachteSituatie()
        {
            lblID.Content = "";
            txtNaam.Text = "";
            txtVoornaam.Text = "";
            dtpGeboortedatum.Text = "";
            btnEdit.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

        }
        private Student ZoekStudent(string zoek_id)
        {
            foreach (Student leerling in studenten)
            {
                if (leerling.ID == zoek_id)
                {
                    return leerling;
                }
            }
            return null;
        }
        private Klas ZoekKlas(string zoek_id)
        {
            foreach (Klas klas in klassen)
            {
                if (klas.ID == zoek_id)
                {
                    return klas;
                }
            }
            return null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DoeBewerkingsWeergave();
            dtpGeboortedatum.Text = "";
            txtNaam.Text = "";
            txtVoornaam.Text = "";
            txtNaam.Focus();
            lblBewerking.Content = "new";

        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstStudenten.SelectedIndex >= 0)
            {
                DoeBewerkingsWeergave();
                txtNaam.Focus();
                txtNaam.SelectAll();
                lblBewerking.Content = "edit";

            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DoeStandaardWeergave();
            if (lstStudenten.SelectedIndex > -1)
                lstStudenten_SelectionChanged(lstStudenten, null);
            else
            {
                onverwachteSituatie();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(lblBewerking.Content.ToString() == "new")
            {
                string ID = "";
                try
                {
                    Student leerling = new Student(txtNaam.Text, txtVoornaam.Text, dtpGeboortedatum.SelectedDate);
                    studenten.Add(leerling);
                    ID = leerling.ID;

                }
                catch (Exception fout)
                {
                    MessageBox.Show($"Er heeft zich een fout voorgedaan \n\n{fout.Message}", "Persoon NIET toegevoegd", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                finally
                {
                    DoeStandaardWeergave();
                    if(ID != "")
                    {
                        vulDeStudenten();
                        lstStudenten.SelectedValue = ID;
                        lstStudenten_SelectionChanged(lstStudenten, null);
                    }
                    else
                    {
                        if (lstStudenten.SelectedIndex > -1)
                            lstStudenten_SelectionChanged(lstStudenten, null);
                        else
                        {
                            onverwachteSituatie();
                        }
                    }
                }
            }
            else
            {
                string zoekID = lblID.Content.ToString();
                Student leerling = ZoekStudent(zoekID);
                if(leerling != null)
                {
                    try
                    {
                        leerling.Naam = txtNaam.Text;
                        leerling.Voornaam = txtVoornaam.Text;
                        leerling.Geboortedatum = (DateTime)dtpGeboortedatum.SelectedDate;
                    }
                    catch(Exception fout)
                    {
                        MessageBox.Show($"Er heeft zich een fout voorgedaan \n\n{fout.Message}", "Persoon NIET gewijzigd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        DoeStandaardWeergave();
                        if (zoekID != "")
                        {
                            vulDeStudenten();
                            lstStudenten.SelectedValue = zoekID;
                            lstStudenten_SelectionChanged(lstStudenten, null);
                        }
                        else
                        {
                            if (lstStudenten.SelectedIndex > -1)
                                lstStudenten_SelectionChanged(lstStudenten, null);
                            else
                            {
                                onverwachteSituatie();
                            }
                        }
                    }
                }
                else
                {
                    DoeStandaardWeergave();
                    onverwachteSituatie();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstStudenten.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Ben je zeker?", "Student wissen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    if (lstStudenten.SelectedValue != null)
                    {
                        Student zoekStudent = ZoekStudent(lstStudenten.SelectedValue.ToString());
                        if (zoekStudent != null)
                        {
                            studenten.Remove(zoekStudent);
                            vulDeStudenten();
                        }
                        else
                        {
                            onverwachteSituatie();
                        }
                    }
                }
            }
        }

        private void lstDeKlassen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDeKlassen.SelectedValue != null)
            {
                Klas zoekKlas = ZoekKlas(lstDeKlassen.SelectedValue.ToString());
                VulDeLeden(zoekKlas);
            }
            else
            {
                lstLeden.Items.Clear();
            }
        }
        private void VulDeLeden(Klas klas)
        {
            lstLeden.Items.Clear();
            List<Student> klasleden = klas.GetAllStudents();
            foreach (Student lid in klasleden)
            {
                lstLeden.Items.Add(lid);
            }

        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if(lstStudenten.SelectedIndex >= 0)
            {
                Student zoekStudent = ZoekStudent(lstStudenten.SelectedValue.ToString());
                Klas zoekKlas = ZoekKlas(lstDeKlassen.SelectedValue.ToString());
                zoekKlas.AddStudent(zoekStudent);
                VulDeLeden(zoekKlas);


            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if(lstLeden.SelectedIndex >= 0)
            {
                Student zoekStudent = ZoekStudent(lstLeden.SelectedValue.ToString());
                Klas zoekKlas = ZoekKlas(lstDeKlassen.SelectedValue.ToString());
                zoekKlas.RemoveStudent(zoekStudent);
                VulDeLeden(zoekKlas);
            }
        }
    }
}
