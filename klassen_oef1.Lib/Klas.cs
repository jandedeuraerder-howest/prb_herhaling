using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace klassen_oef1.Lib
{
    public class Klas
    {
        private string id;
        private string klasnaam;
        public string ID
        {
            get { return id; }
        }

        public string Klasnaam
        {
            get { return klasnaam; }
            set
            {
                if (value.Trim() == "")
                    throw new Exception("Klasnaam kan niet leeg zijn");
                else
                    klasnaam = value;
            }
        }
        private List<Student> leden;

        public Klas()
        {
            id = Guid.NewGuid().ToString();
            leden = new List<Student>();
        }
        public Klas(string klasnaam):this()  
            // hierdoor wordt de code van de argumentloze constructor ook uitgevoerd
            // en krijgt het private veld id zeker een waarde
        {
            if (klasnaam.Trim() == "")
                throw new Exception("Klasnaam kan niet leeg zijn");
            else
                this.klasnaam = klasnaam;
        }

        public void AddStudent(Student klaslid)
        {
            leden.Add(klaslid);
        }
        public void RemoveStudent(Student klaslid)
        {
            leden.Remove(klaslid);
        }
        public List<Student> GetAllStudents()
        {
            return leden;
        }
        public Student FindStudent(string id)
        {
            foreach (Student leerling in leden)
            {
                if (leerling.ID == id)
                {
                    return leerling;
                }
            }
            return null;
        }



    }

    
}
