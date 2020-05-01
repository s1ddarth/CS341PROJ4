using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
  
namespace program.Pages  
{  
    public class NumbersByDateModel : PageModel  
    {
        // Store the dates on which test data is recorded
        public List<string> dates { get; set; }
        // the number of tests reporting individuals with the disease
        public List<int> positive { get; set; } 
        // the number of tests reporting individuals who did not have the disease
        public List<int> negative { get; set; }
        // the number of deaths reported due to the disease
        public List<int> deaths { get; set; }
        // Annotations may not appear on every day, so the label and value are stored
        // as a tuple, accessed via .Item1 and .Item2
        public List<Tuple<string, string>> annotations { get; set; }
        
        public Exception EX { get; set; }
  
        // When the page is loaded
        public void OnGet()  
        {
          dates = new List<string>();
          positive = new List<int>();
          negative = new List<int>();
          deaths = new List<int>();
          annotations = new List<Tuple<string, string>>();
          
          EX = null;
          
          try
          {
            // query retrieving data from US testing data table
            string sql = string.Format(@"
SELECT date AS DATE, Sum(positive) AS positive, Sum(negative) as negative, Sum(death) as deaths
FROM us_states_covid19_daily
GROUP BY date
ORDER BY date;
");
          
            DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
            int weekday= 0;
            int week = 0;
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
              string date = Convert.ToString(row["DATE"]);
              dates.Add(date);
              int posi = Convert.ToInt32(row["positive"]);
              positive.Add(posi);
              int neg = Convert.ToInt32(row["negative"]);
              negative.Add(neg);
              int death = Convert.ToInt32(row["deaths"]);
              deaths.Add(death);
              ++weekday;
              if(weekday>=7)  // Create an annotation for the end of each week
              {
                ++week;
                annotations.Add(new Tuple<string, string>(date, "Week "+week));
                weekday = 0;
              }
            }
		      }
		      catch(Exception ex)
		      {
            EX = ex;
		      }
		      finally
		      { 
            // nothing at the moment, the data access tier cleans up after itself
          } 
        }  
        
    }//class
}//namespace