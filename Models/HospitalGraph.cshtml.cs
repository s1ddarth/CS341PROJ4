using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
  
namespace program.Pages  
{  
    public class HospitalGraphModel : PageModel  
    {
        // Store the dates on which test data is recorded
        public List<string> dates { get; set; }
        // the number of tests reporting individuals with the disease
        public List<int> hospitalized { get; set; }
        // the number of deaths reported due to the disease
        public List<int> deaths { get; set; }
        
        public string Input { get; set; }
        
        public Exception EX { get; set; }
  
        // When the page is loaded
        public void OnGet(string input)  
        {
          
          dates = new List<string>();
          hospitalized = new List<int>();
          deaths = new List<int>();
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
                            SELECT date as DATE, hospitalized, death
                            FROM us_states_covid19_daily
                            WHERE state LIKE '%{0}%'
                            ", input);
            
                DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
                
                int hospitINT;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string date = Convert.ToString(row["DATE"]);
                    dates.Add(date);

                    string hospit = Convert.ToString(row["hospitalized"]);
                    if(hospit == ""){
						hospitINT = 0;
					}
					else hospitINT = Convert.ToInt32(hospit); 
					hospitalized.Add(hospitINT);
                    
					int death = Convert.ToInt32(row["death"]);
                    deaths.Add(death);
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