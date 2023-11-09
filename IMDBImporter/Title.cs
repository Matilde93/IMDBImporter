using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBImporter
{
    public class Title
    {
        
        public string titleID { get; set; }
        public string titleType { get; set; }
        public string primaryTitle { get; set; }
        public string originalTitle { get; set; }
        public bool isAdult { get; set; }
        public int? startYear { get; set; }
        public int? endYear { get; set; }
        public int? runTimeMinutes { get; set; }
        public List<string> genres { get; set; }


        public Title(string titleID, string titleType, string primaryTitle, string originalTitle, bool isAdult, int? startYear, int? endYear, int? runTimeMinutes, string genresString)
        {
            this.titleID = titleID;
            this.titleType = titleType;
            this.primaryTitle = primaryTitle;
            this.originalTitle = originalTitle;
            this.isAdult = isAdult;
            this.startYear = startYear;
            this.endYear = endYear;
            this.runTimeMinutes = runTimeMinutes;

            genres = genresString.Split(",").ToList();
        }
 
        }

    
}
