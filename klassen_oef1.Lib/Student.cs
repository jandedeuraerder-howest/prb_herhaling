using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace klassen_oef1.Lib
{
    public class Student
    {
        private string id;
        private string naam;
        private string voornaam;
        private DateTime? geboorteDatum;

        public string ID
        {
            get { return id; }
        }
        public string Naam
        {
            get { return naam; }
            set 
            {
                if (value.Trim() == "")
                    throw new Exception("Naam kan niet leeg zijn");
                else
                    naam = value; 
            }
        }
        public string Voornaam
        {
            get { return voornaam; }
            set 
            {
                if (value.Trim() == "")
                    throw new Exception("Voornaam kan niet leeg zijn");
                else
                    voornaam = value;
            }
        }
        public DateTime? Geboortedatum
        {
            get {
                return geboorteDatum;  
            }
            set
            {
                try
                {
                    geboorteDatum = value;
                }
                catch
                {
                    throw new Exception( "Er werd een ongeldige datum ingevoerd!");
                }
            }
        }
        public Student() : this("", "", null)  
            // vrij nutteloos, maar als voorbeeld koppelen (chainen) we de 
            // default constructor met een constructor die argumenten verwacht
        {
        }
        public Student(string naam, string voornaam, DateTime? geboorteDatum)
        {
            id = Guid.NewGuid().ToString();
            if (naam.Trim() == "")
                throw new Exception("Naam kan niet leeg zijn");
            else
                this.naam = naam;
            if (voornaam.Trim() == "")
                throw new Exception("Voornaam kan niet leeg zijn");
            else
                this.voornaam = voornaam;
            try
            {
                this.geboorteDatum = geboorteDatum;
            }
            catch
            {
                throw new Exception("Er werd een ongeldige datum ingevoerd!");
            }
        }

    }
}
