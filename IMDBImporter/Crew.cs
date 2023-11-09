using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class Crew
    {

        public string titleID { get; set; }
        public List<string>? directors { get; set; }
        public List<string>? writers { get; set; }

        public Crew(string titleID, string directorsString, string writersString)
        {
            this.titleID = titleID;

            directors = directorsString.Split(",").ToList();
            writers = writersString.Split(",").ToList();
        }

    }
}
