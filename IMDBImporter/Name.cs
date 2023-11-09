using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class Name
    {

        public string nameID { get; set; }
        public string name { get; set; }
        public int? birthYear { get; set; }
        public int? deathYear { get; set; }

        public List<string>? professions { get; set; }
        public List<string>? knownForTitles { get; set; }


        public Name(string nameID, string name, int? birthYear, int? deathYear, string professionsString, string knownForTitlesString)
        {
            this.nameID = nameID;
            this.name = name;
            this.birthYear = birthYear;
            this.deathYear = deathYear;

            professions = professionsString.Split(",").ToList();
            knownForTitles = knownForTitlesString.Split(",").ToList();
        }

    }
}
