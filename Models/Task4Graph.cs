using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
  
namespace program.Pages  
{  
    public class Task4GraphModel : PageModel  
    {
        // Store the dates on which test data is recorded
        public List<string> dates { get; set; }
        // the number of tests reporting individuals with the disease
        public List<int> total { get; set; }
        // the number of deaths reported due to the disease
        
        public string Input { get; set; }
        
        public Exception EX { get; set; }
  
        // When the page is loaded
        public void OnGet(string input)  
        {

            dates = new List<string>();
            total = new List<int>();

            Input = input;
          
            EX = null;
          
            try
            {
              if (input == null)
              {
                //
                // there's no page argument, perhaps user surfed to the page directly?  
                // In this case, nothing to do.
                //
              }
            
            else{
                input = input.Replace("'", "''");
                // query retrieving data from US testing data table
                string sql = string.Format(@"
                            SELECT date as DATE, total
                            FROM us_states_covid19_daily
                            WHERE state LIKE '%{0}%'
                            ORDER BY date ASC
                            ", input);
            
                DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
                                
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string date = Convert.ToString(row["DATE"]);

                    int rowTotal = Convert.ToInt32(row["total"]);

                    dates.Add(date);
                    total.Add(rowTotal);
                }

            }}
                
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